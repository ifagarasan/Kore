using Kore.Input.Keyboard;
using Kore.Input.Keys;
using NUnit.Framework;

namespace Kore.Input.Tests.Keyboard
{
    [TestFixture]
    public class KeyboardTrackerShould
    {
        private KeyboardTracker _keyboardTracker;
        private Key _key;
        private Key _notTracked;

        [SetUp]
        public void Setup()
        {
            _keyboardTracker = new KeyboardTracker();
            _key = new Key('A');
            _notTracked = new Key('X');
        }

        [TestCase]
        public void MarkKeyAsPressed()
        {
            _keyboardTracker.Press(_key);

            Assert.IsTrue(_keyboardTracker.Pressed(_key));
        }

        [TestCase]
        public void MarkKeyAsReleased()
        {
            _keyboardTracker.Press(_key);
            _keyboardTracker.Release(_key);

            Assert.IsFalse(_keyboardTracker.Pressed(_key));
        }

        [TestCase]
        public void ReturnReleasedByDefault()
        {
            Assert.IsFalse(_keyboardTracker.Pressed(_notTracked));
        }
    }
}
