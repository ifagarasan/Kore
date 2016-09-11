using System.IO;
using Kore.Code.Functional.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Functional
{
    [TestClass]
    public class ReaderFunctional
    {
        [TestMethod]
        public void ArrayReaderFunctional()
        {
            var rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            var path = Path.Combine(new string[] { rootFolder, "TestRequiredData", "readers", "array.txt" });

            int[] result;

            using (var rd = new StreamReader(path))
                result = ArrayReader.Read<int>(rd);

            var expected = new int[] { 1, 2, 3, 4, 5 };

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; ++i)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void MatrixReaderFunctional()
        {
            var rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            var path = Path.Combine(new string[] { rootFolder, "TestRequiredData", "readers", "matrix.txt" });

            int[,] result;

            using (var rd = new StreamReader(path))
                result = MatrixReader.Read<int>(rd);

            var expected = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 21, 22, 23, 24, 25 } };

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Rank, result.Rank);

            for (var i = 0; i < 5; ++i)
                for (var j = 0; j < 5; ++j)
                    Assert.AreEqual(expected[i, j], result[i, j]);
        }
    }
}
