﻿using System;
using BLL.BudgetManagement;
using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.ParameterManagement;
using BLL.ProjectManagement;
using BLL.VoucherManagement;
using BLL.FixedAssetSchedule;
using BLL.BudgetManagement;
using BLL.OpeningBalanceManagement;

namespace BLL.Factories
{
    public class BLLCoreFactory
    {
        public static IRepository<Budget> BudgetRepository { get; set; }
        public static IRepository<DepreciationRate> DepreciationRateRepository { get; set; }
        public static IRepository<Head> HeadRepository { get; set; }
        public static IRepository<Project> ProjectRepository { get; set; }
        public static IRepository<ProjectHead> ProjectHeadRepository { get; set; }
        public static IRepository<Record> RecordRepository { get; set; }
        public static IRepository<Parameter> ParameterRepository { get; set; }
        public static IRepository<FixedAsset> FixedAssetRepository { get; set; }
        public static IRepository<BankBook> BankBookRepository { get; set; }
        public static IRepository<OpeningBalance> OpeningBalanceRepository { get; set; }

        public static ILedgerManager GetLedgerManager()
        {
            if (RecordRepository != null && ProjectRepository != null)
            {
                LedgerManager ledgerManager = new LedgerManager(RecordRepository, GetParameterManager());
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IVoucherManager GetVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null)
            {
                VoucherManager voucherManager = new VoucherManager(RecordRepository);
                voucherManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return voucherManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IParameterManager GetParameterManager()
        {
            if (ParameterRepository != null)
            {
                ParameterManager parameterManager = new ParameterManager(ParameterRepository);
                //parameterManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //parameterManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return parameterManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IMassVoucherManager GetMassVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null && HeadRepository != null)
            {
                MassVoucherManager massVoucherManager = new MassVoucherManager(RecordRepository, ProjectRepository, HeadRepository, ProjectHeadRepository, FixedAssetRepository, BankBookRepository);
                massVoucherManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //massVoucherManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return massVoucherManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IRecordManager GetRecordManager()
        {
            if (RecordRepository != null)
            {
                RecordManager recordManager = new RecordManager(RecordRepository);
                recordManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //recordManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return recordManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IHeadManager GetHeadManager()
        {
            if (ProjectHeadRepository != null && HeadRepository != null)
            {
                HeadManager headManager = new HeadManager(ProjectHeadRepository, HeadRepository);
                headManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //headManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return headManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IProjectManager GetProjectManager()
        {
            if (ProjectRepository != null && HeadRepository != null && ProjectHeadRepository != null && RecordRepository != null && OpeningBalanceRepository != null && ParameterRepository != null)
            {
                ProjectManager projectManager = new ProjectManager(ProjectRepository, HeadRepository, ProjectHeadRepository, RecordRepository, OpeningBalanceRepository, ParameterRepository);
                projectManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //projectManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return projectManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IDepreciationRateManager GetDepreciationRateManager()
        {
            if (ProjectHeadRepository != null && DepreciationRateRepository != null)
            {
                DepreciationRateManager depreciationRateManager = new DepreciationRateManager(DepreciationRateRepository, ProjectHeadRepository);
                depreciationRateManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //depreciationRateManager.ManagerEvent += LogService.Instance.ManagerEventHandler;
                return depreciationRateManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IBudgetManager GetBudgetManager()
        {
            //<<<<<<< HEAD
            if (BudgetRepository != null && ProjectRepository != null && ProjectHeadRepository != null)
            {
                BudgetManager budgetManager = new BudgetManager(BudgetRepository, ProjectRepository, ProjectHeadRepository);
                budgetManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //budgetManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return budgetManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IOpeningBalanceManager GetOpeningBalanceManager()
        {
            if (OpeningBalanceRepository != null && ProjectRepository != null && ProjectHeadRepository != null)
            {
                OpeningBalanceManager openingBalanceManager = new OpeningBalanceManager(OpeningBalanceRepository, ProjectHeadRepository, ParameterRepository);
                openingBalanceManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //openingBalanceManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return openingBalanceManager;
            }

            throw new ArgumentNullException("message");
        }
        
        //=======
        //            if (ProjectHeadRepository != null && BudgetRepository != null)
        //            {
        //                BudgetManager budgetManager = new BudgetManager(BudgetRepository, ProjectRepository, ProjectHeadRepository);
        //                budgetManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
        //                //budgetManager.ManagerEvent += LogService.Instance.ManagerEventHandler;
        //                return budgetManager;
        //            }

        //            throw new ArgumentNullException("message");
        //        }        
        //>>>>>>> githubJakaria42/master
    }
}
