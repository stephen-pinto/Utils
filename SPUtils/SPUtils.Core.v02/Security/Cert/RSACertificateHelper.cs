using System.Security.Cryptography;
using System.Text;

namespace SPUtils.Core.v02.Security.Cert
{
    public class RSA_SHA265_CertificateHelper
    {
        public byte[] SignData(string dataStr)
        {
            RSACryptoServiceProvider rsaPrvdr = new RSACryptoServiceProvider();

            byte[] dataBytes = Encoding.UTF8.GetBytes(dataStr);

            byte[] signature = rsaPrvdr.SignData(dataBytes, "SHA256");

            return signature;
        }

        public bool VerifyData(byte[] signature, string dataStr)
        {
            RSACryptoServiceProvider rsaPrvdr = new RSACryptoServiceProvider();

            byte[] dataBytes = Encoding.UTF8.GetBytes(dataStr);

            if (rsaPrvdr.VerifyData(dataBytes, "SHA256", signature))
                return true;
            else
                return false;
        }
    }
}
