using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Factories;
using System.Windows.Input;
using System.Windows;

namespace GKS.Model.ViewModels
{
    public class CloseCurrentFinancialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IParameterManager _parameterManager;
        private readonly IOpeningBalanceManager _openingBalanceManager;

        public CloseCurrentFinancialYearModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
            _parameterManager = BLLCoreFactory.GetParameterManager();
            _openingBalanceManager = BLLCoreFactory.GetOpeningBalanceManager();

            _currentYearBalancesDataGrid = new List<CurrentYearDatagridRow>();
            AllProjects = _projectManager.GetProjects(false);
        }

        public string CurrentFinancialYear
        {
            get
            {
                return _parameterManager.Get("CurrentFinancialYear");
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
                NotifyPropertyChanged("CurrentYearBalancesDataGrid");
            }
        }

        private IList<CurrentYearDatagridRow> _currentYearBalancesDataGrid;
        public IList<CurrentYearDatagridRow> CurrentYearBalancesDataGrid
        {
            get
            {
                if (ClosingBalancesGridItems == null || ClosingBalancesGridItems.Count == 0)
                    return null;

                _currentYearBalancesDataGrid.Clear();
                for (int i = 0; i < ClosingBalancesGridItems.Count; i++)
                {
                    string headName = ClosingBalancesGridItems.Keys.ToArray()[i];
                    double balance = ClosingBalancesGridItems.Values.ToArray()[i];
                    bool isDebit = balance >= 0;
                    CurrentYearDatagridRow temp = new CurrentYearDatagridRow { HeadName = headName,
                                                                               Debit = isDebit ? balance : 0,
                                                                               Credit = isDebit ? 0 : balance*(-1)
                    };
                    _currentYearBalancesDataGrid.Add(temp);
                }

                return _currentYearBalancesDataGrid.ToList();
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

        private void CloseCurrentFinancialYear()
        {
            _openingBalanceManager.CloseCurrentAccYear();
            if (_parameterManager.Set("CurrentFinancialYear", ""))
                MessageBox.Show("Accounting year closed.\n\nPlease restart SOLVE to avoid inconsistent behavior.");
        }

        private bool hasOpenFinancialYear
        {
            get
            {
                return _parameterManager.Get("CurrentFinancialYear") != "";
            }
        }

        private void NotifyClosingBalancesGrid()
        {
            if (SelectedProject == null)
                return;

            ClosingBalancesGridItems = _openingBalanceManager.GetAllClosingBalances(SelectedProject);
        }

        private RelayCommand _closeFinancialYearClicked;
        public ICommand CloseFinancialYearClicked
        {
            get { return _closeFinancialYearClicked ?? (_closeFinancialYearClicked = new RelayCommand(p1 => this.CloseCurrentFinancialYear(), p2 => hasOpenFinancialYear)); }
        }

        private RelayCommand _oKClicked;
        public ICommand OKClicked
        {
            get { return _oKClicked ?? (_oKClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class CurrentYearDatagridRow
    {
        public string HeadName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
