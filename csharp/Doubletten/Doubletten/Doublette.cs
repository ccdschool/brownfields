using System.Collections.Generic;

namespace Doubletten
{
    public class Doublette
    {
        private readonly IList<FileInfo> fileInfos;

        public Doublette(IEnumerable<FileInfo> fileInfos) {
            this.fileInfos = new List<FileInfo>(fileInfos);
        }

        public IEnumerable<FileInfo> FileInfos {
            get { return fileInfos; }
        }
    }
}