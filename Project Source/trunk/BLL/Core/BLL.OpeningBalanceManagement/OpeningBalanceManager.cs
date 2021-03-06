﻿using System;
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
        private readonly IRepository<Record> _recordRepository;

        public OpeningBalanceManager(IRepository<OpeningBalance> openingBalanceRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<Parameter> parameterRepository, IRepository<Record> recordRepository)
        {
            _openingBalanceRepository = openingBalanceRepository;
            _projectHeadRepository = projectHeadRepository;
            _parameterRepository = parameterRepository;
            _recordRepository = recordRepository;
        }

        public string GetCurrentFinancialYear()
        {
            return _parameterRepository == null ? "" : _parameterRepository.GetSingle(p => p.Key == "CurrentFinancialYear").Value;
            //return _parameterRepository == null ? "" : _parameterManager.Get("CurrentFinancialYear");
        }

        public IList<OpeningBalance> GetOpeningBalances(Project project)
        {
            string currentFinancialYear = GetCurrentFinancialYear();
            return _openingBalanceRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.FinancialYear == currentFinancialYear).ToList();
        }

        // TODO: Refactor -- Code cloning with the overload below.
        public double GetOpeningBalance(Project project, Head head)
        {
            if (!head.HeadType.Equals("Capital", StringComparison.OrdinalIgnoreCase))
                return 0.0;

            string currentFinancialYear = GetCurrentFinancialYear();
            OpeningBalance openingBalance = _openingBalanceRepository.GetSingle(r => r.ProjectHead.Project.ID == project.ID
                                                                                  && r.ProjectHead.Head.ID == head.ID
                                                                                  && r.FinancialYear == currentFinancialYear && r.Description.Equals("opening", StringComparison.OrdinalIgnoreCase));

            if (openingBalance == null)
                return 0.0;
            else
                return openingBalance.Balance;
        }

        // This should be called with a capital type head name.
        private double GetOpeningBalance(string projectName, string headName, string year)
        {
            OpeningBalance openingBalance = _openingBalanceRepository.GetSingle(r => r.ProjectHead.Project.Name == projectName
                                                                                  && r.ProjectHead.Head.Name == headName
                                                                                  && r.FinancialYear == year && r.Description.Equals("opening", StringComparison.OrdinalIgnoreCase));

            if (openingBalance == null)
                return 0.0;
            else
                return openingBalance.Balance;
        }

        public SortedDictionary<string, double> GetAllClosingBalances(Project project)
        {
            string currentFinancialYear = GetCurrentFinancialYear();
            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.FinancialYear == currentFinancialYear).ToList();
            //records = records.Where(r => r.LedgerType.Equals("LedgerBook", StringComparison.OrdinalIgnoreCase)).ToList();

            SortedDictionary<string, double> closingBalances = new SortedDictionary<string, double>();
            foreach (Record record in records)
            {
                string headName;
                if (record.LedgerType.Equals("CashBook", StringComparison.OrdinalIgnoreCase))
                    headName = "Cash Book";
                else if (record.LedgerType.Equals("BankBook", StringComparison.OrdinalIgnoreCase))
                    headName = "Bank Book";
                else
                    headName = record.ProjectHead.Head.Name;

                double cumulativeBalance = record.Debit - record.Credit;
                double currentBalance;
                if (closingBalances.TryGetValue(headName, out currentBalance))
                {
                    closingBalances.Remove(headName);
                    cumulativeBalance += currentBalance;
                }
                else
                {
                    double openingBalance = GetOpeningBalance(project.Name, headName, currentFinancialYear);
                    cumulativeBalance = openingBalance + cumulativeBalance;
                }

                closingBalances.Add(headName, cumulativeBalance);
            }

            return closingBalances;
        }

        // TODO: Refactor -- Code cloning with the previous function.
        public SortedDictionary<string, double> GetClosingBalancesForLastYear(Project project, string lastYear)
        {
            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.FinancialYear == lastYear).ToList();
            records = records.Where(r => r.ProjectHead.Head.HeadType.Equals("Capital", StringComparison.OrdinalIgnoreCase)
                /*|| r.LedgerType.Equals("CashBook", StringComparison.OrdinalIgnoreCase)
                || r.LedgerType.Equals("BankBook", StringComparison.OrdinalIgnoreCase)*/).ToList();

            SortedDictionary<string, double> closingBalances = new SortedDictionary<string, double>();
            foreach (Record record in records)
            {
                string headName;
                if (record.LedgerType.Equals("CashBook", StringComparison.OrdinalIgnoreCase))
                    headName = "Cash Book";
                else if (record.LedgerType.Equals("BankBook", StringComparison.OrdinalIgnoreCase))
                    headName = "Bank Book";
                else
                    headName = record.ProjectHead.Head.Name;

                double cumulativeBalance = record.Debit - record.Credit;

                double currentBalance;
                if (closingBalances.TryGetValue(headName, out currentBalance))
                {
                    closingBalances.Remove(headName);
                    cumulativeBalance += currentBalance;
                }
                else
                {
                    double openingBalance = GetOpeningBalance(project.Name, headName, lastYear);
                    cumulativeBalance = openingBalance + cumulativeBalance;
                }

                closingBalances.Add(headName, cumulativeBalance);
            }

            return closingBalances;
        }

        public string GetLastFinancialYear()
        {
            IList<OpeningBalance> allClosingBalances = _openingBalanceRepository.Get(ob => ob.Description.Equals("closing", StringComparison.OrdinalIgnoreCase)).ToList();

            if (allClosingBalances == null || allClosingBalances.Count == 0)
                return "";

            return allClosingBalances.Max(cb => cb.FinancialYear);
        }

        public bool OpenNewAccountingYear(string year)
        {
            IList<OpeningBalance> allClosingBalances = _openingBalanceRepository.Get(ob => ob.FinancialYear == year && ob.Description.Equals("closing", StringComparison.OrdinalIgnoreCase)).ToList();

            if (allClosingBalances == null)
                return true;

            if (allClosingBalances.Count > 0)
            {
                foreach (OpeningBalance closingBalance in allClosingBalances)
                {
                    _openingBalanceRepository.Delete(closingBalance);
                }

                if (_openingBalanceRepository.Save() <= 0)
                    return false;
            }

            return true;
        }

        public bool CloseCurrentAccYear()
        {
            string currentFinancialYear = GetCurrentFinancialYear();
            IList<ProjectHead> allProjectHeads = _projectHeadRepository.GetAll().ToList();
            //IList<Project> allProjects = (IList<Project>)_projectHeadRepository.GetAll().Distinct();
            IList<string> processed = new List<string>();
            foreach (ProjectHead projectHead in allProjectHeads)
            {
                string projectName = projectHead.Project.Name;
                if (processed.Contains(projectName))
                    continue;

                processed.Add(projectName);

                if (projectHead.Head.HeadType.Equals("Capital", StringComparison.OrdinalIgnoreCase))
                {
                    SortedDictionary<string, double> closingBalances = GetAllClosingBalances(projectHead.Project);
                    for (int i = 0; i < closingBalances.Count; i++)
                    {
                        string headName = closingBalances.Keys.ToArray()[i];
                        ProjectHead actualProjectHead = _projectHeadRepository.GetSingle(ph => ph.Project.Name == projectName && ph.Head.Name == headName);
                        OpeningBalance closingBalance = new OpeningBalance
                        {
                            Balance = closingBalances.Values.ToArray()[i],
                            FinancialYear = currentFinancialYear,
                            Date = DateTime.Today,
                            IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true,
                            Description = "closing",
                            ProjectHead = actualProjectHead
                        };

                        if (!InsertOrUpdateOpeningBalance(closingBalance, false))
                            return false;
                    }
                }
            }

            //string currentFinancialYear = GetCurrentFinancialYear();
            //IList<OpeningBalance> allOpeningBalances = _openingBalanceRepository.GetAll().ToList();
            //allOpeningBalances = allOpeningBalances.Where(ob => ob.FinancialYear == currentFinancialYear && ob.Description.Equals("opening", StringComparison.OrdinalIgnoreCase)).ToList();
            //foreach (OpeningBalance openingBalance in allOpeningBalances)
            //{
            //    // TODO: closing balance is wrong here. It should be calculated from records.
            //    OpeningBalance closingBalance = new OpeningBalance
            //    {
            //        Balance = openingBalance.Balance,
            //        FinancialYear = currentFinancialYear,
            //        Date = DateTime.Today,
            //        IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true,
            //        Description = "closing",
            //        ProjectHead = openingBalance.ProjectHead
            //    };

            //    if (!InsertOrUpdateOpeningBalance(closingBalance, false))
            //        return false;
            //}

            return true;
        }

        public bool ImportBalancesFromLastYear()
        {
            string currentFinancialYear = GetCurrentFinancialYear();
            string lastAccYear = GetLastFinancialYear();
            IList<OpeningBalance> allClosingBalances = _openingBalanceRepository.Get(ob => ob.FinancialYear == lastAccYear && ob.Description.Equals("closing", StringComparison.OrdinalIgnoreCase)).ToList();

            if (allClosingBalances == null || allClosingBalances.Count == 0)
                return false;

            foreach (OpeningBalance closingBalance in allClosingBalances)
            {
                OpeningBalance openingBalance = new OpeningBalance
                {
                    Balance = closingBalance.Balance,
                    FinancialYear = currentFinancialYear,
                    Date = DateTime.Today,
                    IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true,
                    Description = "opening",
                    ProjectHead = closingBalance.ProjectHead
                };

                if (!InsertOrUpdateOpeningBalance(openingBalance, false))
                    return false;
            }

            return true;
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
            //if (projectHead != null && projectHead.Budgets != null)
            if (projectHead != null && projectHead.OpeningBalances != null)
            {
                string currentFinancialYear = GetCurrentFinancialYear();
                OpeningBalance openingBalance = projectHead.OpeningBalances.SingleOrDefault(b => b.FinancialYear == currentFinancialYear);
                if (openingBalance != null)
                {
                    openingBalance.Balance = amount;
                    openingBalance.FinancialYear = currentFinancialYear;
                    openingBalance.Date = DateTime.Today;
                    openingBalance.IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true;
                    openingBalance.Description = "opening";

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
                        Description = "opening",
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