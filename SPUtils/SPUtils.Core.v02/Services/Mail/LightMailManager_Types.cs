using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SPUtils.Core.v02.Services.Mail
{
    public class UserCredentialInfo
    {
        private string User = null;
        private string Password = null;

        private System.Net.NetworkCredential _credentialInfo = null;
        public System.Net.NetworkCredential CredentialInfo
        {
            get
            {
                if (_credentialInfo == null)
                    _credentialInfo = new System.Net.NetworkCredential(User, Password);

                return _credentialInfo;
            }
        }

        public UserCredentialInfo(string uname, string passwd)
        {
            User = uname;
            Password = passwd;
        }

        public void SetNewCredential(string uname, string passwd)
        {
            User = uname;
            Password = passwd;
            _credentialInfo = null;
        }
    }

    public class MailClientSMTPInfo
    {
        private string SMTP_Host = null;
        private int SMTP_Port = -1;
        private bool SSLEnabled = true;
        private UserCredentialInfo SenderCredentials = null;
        private SmtpDeliveryMethod DeliveryMethod = SmtpDeliveryMethod.Network;

        public MailClientSMTPInfo(string uname, string passwd, string smtpHost, int smtpPort = 25, SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network)
        {
            SSLEnabled = true;
            SMTP_Host = smtpHost;
            SMTP_Port = smtpPort;
            DeliveryMethod = deliveryMethod;
            SenderCredentials = new UserCredentialInfo(uname, passwd);            
        }

        public MailClientSMTPInfo(string smtpHost, int smtpPort = 25, SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network)
        {
            SSLEnabled = false;
            SMTP_Host = smtpHost;
            SMTP_Port = smtpPort;
            DeliveryMethod = deliveryMethod;
        }

        public SmtpClient GetSMTPClient()
        {
            SmtpClient ClientObj = new SmtpClient();

            if (SSLEnabled)
            {
                ClientObj.EnableSsl = true;
                ClientObj.Host = SMTP_Host;
                ClientObj.Port = SMTP_Port;
                ClientObj.DeliveryMethod = DeliveryMethod;
                ClientObj.UseDefaultCredentials = false;
                ClientObj.Credentials = SenderCredentials.CredentialInfo;
            }
            else
            {
                ClientObj.EnableSsl = false;
                ClientObj.Host = SMTP_Host;
                ClientObj.Port = SMTP_Port;
                ClientObj.DeliveryMethod = DeliveryMethod;
                ClientObj.UseDefaultCredentials = true;
            }

            return ClientObj;
        }
    }
}
