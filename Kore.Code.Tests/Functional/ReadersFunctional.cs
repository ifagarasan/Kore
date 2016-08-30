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
            string rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string path = Path.Combine(new string[] { rootFolder, "TestData", "readers", "array.txt" });

            int[] result;

            using (StreamReader rd = new StreamReader(path))
                result = ArrayReader.Read<int>(rd);

            int[] expected = new int[] { 1, 2, 3, 4, 5 };

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Length, result.Length);

            for (int i = 0; i < expected.Length; ++i)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void MatrixReaderFunctional()
        {
            string rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string path = Path.Combine(new string[] { rootFolder, "TestData", "readers", "matrix.txt" });

            int[,] result;

            using (StreamReader rd = new StreamReader(path))
                result = MatrixReader.Read<int>(rd);

            int[,] expected = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 21, 22, 23, 24, 25 } };

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Rank, result.Rank);

            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    Assert.AreEqual(expected[i, j], result[i, j]);
        }
    }
}
