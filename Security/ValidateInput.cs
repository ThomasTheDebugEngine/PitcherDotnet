using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API_mk1.Security
{
    public class ValidateInput
    {
        private static readonly string AlphaNumPattern = "^[a-zA-Z0-9]*$";


        public Task<bool> IsEmptyAsync(string input)
        {
            try
            {
                return Task.Run(() => input.Length == 0);
            }
            catch(ArgumentNullException)
            {
                return Task.Run(() => false);
            }
        }

        public Task<bool> IsGoodChars(string input)
        {
            try
            {
                return Task.Run(() => Regex.IsMatch(input, AlphaNumPattern));
            }
            catch(Exception ex)
            {
                Console.Error.Write($"Regex vaildation error occured ERROR: {ex}");
                return Task.Run(() => false);
            }
        }
    }
}
