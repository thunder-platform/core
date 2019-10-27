using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Thunder.Platform.Core.Helpers
{
    public static class EncryptionHelper
    {
        private static byte[] rgbKey = { 0x35, 0x43, 0x42, 0x32, 0x45, 0x46, 0x39, 0x33, 0x35, 0x32, 0x32, 0x46, 0x31, 0x41, 0x32, 0x48 };
        private static byte[] rgbIV = { 0x35, 0x43, 0x42, 0x32, 0x45, 0x46, 0x39, 0x33, 0x35, 0x32, 0x32, 0x46, 0x31, 0x41, 0x32, 0x48 };

        /// <summary>
        /// Encrypt s string.
        /// </summary>
        /// <param name="plainText">plainText.</param>
        /// <returns>encrypted.</returns>
        public static string EncryptString(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            var alg = new RijndaelManaged();
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, alg.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);

            // Close streams
            cs.Close();
            ms.Close();

            byte[] encryptedData = ms.ToArray();

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="cipherText">cipherText.</param>
        /// <returns>plainText.</returns>
        public static string DecryptString(string cipherText)
        {
            byte[] data = Convert.FromBase64String(cipherText);

            var alg = new RijndaelManaged();
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, alg.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);

            // Close streams
            cs.Close();
            ms.Close();

            byte[] decryptedData = ms.ToArray();

            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
