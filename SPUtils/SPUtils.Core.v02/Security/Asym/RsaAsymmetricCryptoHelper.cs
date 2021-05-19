using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SPUtils.Core.v02.Security.Asym
{
    public class RsaKeys
    {
        public string PrivateKey;
        public string PublicKey;

        public RsaKeys(string privateKey, string publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }
    }

    public class RsaAsymmetricCryptoHelper
    {
        const int PROVIDER_RSA_FULL = 1;
        const uint DefaultKeySize = 1024;               //The more it increases the slower the performance
        const string SP_KEY_CONTAINER = "SPKeyContainer";

        private bool usingCspParameters = true;
        private bool usefOAEP = true;                   //Has a slight performance drawback but not much
        private int KeySize = (int)DefaultKeySize;

        public RsaKeys GenNewPublicAndPrivateKey(uint keySize = DefaultKeySize)
        {
            KeySize = (int)keySize;

            var rsaPrvdr = new RSACryptoServiceProvider(KeySize);

            string privateKey = rsaPrvdr.ToXmlString(true);
            string publicKey = rsaPrvdr.ToXmlString(false);

            return new RsaKeys(privateKey, publicKey);
        }

        public RsaKeys GenNewPublicAndPrivateKey2(uint keySize = DefaultKeySize)
        {
            KeySize = (int)keySize;

            var cspParams = GetDefaultCspParams();
            var rsaPrvdr = new RSACryptoServiceProvider(KeySize, cspParams);

            usingCspParameters = true;

            rsaPrvdr.PersistKeyInCsp = false;

            string privateKey = rsaPrvdr.ToXmlString(true);
            string publicKey = rsaPrvdr.ToXmlString(false);

            return new RsaKeys(privateKey, publicKey);
        }

        public string Encrypt(string publicKey, string dataStr)
        {
            RSACryptoServiceProvider rsaPrvdr = null;

            if (usingCspParameters)
                rsaPrvdr = new RSACryptoServiceProvider(KeySize, GetDefaultCspParams());
            else
                rsaPrvdr = new RSACryptoServiceProvider(KeySize);

            byte[] dataBytes = Encoding.Unicode.GetBytes(dataStr);

            byte[] encrBytes = rsaPrvdr.Encrypt(dataBytes, usefOAEP);

            return Convert.ToBase64String(encrBytes);
        }

        public string EncryptUserAuthProps(string publicKey, string uname, string passwd)
        {
            RSACryptoServiceProvider rsaPrvdr = null;

            if (usingCspParameters)
                rsaPrvdr = new RSACryptoServiceProvider(KeySize, GetDefaultCspParams());
            else
                rsaPrvdr = new RSACryptoServiceProvider(KeySize);

            rsaPrvdr.FromXmlString(publicKey);

            string dataStr = string.Format("{0};{1}", uname, passwd);

            byte[] dataBytes = Encoding.Unicode.GetBytes(dataStr);

            byte[] encrBytes = rsaPrvdr.Encrypt(dataBytes, usefOAEP);

            return Convert.ToBase64String(encrBytes);
        }

        public string Decrypt(string privateKey, string encrData)
        {
            RSACryptoServiceProvider rsaPrvdr = null;

            if (usingCspParameters)
                rsaPrvdr = new RSACryptoServiceProvider(KeySize, GetDefaultCspParams());
            else
                rsaPrvdr = new RSACryptoServiceProvider(KeySize);

            rsaPrvdr.FromXmlString(privateKey);

            string plainStr = Decrypt(rsaPrvdr, encrData);

            return plainStr;
        }

        public string[] DecryptUserAuthProps(string privateKey, string encrData)
        {
            RSACryptoServiceProvider rsaPrvdr = null;

            if (usingCspParameters)
                rsaPrvdr = new RSACryptoServiceProvider(KeySize, GetDefaultCspParams());
            else
                rsaPrvdr = new RSACryptoServiceProvider(KeySize);

            rsaPrvdr.FromXmlString(privateKey);

            string plainStr = Decrypt(rsaPrvdr, encrData);
            return plainStr.Split(';');
        }

        private string Decrypt(RSACryptoServiceProvider rsaPrvdr, string encrData)
        {
            int chunkSize = rsaPrvdr.KeySize / 8;
            string plainStr = "";
            byte[] encrBytes = Convert.FromBase64String(encrData);

            int iterationCount = encrBytes.Length / chunkSize;

            using (Stream stream = new MemoryStream(encrBytes))
            {
                int bytesRead = 0;
                int totalBytesRead = 0;
                byte[] buffer = new byte[chunkSize];

                //For each block of chunkSize decrypt data
                while ((bytesRead = stream.Read(buffer, totalBytesRead, chunkSize)) > 0)
                {
                    byte[] decryptedBytes = rsaPrvdr.Decrypt(buffer, usefOAEP);
                    totalBytesRead += bytesRead;
                    plainStr += Encoding.Unicode.GetString(decryptedBytes);

                    //If all bytes are decrypted then leave
                    if ((encrBytes.Length - totalBytesRead) == 0)
                        break;
                }
            }

            return plainStr;
        }

        private CspParameters GetDefaultCspParams()
        {
            CspParameters cspParams;
            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = SP_KEY_CONTAINER;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.Flags |= CspProviderFlags.NoPrompt;

            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            return cspParams;
        }
    }
}
