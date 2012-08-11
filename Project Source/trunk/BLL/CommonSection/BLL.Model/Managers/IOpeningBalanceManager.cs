using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IOpeningBalanceManager
    {
        string GetCurrentFinancialYear();
        IList<OpeningBalance> GetOpeningBalances(Project project);
        SortedDictionary<string, double> GetAllClosingBalances(Project project);
        double GetOpeningBalance(Project project, Head head);
        string GetLastFinancialYear();
        bool OpenNewAccountingYear(string year);
        bool CloseCurrentAccYear();
        bool Set(Project project, Head head, double amount);
    }
}
