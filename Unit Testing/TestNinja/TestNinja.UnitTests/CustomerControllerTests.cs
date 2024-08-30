using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class CustomerControllerTests
    {
        CustomerController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new CustomerController();
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            controller = new CustomerController();
            ActionResult result = controller.GetCustomer(0);

            // NotFound
            Assert.That(result, Is.TypeOf<NotFound>());

            // NotFound or one of its derivatives
            //Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            controller = new CustomerController();
            ActionResult result = controller.GetCustomer(1);

            // Ok
            Assert.That(result, Is.TypeOf<Ok>());

            // Ok or one of its derivatives
            //Assert.That(result, Is.InstanceOf<Ok>());
        }
    }
}
