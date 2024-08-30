using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class FizzBuzzTests
    {
        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(1, "1")]
        public void GetOutput_WhenCalled_ReturnsExpectedResult(int number, string expectedResult)
        {
            // Act
            string result = FizzBuzz.GetOutput(number);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
