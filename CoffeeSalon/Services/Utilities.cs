using System.Security.Cryptography;
using System.Text;

namespace CoffeeSalon.Services
{
    public static class Utilities
    {
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (var b in data)
                {
                    sb.Append(b.ToString("x2")); // lowercase hex
                }
                return sb.ToString();
            }
        }
    }
}
