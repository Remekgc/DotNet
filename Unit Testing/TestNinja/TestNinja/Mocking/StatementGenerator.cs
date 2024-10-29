using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.Mocking
{
    public class StatementGenerator : IStatementGenerator
    {
        public string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate)
        {
            HousekeeperStatementReport report = new HousekeeperStatementReport(housekeeperOid, statementDate);

            if (!report.HasData)
            {
                return string.Empty;
            }

            report.CreateDocument();

            string filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeperName));

            report.ExportToPdf(filename);

            return filename;
        }
    }
}
