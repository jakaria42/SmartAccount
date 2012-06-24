using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.OpeningBalanceManagement
{
    public class OpeningBalanceManager : ManagerBase, IOpeningBalanceManager
    {
        private readonly IRepository<OpeningBalance> _openingBalanceRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<Parameter> _parameterRepository;

        public OpeningBalanceManager(IRepository<OpeningBalance> openingBalanceRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<Parameter> parameterRepository)
        {
            _openingBalanceRepository = openingBalanceRepository;
            _projectHeadRepository = projectHeadRepository;
            _parameterRepository = parameterRepository;
        }

        public IList<OpeningBalance> GetOpeningBalances(Project project)
        {
            string currentFianancialYear = _parameterRepository.GetSingle(p => p.Key == "CurrentFinancialYear").Value;
            return _openingBalanceRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.FinancialYear == currentFianancialYear).ToList();
        }

        public bool Set(Project project, Head head, double amount)
        {
            return true;
        }
    }
}