using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SPUtils.Core.v02.Services.Mail
{
    public class LightMailManager
    {
        private MailMessage MessagePacket = null;
        private MailClientSMTPInfo SenderSMTPInfo = null;
        private static bool CertificateIsAutoTrustValidated = false;

        public void Clear()
        {
            if (MessagePacket != null)
                MessagePacket.Dispose();

            MessagePacket = null;
            SenderSMTPInfo = null;
        }

        public void SetSenderCredentialDetails(string uname, string passwd, string smtpHost, int smtpPort = 25, SmtpDeliveryMethod delivery_method = SmtpDeliveryMethod.Network)
        {
            SenderSMTPInfo = new MailClientSMTPInfo(uname, passwd, smtpHost, smtpPort, delivery_method);
        }

        public void CreateMessage(string senderName, string senderAddress, string recipients, string subject, string mailBody, bool isHTMLFormatted)
        {
            //Generally we keep Sender and From address the same.
            MessagePacket = new MailMessage();
            MessagePacket.Sender = new MailAddress(senderAddress, senderName);
            MessagePacket.From = new MailAddress(senderAddress, senderName);
            string[] RecipientArray = recipients.Trim().Split(';');
            foreach (var recipient in RecipientArray)
            {
                if (recipient.Contains(':'))
                {
                    string[] recipietListArray = recipient.Trim().Split(':');
                    MessagePacket.To.Add(new MailAddress(recipietListArray[1].Trim(), recipietListArray[0].Trim()));
                }
                else
                {
                    MessagePacket.To.Add(new MailAddress(recipient.Trim()));
                }
            }
            MessagePacket.Subject = subject;
            MessagePacket.Body = mailBody;
            MessagePacket.IsBodyHtml = isHTMLFormatted;
        }

        public void SendMail(bool AcceptInvalidCertificates = true)
        {
            if (SenderSMTPInfo == null)
                throw new InvalidOperationException("Sender SMTP credential information needs to be set. Set using SetSenderCredentialDetails.");

            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            CertificateIsAutoTrustValidated = AcceptInvalidCertificates;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
            SmtpClient client = SenderSMTPInfo.GetSMTPClient();
            client.Send(MessagePacket);
        }

        public void AddRecipients(string Recipients)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(Recipients))
                throw new ArgumentException("Need at least one recipient");

            string[] RecipientArray = Recipients.Trim().Split(';');
            foreach (var recipient in RecipientArray)
            {
                if (recipient.Contains(':'))
                {
                    string[] recipietListArray = recipient.Trim().Split(':');
                    MessagePacket.To.Add(new MailAddress(recipietListArray[1].Trim(), recipietListArray[0].Trim()));
                }
                else
                {
                    MessagePacket.To.Add(new MailAddress(recipient.Trim()));
                }
            }
        }

        public void AddCC(string Recipients)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(Recipients))
                throw new ArgumentException("Need at least one recipient");

            string[] RecipientArray = Recipients.Trim().Split(';');
            foreach (var recipient in RecipientArray)
            {
                if (recipient.Contains(':'))
                {
                    string[] recipietListArray = recipient.Trim().Split(':');
                    MessagePacket.CC.Add(new MailAddress(recipietListArray[1].Trim(), recipietListArray[0].Trim()));
                }
                else
                {
                    MessagePacket.CC.Add(new MailAddress(recipient.Trim()));
                }
            }
        }

        public void AddBCC(string Recipients)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(Recipients))
                throw new ArgumentException("Need at least one recipient");

            string[] RecipientArray = Recipients.Trim().Split(';');
            foreach (var recipient in RecipientArray)
            {
                if (recipient.Contains(':'))
                {
                    string[] recipietListArray = recipient.Trim().Split(':');
                    MessagePacket.Bcc.Add(new MailAddress(recipietListArray[1].Trim(), recipietListArray[0].Trim()));
                }
                else
                {
                    MessagePacket.Bcc.Add(new MailAddress(recipient.Trim()));
                }
            }
        }

        public void AppendBody(string BodyString, bool OnNewLine = true)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(BodyString))
                throw new ArgumentException("Body string cannot be null or empty");

            if (OnNewLine)
                MessagePacket.Body += Environment.NewLine + BodyString;
            else
                MessagePacket.Body += " " + BodyString;
        }

        public void AddAttachment(string FilePath, string MediaType = null)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentException("File path cannot be null or empty");

            if (string.IsNullOrEmpty(MediaType))
                MessagePacket.Attachments.Add(new Attachment(FilePath));
            else
                MessagePacket.Attachments.Add(new Attachment(FilePath, MediaType));
        }

        public void AddAttachment(string FilePath, System.Net.Mime.ContentType ContentType)
        {
            if (MessagePacket == null)
                throw new InvalidOperationException("Message not prepared to send. Prepare message use CreateMessage.");

            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentException("File path cannot be null or empty");

            MessagePacket.Attachments.Add(new Attachment(FilePath.Trim(), ContentType));
        }

        private static bool ValidateServerCertificate(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (CertificateIsAutoTrustValidated)
                return true;

            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                return true;
            else
            {
                //FIXME: Find better way to ask user to validate certificate
                //if (System.Windows.Forms.MessageBox.Show("The server certificate is not valid.\nAccept?", "Certificate Validation", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                //    return true;
                //else
                //    return false;
                return true;
            }
        }
    }
}
