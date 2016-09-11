using Kore.Input.Keys;
using NUnit.Framework;

namespace Kore.Input.Tests
{
    [TestFixture]
    public class KeyMapShould
    {
        private KeyMap<TestableResource> _keyMap;
        private TestableResource _resource;
        private Key _key;

        [SetUp]
        public void Setup()
        {
            _key = new Key('A');
            _resource = new TestableResource();
            _keyMap = new KeyMap<TestableResource>();
        }

        [TestCase]
        public void AddKey()
        {
            _keyMap.Add(_key, _resource);

            Assert.That(_keyMap.Count, Is.EqualTo(1));
            Assert.True(_keyMap.Contains(_key));
        }
    }
}
