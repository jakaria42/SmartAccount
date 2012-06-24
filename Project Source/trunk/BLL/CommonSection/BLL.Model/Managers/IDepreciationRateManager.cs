using BLL.Model.Entity;
using System.Collections.Generic;
namespace BLL.Model.Managers
{
    public interface IDepreciationRateManager
    {
        double GetDepreciationRate(string projectName, string headName);
        IList<DepreciationRate> GetDepreciationRates(Project project);
        bool Set(Project project, Head head, double rate);
    }
}
