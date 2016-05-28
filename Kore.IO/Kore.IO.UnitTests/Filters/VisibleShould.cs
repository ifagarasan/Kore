using System.Collections.Generic;
using Kore.IO.Filters;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Filters
{
    [TestClass]
    public class VisibleShould
    {
        VisibleFileFilter _visibleFileFilter;
        Mock<IKoreFileInfo> _mockFileInfo;
        List<IKoreFileInfo> _inputFiles;

        [TestInitialize]
        public void Setup()
        {
            _mockFileInfo = new Mock<IKoreFileInfo>();
            _visibleFileFilter = new VisibleFileFilter();            
            _inputFiles = new List<IKoreFileInfo>() { _mockFileInfo.Object, _mockFileInfo.Object, _mockFileInfo.Object };
        }

        public void FilterOutHiddenFiles()
        {
            _mockFileInfo.Setup(m => m.Hidden).Returns(true);   

            List<IKoreFileInfo> output = _visibleFileFilter.Filter(_inputFiles);

            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void KeepVisibleFiles()
        {
            _mockFileInfo.Setup(m => m.Hidden).Returns(false);

            List<IKoreFileInfo> output = _visibleFileFilter.Filter(_inputFiles);

            Assert.AreEqual(_inputFiles.Count, output.Count);
        }
    }
}
