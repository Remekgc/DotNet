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
    public class HousekeeperServiceTests
    {
        HousekeeperService service;
        Mock<IEmailSender> emailSender;
        Mock<IStatementGenerator> statementGenerator;
        Mock<IXtraMessageBox> messageBox;
        Housekeeper housekeeper;
        string statementFileName;
        DateTime statementDate = new System.DateTime(2017, 1, 1);

        [SetUp]
        public void SetUp()
        {
            housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                housekeeper
            }.AsQueryable());

            statementFileName = "fileName";
            statementGenerator = new Mock<IStatementGenerator>();
            statementGenerator.Setup(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate)).Returns(() => statementFileName);
            emailSender = new Mock<IEmailSender>();
            messageBox = new Mock<IXtraMessageBox>();

            service = new HousekeeperService(unitOfWork.Object, statementGenerator.Object, emailSender.Object, messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            housekeeper.Email = null;

            service.SendStatementEmails(statementDate);

            statementGenerator.Verify(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, (statementDate)), Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatement()
        {
            housekeeper.Email = " ";

            service.SendStatementEmails(statementDate);

            statementGenerator.Verify(sg => sg.SaveStatement(housekeeper.Oid, housekeeper.FullName, (statementDate)), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            service.SendStatementEmails(statementDate);
            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
        {
            statementFileName = string.Empty;

            service.SendStatementEmails(statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
            statementFileName = null;

            service.SendStatementEmails(statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhiteSpace_ShouldNotEmailTheStatement()
        {
            statementFileName = " ";

            service.SendStatementEmails(statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            emailSender.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            service.SendStatementEmails(statementDate);

            messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailSent()
        {
            emailSender.Verify(es => es.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, statementFileName, It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
