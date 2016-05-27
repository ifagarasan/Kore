using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.IO.Filters;
using Kore.IO.Util;
using Moq;

namespace Kore.IO.UnitTests.Retrievers
{
    [TestClass]
    public class VisibleShould
    {
        Mock<IFileInfoProvider> _mockFileInfoProvider;
        VisibleFileFilter _visibleFileFilter;
        Mock<IKoreFileInfo> _mockFileInfo;

        [TestInitialize]
        public void Setup()
        {
            _mockFileInfoProvider = new Mock<IFileInfoProvider>();
            _visibleFileFilter = new VisibleFileFilter(_mockFileInfoProvider.Object);
            _mockFileInfo = new Mock<IKoreFileInfo>();
        }

        public void FilterOutHiddenFiles()
        {
            _mockFileInfo.Setup(m => m.Hidden).Returns(true);

            _mockFileInfoProvider.Setup(m => m.GetFileInfo(It.IsAny<string>())).Returns(_mockFileInfo.Object);

            List<string> inputFiles = new List<string>() { "a", "b", "c" };

            List<string> output = _visibleFileFilter.Filter(inputFiles);

            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void KeepVisibleFiles()
        {
            _mockFileInfo.Setup(m => m.Hidden).Returns(false);

            _mockFileInfoProvider.Setup(m => m.GetFileInfo(It.IsAny<string>())).Returns(_mockFileInfo.Object);

            List<string> inputFiles = new List<string>() { "a", "b", "c" };

            List<string> output = _visibleFileFilter.Filter(inputFiles);

            Assert.AreEqual(inputFiles.Count, output.Count);
        }
    }
}
