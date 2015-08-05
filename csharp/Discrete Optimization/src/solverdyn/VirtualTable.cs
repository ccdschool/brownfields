using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace solverdyn
{
    internal class VirtualTable : IDisposable
    {
        private readonly string _path;
        private readonly int _column_count;
        private readonly int _row_count;

        private int _curr_col_index;
        private int[] _curr_col_data;

        private int _cache_col_index;
        private int[] _cache_col_data;


        private readonly BinaryFormatter _bf = new BinaryFormatter();


        public VirtualTable(int column_count, int row_count) : this("_vt", column_count, row_count) {}
        public VirtualTable(string path, int column_count, int row_count) 
        {
            _path = path;
            _column_count = column_count;
            _row_count = row_count;
            
            Directory.CreateDirectory(path);

            _curr_col_data = new int[row_count];
            _curr_col_index = 0;
        }


        public int this[int col_index, int row_index]
        {
            get
            {
                if (col_index != _curr_col_index)
                    Swap_in(col_index);
                return _curr_col_data[row_index];
            }

            set
            {
                if (col_index != _curr_col_index)
                    Swap_in(col_index);
                _curr_col_data[row_index] = value;
            }
        }


        private void Swap_in(int col_index)
        {
            if (_cache_col_data == null)
                Load_cache(col_index);
            else if (_cache_col_index != col_index)
            {
                Store_cache();
                Load_cache(col_index);
            }
            Exchange_with_cache();
        }


        private void Load_cache(int col_index)
        {
            var filename = Build_cache_filename(col_index);
            if (File.Exists(filename))
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    _cache_col_data = (int[]) _bf.Deserialize(fs);
                    _cache_col_index = col_index;
                }
            else
            {
                _cache_col_data = new int[_row_count];
                _cache_col_index = col_index;
            }
        }


        private void Store_cache()
        {
            using (var fs = new FileStream(Build_cache_filename(_cache_col_index), FileMode.Create))
            {
                _bf.Serialize(fs, _cache_col_data);
            }
        }


        private string Build_cache_filename(int col_index)
        {
            return Path.Combine(_path, col_index.ToString());
        }

        private void Exchange_with_cache()
        {
            var tmp_data = _cache_col_data;
            _cache_col_data = _curr_col_data;
            _curr_col_data = tmp_data;

            var tmp_index = _cache_col_index;
            _cache_col_index = _curr_col_index;
            _curr_col_index = tmp_index;
        }


        public void Dispose()
        {
            Directory.Delete(_path, true);
        }
    }
}