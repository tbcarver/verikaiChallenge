using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace verikai
{
    public class AesFileEncryptor
    {
        private byte[] key;
        private byte[] iv;

        public AesFileEncryptor(string base64Key, string base64IV)
        {
            key = Convert.FromBase64String(base64Key);
            iv = Convert.FromBase64String(base64IV);
        }

        public void EncryptFile(string sourcePathName, string targetPathName)
        {
            string text = File.ReadAllText(sourcePathName);

            using (Aes symmetricAlgorithm = Aes.Create())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.IV = iv;
                symmetricAlgorithm.Mode = CipherMode.CBC;
                symmetricAlgorithm.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor();

                byte[] textBytes = Encoding.Unicode.GetBytes(text);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                string encryptedText = Convert.ToBase64String(encryptedBytes);

                File.WriteAllText(targetPathName, encryptedText);
            }
        }
    }
}
