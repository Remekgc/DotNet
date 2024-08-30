using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class HtmlFormatterTests
    {
        HtmlFormatter formatter;

        [SetUp]
        public void SetUp()
        {
            formatter = new HtmlFormatter();
        }

        [TearDown]
        public void TearDown()
        {
            formatter = null;
        }


        [Test]
        public void FormatAsBold_WhenCalled_ReturnsStringEnclosedInStrongElement()
        {
            // Act
            string result = formatter.FormatAsBold("abc");

            // Assert
            //Assert.That(result, Is.EqualTo("<strong>abc</strong>"));
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain("abc"));
        }
    }
}
