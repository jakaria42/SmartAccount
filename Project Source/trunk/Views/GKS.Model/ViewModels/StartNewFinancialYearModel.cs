﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Model.Entity;
using System.Windows;

namespace GKS.Model.ViewModels
{
    public class StartNewFinancialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IParameterManager _parameterManager;
        private readonly IOpeningBalanceManager _openingBalanceManager;

        public StartNewFinancialYearModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _parameterManager = BLLCoreFactory.GetParameterManager();
                _openingBalanceManager = BLLCoreFactory.GetOpeningBalanceManager();

                _lastFinancialYearDatagrid = new List<LastYearDatagridRow>();

                LoadFinancialYears();
                AllProjects = _projectManager.GetProjects(false);
                LastFinancialYear = _openingBalanceManager.GetLastFinancialYear();
            }
            catch
            { }
        }

        private string _lastFinancialYear;
        public string LastFinancialYear
        {
            get
            {
                return _lastFinancialYear;
            }
            set
            {
                _lastFinancialYear = value;
                NotifyPropertyChanged("LastFinancialYear");
            }
        }

        private void LoadFinancialYears()
        {
            // We'll show budgets for current year +- 10 years, total 20 years.
            List<int> years = new List<int>();
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }

            NewFinancialYear = years;
        }

        private List<int> _newFinancialYear;
        public List<int> NewFinancialYear
        {
            get
            {
                return _newFinancialYear;
            }
            set
            {
                _newFinancialYear = value;
                NotifyPropertyChanged("NewFinancialYear");
            }
        }

        private int _selectedNewFinancialYear;
        public int SelectedNewFinancialYear
        {
            get
            {
                return _selectedNewFinancialYear;
            }
            set
            {
                _selectedNewFinancialYear = value;
                NotifyPropertyChanged("SelectedNewFinancialYear");
            }
        }

        private IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get
            {
                return _allProjects;
            }
            set
            {
                _allProjects = value;
                NotifyPropertyChanged("AllProjects");
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                NotifyPropertyChanged("SelectedProject");
                NotifyClosingBalancesGrid();
            }
        }

        private SortedDictionary<string, double> _closingBalancesGridItems;
        private SortedDictionary<string, double> ClosingBalancesGridItems
        {
            get
            {
                return _closingBalancesGridItems;
            }
            set
            {
                _closingBalancesGridItems = value;
                NotifyPropertyChanged("ClosingBalancesGridItems");
                NotifyPropertyChanged("LastFinancialYearDatagrid");
            }
        }

        private IList<LastYearDatagridRow> _lastFinancialYearDatagrid;
        public IList<LastYearDatagridRow> LastFinancialYearDatagrid
        {
            get
            {
                if (ClosingBalancesGridItems == null || ClosingBalancesGridItems.Count == 0)
                    return null;

                _lastFinancialYearDatagrid.Clear();
                for (int i = 0; i < ClosingBalancesGridItems.Count; i++)
                {
                    string headName = ClosingBalancesGridItems.Keys.ToArray()[i];
                    double balance = ClosingBalancesGridItems.Values.ToArray()[i];
                    //bool isDebit = balance >= 0;
                    LastYearDatagridRow temp = new LastYearDatagridRow
                    {
                        HeadName = headName,
                        Amount = balance
                        //Debit = isDebit ? balance : 0,
                        //Credit = isDebit ? 0 : balance * (-1)
                    };
                    _lastFinancialYearDatagrid.Add(temp);
                }

                return _lastFinancialYearDatagrid.ToList();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            private set
            {
                _colorCode = value;
                NotifyPropertyChanged("ColorCode");
            }
        }

        private void OpenNewFinancialYear()
        {
            string newFinancialYear = SelectedNewFinancialYear.ToString();
            if (!_openingBalanceManager.OpenNewAccountingYear(newFinancialYear))
                return;

            if (_parameterManager.Set("CurrentFinancialYear", newFinancialYear))
                MessageBox.Show("New accounting year opened for the year " + newFinancialYear + ".\n\nPlease restart SOLVE to avoid inconsistent behavior.", "SOLVE");
        }

        private bool hasOpenFinancialYear
        {
            get
            {
                return _parameterManager.Get("CurrentFinancialYear") != "";
            }
        }

        public void Reset()
        {
            AllProjects = _projectManager.GetProjects(false);
        }

        private void NotifyClosingBalancesGrid()
        {
            if (SelectedProject == null)
                return;

            if (LastFinancialYear != "")
                ClosingBalancesGridItems = _openingBalanceManager.GetClosingBalancesForLastYear(SelectedProject, LastFinancialYear);
        }

        private void ImportBalancesFromLastYear()
        {
            MessageBoxResult result = MessageBox.Show("All the existing opening balances for the current accounting year will be overwritten. Are you sure you wish to continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _openingBalanceManager.ImportBalancesFromLastYear();
            }
        }

        private RelayCommand _openNewFinancialYearClicked;
        public ICommand OpenNewFinancialYearClicked
        {
            get { return _openNewFinancialYearClicked ?? (_openNewFinancialYearClicked = new RelayCommand(p1 => this.OpenNewFinancialYear(), p2 => !hasOpenFinancialYear)); }
        }

        private RelayCommand _editOpeningBalanceClicked;
        public ICommand EditOpeningBalanceClicked
        {
            get { return _editOpeningBalanceClicked ?? (_editOpeningBalanceClicked = new RelayCommand(p1 => this.InvokeOnFinish(), p2 => hasOpenFinancialYear)); }
        }

        private RelayCommand _importToCurrrentYearClicked;
        public ICommand ImportToCurrrentYearClicked
        {
            get { return _importToCurrrentYearClicked ?? (_importToCurrrentYearClicked = new RelayCommand(p1 => this.ImportBalancesFromLastYear(), p2 => hasOpenFinancialYear)); }
        }
    }

    public class LastYearDatagridRow
    {
        public string HeadName { get; set; }
        public double Amount { get; set; }
    }
}
