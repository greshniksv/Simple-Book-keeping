using System.Security.Cryptography;
using System.Text;
using SimpleBookKeeping.Unility.Interfaces;

namespace SimpleBookKeeping.Unility
{
    public class Md5HashCalculator : IHashCalculator
    {
        private MD5 md5;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Md5HashCalculator()
        {
            md5 = MD5.Create();
        }

        public string GetHash(string data)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}