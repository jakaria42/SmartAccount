using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Entity;
using BLL.Model.Repositories;
using BLL.Model;

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

        public string GetCurrentFinancialYear()
        {
            return _parameterRepository == null ? "" : _parameterRepository.GetSingle(p => p.Key == "CurrentFinancialYear").Value;
        }

        public IList<OpeningBalance> GetOpeningBalances(Project project)
        {
            string currentFinancialYear = GetCurrentFinancialYear();
            return _openingBalanceRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.FinancialYear == currentFinancialYear).ToList();
        }

        public bool Set(Project project, Head head, double amount)
        {
            if (project == null)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
            }
            if (head == null)
            {
                InvokeManagerEvent(EventType.Error, "NoHeadSelected");
                return false;
            }
            if (amount == 0)
            {
                // TODO: This will not work right now, think through. But not a big problem.
                InvokeManagerEvent(EventType.Warning, "ZeroDepreciationProvidedForFixedAsset");
            }

            string projectName = project.Name;
            string headName = head.Name;

            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Head.Name == headName && ph.Project.Name == projectName);

            bool update = false;
            if (projectHead != null && projectHead.Budgets != null)
            {
                string currentFinancialYear = GetCurrentFinancialYear();
                OpeningBalance openingBalance = projectHead.OpeningBalances.SingleOrDefault(b => b.FinancialYear == currentFinancialYear);
                if (openingBalance != null)
                {
                    openingBalance.Balance = amount;
                    openingBalance.FinancialYear = currentFinancialYear;
                    openingBalance.Date = DateTime.Today;
                    openingBalance.IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true;

                    update = true;
                }
                else
                {
                    openingBalance = new OpeningBalance
                    {
                        Balance = amount,
                        FinancialYear = currentFinancialYear,
                        Date = DateTime.Today,
                        IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true,
                        ProjectHead = projectHead
                    };
                }

                return InsertOrUpdateOpeningBalance(openingBalance, update);
            }
            else
                return false;

            //return true;
        }

        private bool InsertOrUpdateOpeningBalance(OpeningBalance openingBalance, bool update)
        {
            if (update)
                _openingBalanceRepository.Update(openingBalance);
            else
                _openingBalanceRepository.Insert(openingBalance);

            if (_openingBalanceRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewBudgetSavedSuccessfully", Parameters = new Dictionary<string, string> { { "BudgetYear", openingBalance.Date.Year.ToString() } } });
                return true;
            }

            InvokeManagerEvent(EventType.Error, "BudgetUpdatedFailed");
            return false;
        }
    }
}