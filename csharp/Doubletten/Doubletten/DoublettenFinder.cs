using System.Collections.Generic;
using System.Linq;

namespace Doubletten
{
    public class DoublettenFinder
    {
        public IEnumerable<Doublette> GetDoubletten(IEnumerable<FileInfo> fileInfos) {
            var query = from fileInfo in fileInfos
                        group fileInfo by fileInfo.Hash
                            into fileGroup
                            where fileGroup.Count() > 1
                            select (Doublette)new Doublette(fileGroup.ToArray());

            return query;
        }
    }
}