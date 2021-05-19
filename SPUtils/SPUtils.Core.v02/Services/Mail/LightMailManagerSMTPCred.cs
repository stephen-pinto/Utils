using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SPUtils.Core.v02.Services.Mail
{
    public class LightMailManagerSMTPCred
    {
        private string  User = null;
        private string  Password = null;
        private int     SMTP_Port = -1;
        private bool    SSLEnabled = true;
        private string  SMTP_Host = null;
        private SmtpDeliveryMethod DeliveryMethod = SmtpDeliveryMethod.Network;

        public string EmailId { get; set; } = null;
        public string FullNameOrAlias { get; set; } = "";

        private LightMailManagerSMTPCred()
        {}

        public LightMailManagerSMTPCred(string emailId, string uname, string passwd, string smtpHost, int smtpPort = 25, SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network)
        {
            SSLEnabled = true;
            EmailId = emailId;
            User = uname;
            Password = passwd;
            SMTP_Host = smtpHost;
            SMTP_Port = smtpPort;
            DeliveryMethod = deliveryMethod;
        }

        public LightMailManagerSMTPCred(string emailId, string smtpHost, int smtpPort = 25, SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network)
        {
            SSLEnabled = false;
            EmailId = emailId;
            SMTP_Host = smtpHost;
            SMTP_Port = smtpPort;
            DeliveryMethod = deliveryMethod;
        }

        public SmtpClient GetSMTPClient()
        {
            SmtpClient Client = new SmtpClient();

            if (SSLEnabled)
            {
                Client.Host = SMTP_Host;
                Client.Port = SMTP_Port;
                Client.EnableSsl = true;
                Client.DeliveryMethod = DeliveryMethod;
                Client.UseDefaultCredentials = false;
                NetworkCredential ClientCredential = new NetworkCredential(User, Password);
                Client.Credentials = ClientCredential;
            }
            else
            {
                Client.Host = SMTP_Host;
                Client.Port = SMTP_Port;
                Client.EnableSsl = false;
                Client.DeliveryMethod = DeliveryMethod;
                Client.UseDefaultCredentials = true;
            }

            return Client;
        }
    }    
}
