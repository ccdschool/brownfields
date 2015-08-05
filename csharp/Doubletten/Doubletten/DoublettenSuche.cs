using System.Collections.Generic;
using System.IO;

namespace Doubletten
{
    public class DoublettenSuche
    {
        private readonly DirectoryCrawler crawler;
        private readonly DoublettenFinder finder;

        public DoublettenSuche(DirectoryCrawler crawler, DoublettenFinder finder) {
            this.crawler = crawler;
            this.finder = finder;
        }

        public string[] Suche(string path) {
            var dateien = crawler.GetFileInfos(path);
            var doubletten = finder.GetDoubletten(dateien);

            var ergebnis = new List<string>();
            foreach (var doublette in doubletten) {
                var first = true;
                foreach (var fileInfo in doublette.FileInfos) {
                    if (first) {
                        ergebnis.Add(Path.GetFileName(fileInfo.Name));
                        first = false;
                    }
                    ergebnis.Add(string.Format(" - {0}", fileInfo.Name));
                }
            }
            return ergebnis.ToArray();
        }
    }
}