using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvasilva.Framework.BLL.Util
{
    public class Variables
    {


        public const string ssLoginUser = "ssLoginUser";
        public const string qsReturnUrl = "returnUrl";
        public const string ssCurrentModule = "ssCurrentModule";
        public const string ssCurrentPermition = "ssCurrentPermition";
        




        public static User CurrentUser
        {
            get { return HttpContext.Current.Session[ssLoginUser] as User; }
            set { HttpContext.Current.Session[ssLoginUser] = value; }
        }
        public static Module CurrentModule
        {
            get
            {



                if (HttpContext.Current.Session[ssCurrentModule] == null)
                {

                    HttpContext.Current.Session[ssCurrentModule] = new Module { Id = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentModule"]) };
                }



                return HttpContext.Current.Session[ssCurrentModule] as Module;

            }

        }

        public static Permission CurrentPermition
        {
            get { return HttpContext.Current.Session[ssCurrentPermition] as Permission; }
            set { HttpContext.Current.Session[ssCurrentPermition] = value; }
        }

    }
}
