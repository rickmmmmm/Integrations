using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hayes_Integration_Service.APICalls
{
    class APIConfiguration
    {
        public static string HMACSHA256Header(DateTime HeaderDateTime, string EncryptionKey)
        {
            string secret = EncryptionKey;

            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            string message = (HeaderDateTime - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);

            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            string HeaderSignature = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            HeaderSignature = HeaderSignature.Replace("{", "").Replace("}", "");

            return HeaderSignature;
        }
    }
}
