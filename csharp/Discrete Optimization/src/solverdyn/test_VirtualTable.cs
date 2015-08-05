using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace solverdyn
{
    [TestFixture]
    public class test_VirtualTable
    {
        [Test]
        public void Dispose_deletes_temp_dir()
        {
            using (new VirtualTable(3, 4)) { }
            Assert.IsFalse(Directory.Exists("_vt"));
        }

        [Test]
        public void Write_read_from_current_column()
        {
            using (var sut = new VirtualTable(1, 3))
            {
                sut[0, 0] = 1;
                Assert.AreEqual(1, sut[0,0]);

                sut[0, 2] = 3;
                Assert.AreEqual(3, sut[0,2]);
            }
        }

        [Test]
        public void Write_to_col_for_first_time()
        {
            using (var sut = new VirtualTable(2, 3))
            {
                sut[1, 0] = 1;
                Assert.AreEqual(1, sut[1,0]);
            }
        }

        [Test]
        public void Switch_between_columns()
        {
            using (var sut = new VirtualTable(2, 3))
            {
                sut[0, 0] = 1;
                sut[1, 0] = 2;
 
                Assert.AreEqual(1, sut[0, 0]);
                Assert.AreEqual(2, sut[1, 0]);
            }
        }

        [Test]
        public void Switch_with_load_store()
        {
            using (var sut = new VirtualTable(2, 3))
            {
                sut[0, 0] = 1;
                sut[1, 0] = 2;
                sut[2, 0] = 3;

                Assert.AreEqual(1, sut[0, 0]);
                Assert.AreEqual(2, sut[1, 0]);
                Assert.AreEqual(3, sut[2, 0]);
            }   
        }

    }
}
