using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class MathTests
    {
        private Math math;

        [SetUp]
        public void SetUp()
        {
            math = new Math();
        }

        [TearDown]
        public void TearDown()
        {
            math = null;
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(2, 1, 3)]
        public void Add_WhenCalled_ReturnsTheSumOfArguments(int a, int b, int expectedResult)
        {
            // Act
            int result = math.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnsGreaterArgument(int a, int b, int expetedResult)
        {
            // Act
            int result = math.Max(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expetedResult));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            int result = math.Max(2, 1);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("Test ignore.")]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgumentIgnored()
        {
            // Act
            int result = math.Max(2, 1);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Act
            int result = math.Max(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnsSameArgument()
        {
            // Act
            int result = math.Max(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumbersUpToLimit()
        {
            // Act
            IEnumerable<int> result = math.GetOddNumbers(5);

            // Assert
            //Assert.That(result, Is.Not.Empty);
            //Assert.That(result, Is.EqualTo(3));
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }
    }
}
