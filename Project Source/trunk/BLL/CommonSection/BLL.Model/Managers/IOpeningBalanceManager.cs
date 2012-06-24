using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IOpeningBalanceManager
    {
        IList<OpeningBalance> GetOpeningBalances(Project project);
        bool Set(Project project, Head head, double amount);
    }
}
