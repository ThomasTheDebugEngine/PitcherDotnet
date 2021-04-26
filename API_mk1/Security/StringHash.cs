using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_mk1.Security
{
    public static class StringHash
    {
        public static string GetSHA2(string input)
        {
            byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            var SBuilder = new StringBuilder();

            for(int i = 0; i < data.Length; i++)
            {
                SBuilder.Append(data[i].ToString("x2"));
            }

            return SBuilder.ToString();
        }
    }
}
