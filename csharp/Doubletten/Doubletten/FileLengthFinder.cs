namespace Doubletten
{
    public class FileLengthFinder
    {
        public long Length(string filename) {
            return new System.IO.FileInfo(filename).Length;
        }
    }
}