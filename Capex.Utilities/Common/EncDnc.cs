using Capex.Models.Common;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace Capex.Utilities.Common
{
    public static class EncDnc
    {
        //public static string DecryptString(string cipherText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] buffer = Convert.FromBase64String(cipherText);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(Convert.ToString(AppSettings.Current.EncKey));
        //        aes.IV = iv;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}
        public static string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16]; // Default 16-byte IV (must match encryption IV)
            byte[] buffer = Convert.FromBase64String(cipherText);

            // Ensure the encryption key is exactly 16 bytes (for AES-128)
            string keyString = AppSettings.Current.EncKey;
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);

            if (keyBytes.Length < 16)
                keyBytes = keyBytes.Concat(new byte[16 - keyBytes.Length]).ToArray(); // pad with zero bytes
            else if (keyBytes.Length > 16)
                keyBytes = keyBytes.Take(16).ToArray(); // trim to 16 bytes

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }


        public static string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Convert.ToString(AppSettings.Current.EncKey));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public static string Decryption1(string strText)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var sr = new System.IO.StringReader(AppSettings.Current.PrivateKey);

                    // we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    // get the object back from the stream
                    var prtKey = (RSAParameters)xs.Deserialize(sr);
                    rsa.ImportParameters(prtKey);

                    // first, get our bytes back from the base64 string ...
                    var bytesCypherText = Convert.FromBase64String(strText);

                    var decryptedBytes = rsa.Decrypt(bytesCypherText, false);
                    var decryptedData = System.Text.Encoding.Unicode.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(LoggerMessage.ErrorMessage + ex);
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static (string Hash, string Salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256);
            var hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes) == storedHash;
        }
    }
}
