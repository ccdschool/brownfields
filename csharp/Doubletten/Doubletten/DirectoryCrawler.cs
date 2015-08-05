using System.Collections.Generic;
using System.IO;

namespace Doubletten
{
    public class DirectoryCrawler
    {
        private readonly HashBuilder hashBuilder;

        public DirectoryCrawler(HashBuilder hashBuilder) {
            this.hashBuilder = hashBuilder;
        }

        public IEnumerable<FileInfo> GetFileInfos(string path) {
            var fileNames = Directory.GetFiles(path);
            foreach (var fileName in fileNames) {
                var fileInfo = new System.IO.FileInfo(fileName);
                yield return new FileInfo(fileInfo.FullName, hashBuilder.Hash);
            }
            foreach (var directory in Directory.GetDirectories(path)) {
                var result = GetFileInfos(directory);
                foreach (var info in result) {
                    yield return info;
                }
            }
        }
    }
}