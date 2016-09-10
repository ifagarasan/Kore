using System.Windows.Forms;
using Kore.Input.Keys;
using NUnit.Framework;

namespace Kore.Input.Tests
{
    [TestFixture]
    public class KeyBuilderShould
    {
        private Key _a;
        private IKeyBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _a = new Key('A');
            _builder = new KeyBuilder();
        }

        [TestCase]
        public void BuildKeyBasedOnKeyCode()
        {
            Assert.That(_builder.Build(new KeyEventArgs(System.Windows.Forms.Keys.A)), Is.EqualTo(_a));
        }

        [TestCase]
        public void BuildKeyUp()
        {
            var keyUp = _builder.BuildKeyUp(new KeyEventArgs(System.Windows.Forms.Keys.A));

            Assert.IsInstanceOf<KeyUp>(keyUp);
            Assert.That(keyUp, Is.EqualTo(_a));
        }

        [TestCase]
        public void BuildKeyDown()
        {
            var keyDown = _builder.BuildKeyDown(new KeyEventArgs(System.Windows.Forms.Keys.A));

            Assert.IsInstanceOf<KeyDown>(keyDown);
            Assert.That(keyDown, Is.EqualTo(_a));
        }

    }
}
