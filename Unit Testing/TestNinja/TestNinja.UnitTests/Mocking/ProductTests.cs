using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            Product product = new Product { ListPrice = 100 };

            float result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount_Bad()
        {
            Mock<ICustomer> customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsGold).Returns(true);

            Product product = new Product { ListPrice = 100 };

            float result = product.GetPrice(customer.Object);
        }
    }
}
