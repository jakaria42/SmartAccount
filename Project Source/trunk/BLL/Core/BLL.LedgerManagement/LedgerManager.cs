﻿using System.Collections.Generic;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using BLL.Model;
using System;
using System.Linq;
using BLL.ParameterManagement;

namespace BLL.LedgerManagement
{
    public class LedgerManager : ManagerBase, ILedgerManager
    {
        private readonly ILedgerRepository _ledgerRepository;
        private readonly IParameterManager _parameterManager;
        public LedgerManager(ILedgerRepository ledgerRepository, IParameterRepository parameterRepository)
        {
            _ledgerRepository = ledgerRepository;
            _parameterManager = new ParameterManager(parameterRepository);
            //_parameterManager = BLLCoreFactory.GetParameterManager(); //TODO: using BLLCoreFactory creates circular reference
            LedgerEndDate = DateTime.Now;
        }

        public override string ModuleName
        {
            get { return "Ledger"; }
        }

        public DateTime LedgerEndDate { get; set; }

        public bool Validate(Project project, Head head, bool showAllAdvance)
        {
            if (project == null)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
            }

            if (!showAllAdvance && head == null)
            {
                InvokeManagerEvent(EventType.Error, "NoHeadSelected");
                return false;
            }
            return true;
        }

        public IList<Ledger> GetLedgerBook(int projectId, int headId, bool isCashBankShown = false)
        {
            DateTime financialYearStartDate = _parameterManager.GetFinancialYearStartDate();

            return
                _ledgerRepository.GetLedger(projectId, headId).OrderBy(l => l.Date).Where(
                    l => GetDateAt12AM(l.Date) >= financialYearStartDate && GetDateAt12AM(l.Date) <= LedgerEndDate).
                    ToList();
        }

        public IList<Ledger> GetAllAdvance(int projectId)
        {
            return _ledgerRepository.GetLedger(projectId);
        }

        public DateTime GetDateAt12AM(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}