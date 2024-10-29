using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.Mocking
{
    public partial class HousekeeperService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStatementGenerator statementGenerator;
        private readonly IEmailSender emailSender;
        private readonly IXtraMessageBox xtraMessageBox;

        public HousekeeperService(IUnitOfWork unitOfWork, IStatementGenerator statementGenerator, IEmailSender emailSender, IXtraMessageBox xtraMessageBox)
        {
            this.unitOfWork = unitOfWork;
            this.statementGenerator = statementGenerator;
            this.emailSender = emailSender;
            this.xtraMessageBox = xtraMessageBox;
        }

        public void SendStatementEmails(DateTime statementDate)
        {
            System.Linq.IQueryable<Housekeeper> housekeepers = unitOfWork.Query<Housekeeper>();

            foreach (Housekeeper housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                {
                    continue;
                }

                string statementFilename = statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                {
                    continue;
                }

                string emailAddress = housekeeper.Email;
                string emailBody = housekeeper.StatementEmailBody;

                try
                {
                    emailSender.EmailFile(emailAddress, emailBody, statementFilename, string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress), MessageBoxButtons.OK);
                }
            }
        }
    }
}