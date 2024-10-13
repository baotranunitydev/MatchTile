using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Ultils;
namespace Ultils.Encryption
{
    public static class UltilsEncryption
    {
        private static string KEY_32 => "RemoteConfigController.KEY_32";
        private static string KEY_16 => "RemoteConfigController.KEY_16";


        public static string Encrypt(string text)
        {
            try
            {
                var key = KEY_32;
                var iv = KEY_16;
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                byte[] textBytes = Encoding.UTF8.GetBytes(text);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        var txtEncrypt = Convert.ToBase64String(encryptedBytes);
                        return txtEncrypt;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't Encrypt because: {ex}");
                return null;
            }
        }

        public static string Encrypt(string text, string key32, string key16)
        {
            try
            {
                var key = key32;
                var iv = key16;
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                byte[] textBytes = Encoding.UTF8.GetBytes(text);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        var txtEncrypt = Convert.ToBase64String(encryptedBytes);
                        return txtEncrypt;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't Encrypt because: {ex}");
                return null;
            }
        }

        public static string BuffetBinaryToBase64(byte[] bytes)
        {
            try
            {
                var base64Text = Convert.ToBase64String(bytes);
                return base64Text;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't BuffetBinaryToBase64 because: {ex}");
                return null;
            }
        }

        public static string Decrypt(string encryptedText)
        {
            try
            {
                var key = KEY_32;
                var iv = KEY_16;
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        var txtDecrypt = Encoding.UTF8.GetString(decryptedBytes);
                        return txtDecrypt;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't Decrypt because: {ex}");
                return null;
            }
        }

        public static string Decrypt(string encryptedText, string key32, string key16)
        {
            try
            {
                var key = key32;
                var iv = key16;
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        var txtDecrypt = Encoding.UTF8.GetString(decryptedBytes);
                        return txtDecrypt;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't Decrypt because: {ex}");
                return null;
            }
        }

        public static string GetJsonAfterDecrypt(byte[] buffer)
        {
            try
            {
                var txt = BuffetBinaryToBase64(buffer);
                var json = Decrypt(txt);
                return json;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't GetJsonAfterDecrypt because: {ex}");
                return null;
            }
        }
    }
}
