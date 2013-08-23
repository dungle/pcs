using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PCSComUtils.DataAccess
{
    public static class CryptoUtil
    {
        private static readonly byte[] m_arrCryptoIV = { 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Key and IV used for RC2 cryto algorithm
        /// </summary>
        private static readonly byte[] m_arrCryptoKey = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Encrypt data with the key
        /// </summary>
        /// <param name="p_strData"></param>
        /// <returns></returns>
        public static string Encrypt(string p_strData)
        {
            var objRC2Service = new RC2CryptoServiceProvider();
            ICryptoTransform objTransform = objRC2Service.CreateEncryptor(m_arrCryptoKey, m_arrCryptoIV);
            var objMemStream = new MemoryStream();
            var objCryptoStream = new CryptoStream(objMemStream, objTransform, CryptoStreamMode.Write);
            byte[] arrData = Encoding.ASCII.GetBytes(p_strData);
            objCryptoStream.Write(arrData, 0, arrData.Length);
            objCryptoStream.FlushFinalBlock();
            byte[] arrCypher = objMemStream.ToArray();
            return GetHexString(arrCypher);
        }

        /// <summary>
        /// Decrypt data with the key
        /// </summary>
        /// <param name="p_strCypher"></param>
        /// <returns></returns>
        public static string Decrypt(string p_strCypher)
        {
            var objRC2Service = new RC2CryptoServiceProvider();
            ICryptoTransform objTransform = objRC2Service.CreateDecryptor(m_arrCryptoKey, m_arrCryptoIV);
            byte[] arrCypher = GetHexBytes(p_strCypher);
            var objMemStream = new MemoryStream(arrCypher);
            var objCryptoStream = new CryptoStream(objMemStream, objTransform, CryptoStreamMode.Read);
            var arrData = new byte[arrCypher.Length];
            objCryptoStream.Read(arrData, 0, arrData.Length);
            return Encoding.ASCII.GetString(arrData).Replace("\0", "");
        }

        /// <summary>
        /// Get byte values for hexa string
        /// </summary>
        /// <param name="p_strHexString"></param>
        /// <returns></returns>
        public static byte[] GetHexBytes(string p_strHexString)
        {
            int intDiscarded = 0;
            string strNewString = "";
            char chr;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < p_strHexString.Length; i++)
            {
                chr = p_strHexString[i];
                if (IsHexDigit(chr))
                    strNewString += chr;
                else
                    intDiscarded++;
            }
            // if odd number of characters, discard last character
            if (strNewString.Length % 2 != 0)
            {
                intDiscarded++;
                strNewString = strNewString.Substring(0, strNewString.Length - 1);
            }

            int byteLength = strNewString.Length / 2;
            var arrBytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < arrBytes.Length; i++)
            {
                hex = new String(new[] { strNewString[j], strNewString[j + 1] });
                arrBytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return arrBytes;
        }

        /// <summary>
        /// Convert byte values to hexa string
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static string GetHexString(byte[] arrBytes)
        {
            string p_strHexString = "";
            for (int i = 0; i < arrBytes.Length; i++)
            {
                p_strHexString += arrBytes[i].ToString("X2");
            }
            return p_strHexString;
        }

        /// <summary>
        /// Check if character is a hexa digit
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsHexDigit(char p_chr)
        {
            int intNumChar;
            int intNumA = Convert.ToInt32('A');
            int intNum1 = Convert.ToInt32('0');
            p_chr = Char.ToUpper(p_chr);
            intNumChar = Convert.ToInt32(p_chr);
            if (intNumChar >= intNumA && intNumChar < (intNumA + 6))
                return true;
            if (intNumChar >= intNum1 && intNumChar < (intNum1 + 10))
                return true;
            return false;
        }

        /// <summary>
        /// Convert hexa string to byte
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte HexToByte(string p_strHex)
        {
            if (p_strHex.Length > 2 || p_strHex.Length <= 0)
            {
                return 0;
            }
            byte newByte = byte.Parse(p_strHex, NumberStyles.HexNumber);
            return newByte;
        }

        /// <summary>
        /// Encrypt a string use SHA1 Encryption
        /// </summary>
        /// <param name="strPwd">A string will be encrypted</param>
        /// <returns></returns>
        public static string EncryptPassword(string strPwd)
        {
            string strHash = "";

            byte[] byteData = Encoding.Default.GetBytes(strPwd);
            SHA1 sha1 = new SHA1Managed();
            sha1.TransformFinalBlock(byteData, 0, byteData.Length);
            foreach (byte b in sha1.Hash)
                strHash += Convert.ToString(b, 16).ToUpper().PadLeft(2, '0');
            sha1.Clear();

            return strHash;
        }
    }
}