using System;

namespace Doubletten
{
    public class FileInfo
    {
        private readonly string hash;
        private readonly string name;

        public FileInfo(string filename, Func<string, string> hashBuilder) {
            name = filename;
            hash = hashBuilder(filename);
        }

        public string Hash {
            get { return hash; }
        }

        public string Name {
            get { return name; }
        }
    }
}