using Mvasilva.Framework.BLL.Util;
using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mvasilva.Framework.BLL.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PermitionActionFilter : ActionFilterAttribute
    {

        public int PermitionValue { get; set; }

        public int PermitionType { get; set; }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UrlHelper url = new UrlHelper(filterContext.RequestContext);


            if (Variables.CurrentUser != null)
            {
                PermissionValidation(filterContext);
            }
            else
            {
                RedirectAuthentication(filterContext);
            }




            base.OnActionExecuting(filterContext);

        }


        private void PermissionValidation(ActionExecutingContext filterContext)
        {
            try
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];

                Permission permission = PermitionBLL.SelectByUser(Variables.CurrentUser, new Permission { Action = actionName, Controller = controllerName, Module = Variables.CurrentModule });

                if (permission == null || (permission.Value & PermitionValue) != PermitionValue)
                {
                    RedirectAuthentication(filterContext);
                }

                if (permission != null && PermitionType == 2)
                {
                    Variables.CurrentPermition = permission;
                }
                else
                {

                    try
                    {

                        StringBuilder sb = new StringBuilder();
                        foreach (var param in filterContext.ActionParameters)
                        {

                            Type t = param.GetType();


                            if (t.IsPrimitive || t.FullName == "System.String" || t.FullName == "System.DateTime")
                            {
                                sb.AppendFormat("{0}=({1})", param.Key, Convert.ToString(param.Value));
                            }
                            else
                            {
                                sb.AppendFormat("{0}=({1})", param.Key, GetPropValue(param.Value));

                            }
                        }

                        permission.ParametersLog = sb.ToString();

                    }
                    catch (Exception)
                    {


                    }




                }


                LogBLL.Insert(Variables.CurrentUser, Variables.CurrentPermition);


            }
            catch (Exception)
            {


                RedirectAuthentication(filterContext);

            }

        }


        private void RedirectAuthentication(ActionExecutingContext filterContext)
        {
            if (PermitionType != 3)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.Result = new HttpStatusCodeResult(403, "Forbidden.");
                }
                else
                {
                    filterContext.Result = GoToLogin(GetUrlWithoutQueryString(filterContext));
                    // filterContext.Result = Util.Util.GoToHome();
                }
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                filterContext.Result = new HttpStatusCodeResult(200, string.Empty);
            }
        }



        public static RedirectResult GoToLogin(string _url)
        {

            //HttpContext.Current.Response.Redirect(string.Format("{0}?{1}={2}", GetUrlLogin(), SingleSignOn.qsReturnUrl, _url), true);

            return new RedirectResult(string.Format("{0}?{1}={2}", GetUrlLogin(), Variables.qsReturnUrl, HttpUtility.UrlEncode(_url)));
        }


        public static string GetUrlLogin()
        {
            return ConfigurationManager.AppSettings["LoginPath"];
        }

        private string GetPropValue(object obj)
        {
            Type t = obj.GetType();

            StringBuilder sb = new StringBuilder();

            foreach (PropertyInfo item in t.GetProperties())
            {


                if (item.PropertyType.IsPublic && item.PropertyType.IsVisible && (!item.PropertyType.IsCOMObject))
                {

                    if (item.PropertyType.IsPrimitive || item.PropertyType.FullName == "System.String" || item.PropertyType.FullName == "System.DateTime")
                    {
                        try
                        {

                            sb.AppendFormat("{0}=({1})", item.Name, item.GetValue(obj) != null ? Convert.ToString(item.GetValue(obj)) : "null");
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        object objAux = item.GetValue(obj);

                        if (item.PropertyType.Name != "List`1")
                        {
                            try
                            {

                                sb.AppendFormat("{0}=({1})", item.Name, objAux != null ? GetPropValue(objAux) : "null");
                            }
                            catch
                            {

                            }
                        }
                    }
                }



            }


            return sb.ToString();


        }


        private string GetUrlWithoutQueryString(ActionExecutingContext filterContext)
        {

            filterContext.RouteData.Values.Remove("Token");

            Uri _uri = filterContext.HttpContext.Request.Url;
            return _uri.GetLeftPart(UriPartial.Path);
        }


    }








}
