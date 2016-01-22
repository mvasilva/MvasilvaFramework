using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Mvasilva.Framework.BLL.Util
{
    public static class EmailHelper
    {
        //#region Properties

        /// <summary>
        /// Gets the specified email pickup directory.
        /// </summary>
        static string SpecifiedPickupDirectory
        {
            get
            {
                return EmailHelperConfig.GetSpecifiedPickupDirectory();
            }
        }

        //#endregion

        //#region Methods

        private static SmtpClient GetSmtpClient()
        {
            SmtpClient smtp = new SmtpClient();

            if (EmailHelperConfig.GetSmtpDeliveryMethod() == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                smtp.PickupDirectoryLocation = SpecifiedPickupDirectory;
            }

            return smtp;
        }

        /// <summary>
        /// Sends an email message to a single recipient in the specified format.
        /// </summary>
        /// <param name="fromAddress">The email sender address.</param>
        /// <param name="toAddress">The email recipient address, or a coma separeted list of email recipient addresses.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <param name="format">The email body format.</param>
        /// <returns>A string containing the status of the operation.</returns>
        public static void Send(string fromAddress, string toAddress, string subject, string body, EmailFormat format)
        {
            Send(fromAddress, toAddress, null, subject, body, format, null, null);
        }

        public static void Send(string fromAddress, string toAddress, string toBcc, string subject, string body, EmailFormat format)
        {
            Send(fromAddress, toAddress, toBcc, subject, body, format, null, null);
        }

        public static void Send(string fromAddress, string toAddress, string subject, string body, EmailFormat format, List<ImagesResourceEmail> resourceList)
        {
            Send(fromAddress, toAddress, null, subject, body, format, resourceList, null);
        }

        public static void Send(string fromAddress, string toAddress, string toBcc, string subject, string body, EmailFormat format, List<ImagesResourceEmail> resourceList, params string[] files)
        {
            SmtpClient smtp = GetSmtpClient();
            MailMessage message = new MailMessage();

            // Send the email
            try
            {
                // Set the from address
                if (fromAddress.IndexOf(',') >= 0)
                    fromAddress = fromAddress.Substring(0, fromAddress.IndexOf(','));

                message.Priority = MailPriority.High;

                message.From = new MailAddress(fromAddress);




                if (resourceList != null && resourceList.Count() > 0)
                {
                    AlternateView av1 = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    foreach (ImagesResourceEmail item in resourceList)
                    {
                        LinkedResource img = new LinkedResource(item.Url);
                        img.ContentId = item.Id;

                        av1.LinkedResources.Add(img);
                    }

                    message.AlternateViews.Add(av1);
                }
                else
                {
                    message.Body = body;
                }



                // Set the to address
                if (!string.IsNullOrEmpty(toAddress))
                    AddToAddress(toAddress, message);

                if (!string.IsNullOrEmpty(toBcc))
                {
                    // Set the toBcc address
                    AddToBccAddress(toBcc, message);
                }

                // Set the email subject and body
                message.Subject = subject;

                message.IsBodyHtml = format == EmailFormat.HTML;
                if (files != null)
                    foreach (string file in files)
                        message.Attachments.Add(new Attachment(file));

                smtp.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                throw smtpEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void SendWithFile(string fromAddress, string toAddress, string subject, string body, EmailFormat format, List<ImagesResourceEmail> resourceList, List<FileReport> files)
        {
            SmtpClient smtp = GetSmtpClient();
            MailMessage message = new MailMessage();

            // Send the email
            try
            {
                // Set the from address
                if (fromAddress.IndexOf(',') >= 0)
                    fromAddress = fromAddress.Substring(0, fromAddress.IndexOf(','));

                message.Priority = MailPriority.High;

                message.From = new MailAddress(fromAddress);




                if (resourceList != null && resourceList.Count() > 0)
                {
                    AlternateView av1 = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    foreach (ImagesResourceEmail item in resourceList)
                    {
                        LinkedResource img = new LinkedResource(item.Url);
                        img.ContentId = item.Id;

                        av1.LinkedResources.Add(img);
                    }

                    message.AlternateViews.Add(av1);
                }
                else
                {
                    message.Body = body;
                }



                // Set the to address
                if (!string.IsNullOrEmpty(toAddress))
                {
                    AddToAddress(toAddress, message);
                }



                // Set the email subject and body
                message.Subject = subject;

                message.IsBodyHtml = format == EmailFormat.HTML;
                if (files != null)
                {
                    foreach (FileReport file in files)
                    {

                        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(file.ContentType);
                        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(file.MemoryStream, ct);
                        attach.ContentDisposition.FileName = file.FileName;
                        message.Attachments.Add(attach);
                    }
                }

                smtp.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                throw smtpEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private static void AddToAddress(string toAddress, MailMessage message)
        {
            string[] toAddressList = toAddress.Split(',');
            foreach (string toaddress in toAddressList)
                message.To.Add(new MailAddress(toaddress));
        }

        private static void AddToBccAddress(string toBccAddress, MailMessage message)
        {
            string[] toBccAddressList = toBccAddress.Split(',');
            foreach (string tobccaddress in toBccAddressList)
                message.Bcc.Add(new MailAddress(tobccaddress));
        }
    }



    [Serializable]
    public class EmailHelperConfig {
    
     #region Variables

        private static string configurationFile;

        #endregion

        #region Constructors, Destructor and Indexers

        /// <summary>
        /// Initialize Variables
        /// </summary>
        static EmailHelperConfig()
        {
            configurationFile = "~/web.config";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an AppSetting entry by its key.
        /// </summary>
        /// <param name="key">The AppSetting key.</param>
        /// <returns>An object with the entry value.</returns>
        private object GetAppSetting(string key)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                string errorMessage =
                    string.Format(
                        "Webmoto.Core: \"{0}\" must be provided as an appSetting within your config file. An example config declaration is <add key=\"{0}\" value=\"1\" />.",
                        key);
                throw new ConfigurationErrorsException(errorMessage);
            }

            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets an AppSetting entry by its key.
        /// </summary>
        /// <param name="key">The AppSetting key.</param>
        /// <param name="throwExceptionIfNotFound">A boolean indicating whether an exception should be thrown if the informed key is not found.</param>
        /// <returns>An object with the entry value.</returns>
        private object GetAppSetting(string key, bool throwExceptionIfNotFound)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                if (throwExceptionIfNotFound)
                {
                    string errorMessage =
                        string.Format(
                            "Webmoto.Core: \"{0}\" must be provided as an appSetting within your config file. An example config declaration is <add key=\"{0}\" value=\"1\" />.",
                            key);
                    throw new ConfigurationErrorsException(errorMessage);
                }
                else
                {
                    return null;
                }
            }

            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets the <see cref="SmtpDeliveryMethod"/>.
        /// </summary>
        /// <returns>The <see cref="SmtpDeliveryMethod"/>.</returns>
        public static SmtpDeliveryMethod GetSmtpDeliveryMethod()
        {
            SmtpDeliveryMethod method = SmtpDeliveryMethod.Network;

            System.Configuration.Configuration config;

            try
            {
                config = WebConfigurationManager.OpenWebConfiguration(configurationFile);
            }
            catch
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }

            if (null == config)
            {
                return method;
            }

            MailSettingsSectionGroup mailSettings =
                config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (null != mailSettings)
            {
                method = mailSettings.Smtp.DeliveryMethod;
            }

            return method;
        }

        public static SmtpDeliveryMethod ServiceGetSmtpDeliveryMethod()
        {
            SmtpDeliveryMethod method = SmtpDeliveryMethod.Network;

            System.Net.Configuration.SmtpSection section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            if (null != section)
            {
                method = section.DeliveryMethod;
            }

            return method;
        }

        /// <summary>
        /// Gets the Specified Pickup Directory location.
        /// </summary>
        /// <returns>The Specified Pickup Directory location.</returns>
        public static string GetSpecifiedPickupDirectory()
        {
            string pickupDirectory = string.Empty;

            System.Configuration.Configuration config;

            try
            {
                config = WebConfigurationManager.OpenWebConfiguration(configurationFile);
            }
            catch
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }

            if (null == config)
            {
                return pickupDirectory;
            }

            MailSettingsSectionGroup mailSettings =
                config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (null != mailSettings)
            {
                if (mailSettings.Smtp.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
                {
                    pickupDirectory =
                        HttpContext.Current.Server.MapPath(
                            mailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation);
                }
            }

            return pickupDirectory;
        }

        public static string GetUserName()
        {
            string username = string.Empty;

            System.Net.Configuration.SmtpSection section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            if (null != section)
            {
                username = section.Network.UserName;
            }

            return username;
        }

        public static string GetHost()
        {
            string host = string.Empty;

            System.Net.Configuration.SmtpSection section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            if (null != section)
            {
                host = section.Network.Host;
            }

            return host;
        }

        public static bool GetDefaultCredentials()
        {
            bool defaultCredentials = false;

            System.Net.Configuration.SmtpSection section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            if (null != section)
            {
                defaultCredentials = section.Network.DefaultCredentials;
            }

            return defaultCredentials;
        }

        public static string GetPassword()
        {
            string password = string.Empty;

            System.Net.Configuration.SmtpSection section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;

            if (null != string.Empty)
            {
                password = section.Network.Password;
            }

            return password;
        }
        #endregion
    
    }

    public enum EmailFormat
    {
        /// <summary>
        /// Indicates that the email will be sent in plain text format.
        /// </summary>

        PlainText,
        /// <summary>
        /// Indicates that the email will be sent in HTML format.
        /// </summary>

        HTML
    }

}
