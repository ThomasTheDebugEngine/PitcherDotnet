using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;

namespace API_mk1.Security
{
    public class SecurityUtils : ISecurityUtils
    {
        public Task<string> getGuidAsync()
        {
            return Task.Run(() => Guid.NewGuid().ToString());
        }

        public Task<long> getUnixSecondsAsync()
        {
            return Task.Run(() => DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        public object AssignDifferential(object subset, object superset)
        {
            PropertyInfo[] subsetPropsArr = subset.GetType().GetProperties();
            PropertyInfo[] supersetPropsArr = superset.GetType().GetProperties();

            IEnumerable<PropertyInfo> PropOverlap = subsetPropsArr.Except(supersetPropsArr);

            foreach(PropertyInfo prop in PropOverlap) // set the properties in the subset dynamically
            {
                var value = prop.GetValue(subset);
                PropertyInfo propertyInfo = superset.GetType().GetProperty(prop.Name);
                propertyInfo.SetValue(superset, value);
            }

            return superset;
        }

        public string GetSHA256(string inputData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
            StringBuilder builder = new();

            for(int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
