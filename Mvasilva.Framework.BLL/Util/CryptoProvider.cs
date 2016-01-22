using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL.Util
{
    public static class CryptoProvider
    {

        #region Criptografia Simetrica
        /// <summary>
        /// A chave deve possuir 16 com caracteres "1234567890123456"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCriptografiaSimetrica(this string str, string key)
        {
            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {

                provider.Mode = CipherMode.CFB;
                provider.Padding = PaddingMode.PKCS7;

                MemoryStream mStream = new MemoryStream();

                CryptoStream cs = new CryptoStream(mStream, provider.CreateEncryptor(Encoding.UTF8.GetBytes(key), new byte[] { 138, 154, 251, 188, 64, 108, 167, 121 }), CryptoStreamMode.Write);

                byte[] toEncrypt = new UTF8Encoding().GetBytes(str);

                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
                byte[] ret = mStream.ToArray();

                mStream.Close();
                cs.Close();

                str = Convert.ToBase64String(ret);

            }


            return str;
        }
        /// <summary>
        /// A chave deve possuir 16 com caracteres "1234567890123456"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDescriptografiaSimetrica(this string str, string key)
        {
            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {


                provider.Mode = CipherMode.CFB;
                provider.Padding = PaddingMode.PKCS7;
                byte[] inputEquivalent = Convert.FromBase64String(str);

                MemoryStream msDecrypt = new MemoryStream();

                CryptoStream csDecrypt = new CryptoStream(msDecrypt, provider.CreateDecryptor(Encoding.UTF8.GetBytes(key), new byte[] { 138, 154, 251, 188, 64, 108, 167, 121 }), CryptoStreamMode.Write);
                csDecrypt.Write(inputEquivalent, 0, inputEquivalent.Length);
                csDecrypt.FlushFinalBlock();

                csDecrypt.Close();

                str = Encoding.UTF8.GetString(msDecrypt.ToArray());
                msDecrypt.Close();
            }


            return str;
        }

        #endregion


        #region Criptografia Assimetrica

        public static string GetCriptografiaAssimetrica(this string str, string publicKey)
        {

            // Parameter value of 1 indicates RSA provider
            CspParameters csp = new CspParameters(1);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsa.ExportParameters(false));
                rsa.FromXmlString(publicKey);
                byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(str);

                // Encrypt our byte array. The false parameter has to do with 
                // padding (not to clear on this point but you can
                // look it up and decide which is better for your use)
                byte[] bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);
                str = Convert.ToBase64String(bytesEncrypted);

            }
            return str;
        }

        public static string GetDescriptografiaAssimetrica(this string str, string privateKey)
        {

            // Parameter value of 1 indicates RSA provider
            CspParameters csp = new CspParameters(1);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {

                rsa.ImportParameters(rsa.ExportParameters(true));
                rsa.FromXmlString(privateKey);
                byte[] valueToDecrypt = Convert.FromBase64String(str);
                byte[] plainTextValue = rsa.Decrypt(valueToDecrypt, false);
                str = Encoding.UTF8.GetString(plainTextValue);
            }
            return str;
        }

        public static string[] GenerateKeys(this string[] strRetorno)
        {
            //true = chave privada
            //false = chave publica 

            strRetorno = new string[2];
            CspParameters csp = new CspParameters(1);
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(csp))
            {
                strRetorno[0] = rsaProvider.ToXmlString(true);
                strRetorno[1] = rsaProvider.ToXmlString(false);
            }

            return strRetorno;

        }

        #endregion


        #region Hash

        public static string GetHashMD5(this string text)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                text = Convert.ToBase64String(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashSHA1(this string text)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                text = Convert.ToBase64String(sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashHMACMD5(this string text, string key)
        {
            using (HMACMD5 hMD5 = new HMACMD5() { Key = UTF8Encoding.UTF8.GetBytes(key) })
            {
                text = Convert.ToBase64String(hMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashHMACSHA1(this string text, string key)
        {
            using (HMACSHA1 hSHA1 = new HMACSHA1() { Key = UTF8Encoding.UTF8.GetBytes(key) })
            {
                text = Convert.ToBase64String(hSHA1.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashHMACSHA256(this string text, string key)
        {
            using (HMACSHA256 hSHA1 = new HMACSHA256() { Key = UTF8Encoding.UTF8.GetBytes(key) })
            {
                text = Convert.ToBase64String(hSHA1.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashHMACSHA384(this string text, string key)
        {
            using (HMACSHA384 hSHA1 = new HMACSHA384() { Key = UTF8Encoding.UTF8.GetBytes(key) })
            {
                text = Convert.ToBase64String(hSHA1.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetHashHMACSHA512(this string text, string key)
        {
            using (HMACSHA512 hSHA1 = new HMACSHA512() { Key = UTF8Encoding.UTF8.GetBytes(key) })
            {
                text = Convert.ToBase64String(hSHA1.ComputeHash(UTF8Encoding.UTF8.GetBytes(text)));
            }
            return text;
        }

        public static string GetRandomHash(this byte[] key)
        {
            using (RandomNumberGenerator r = RandomNumberGenerator.Create())
            {
                key = new byte[20];
                r.GetBytes(key);
            }
            return Convert.ToBase64String(key).Replace("+", string.Empty).Replace("=", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty).Replace("&", string.Empty);

        }
        #endregion


    }
}
