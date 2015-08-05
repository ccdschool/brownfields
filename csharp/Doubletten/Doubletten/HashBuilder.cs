using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Doubletten
{
    public class HashBuilder
    {
        private readonly FileLengthFinder fileLengthFinder;

        public HashBuilder(FileLengthFinder fileLengthFinder) {
            this.fileLengthFinder = fileLengthFinder;
        }

        public string Hash(string filename) {
            return CalculateMD5(string.Format("{0}-{1}", Path.GetFileName(filename), fileLengthFinder.Length(filename)));
        }

        private static string CalculateMD5(string input) {
            var cryptoService = new MD5CryptoServiceProvider();

            var inputBytes = Encoding.Default.GetBytes(input);
            inputBytes = cryptoService.ComputeHash(inputBytes);
            return BitConverter.ToString(inputBytes).Replace("-", "");
        }
    }
}