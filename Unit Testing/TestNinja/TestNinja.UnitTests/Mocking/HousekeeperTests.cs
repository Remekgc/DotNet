using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using static TestNinja.Mocking.HousekeeperHelper;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class HousekeeperTests
    {
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            Housekeeper housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            HousekeeperHelper helper = new HousekeeperHelper(new Mock<IUnitOfWork>().Object, new Mock<IStatementGenerator>().Object, new Mock<IEmailSender>().Object);
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                housekeeper
            }.AsQueryable());

            var statementDate = new DateTime(2017, 1, 1);

            helper.SendStatementEmails(statementDate);
            unitOfWork.Verify(uow => uow.Query<Housekeeper>());
        }
    }
}
