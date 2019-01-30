using System;
using System.Text;
using System.Security.Cryptography;

namespace ApiGTT.Helpers{

    class Encrypt{
        public static string Hash(string value){
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(value));
            for(int i= 0; i<bytes.Length;i++){
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}