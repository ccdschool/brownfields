using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace solverdyn
{
    class DifferencesStore : IDisposable
    {
        private BinaryFormatter _bf = new BinaryFormatter();


        private readonly Dictionary<int, int[]> _difference_arrays;


        public DifferencesStore()
        {
            if (Directory.Exists("diffs")) this.Dispose();
            Directory.CreateDirectory("diffs");

            _difference_arrays = new Dictionary<int, int[]>();
        }


        public void Store_differences(int item_index, int[] differences)
        {
            _difference_arrays.Add(item_index, differences);
            return;

            var filename = Path.Combine("diffs", item_index.ToString());
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                _bf.Serialize(fs, differences);
            }
        }

        public int[] Load_differences(int item_index)
        {
            return _difference_arrays[item_index];

            var filename = Path.Combine("diffs", item_index.ToString());
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                return (int[])_bf.Deserialize(fs);
            }
        }


        public void Dispose()
        {
            Directory.Delete("diffs", true);
        }
    }
}