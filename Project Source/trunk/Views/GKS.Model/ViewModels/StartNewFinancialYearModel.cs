using System;
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
            }
            catch
            { }
        }

        string _lastFinancialYear;
        string LastFinancialYear
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
            }
        }

        IList<LastYearDatagridRow> _lastFinancialYearDatagrid;
        IList<LastYearDatagridRow> LastFinancialYearDatagrid
        {
            get
            {
                LastYearDatagridRow temp = new LastYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _lastFinancialYearDatagrid.Add(temp);

                return _lastFinancialYearDatagrid;
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
                MessageBox.Show("New accounting year opened for the year " + newFinancialYear + ".\n\nPlease restart SOLVE to avoid inconsistent behavior.");
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

        private RelayCommand _openNewFinancialYearClicked;
        public ICommand OpenNewFinancialYearClicked
        {
            get { return _openNewFinancialYearClicked ?? (_openNewFinancialYearClicked = new RelayCommand(p1 => this.OpenNewFinancialYear(), p2 => !hasOpenFinancialYear)); }
        }

        private RelayCommand _editOpeningBalanceClicked;
        public ICommand EditOpeningBalanceClicked
        {
            get { return _editOpeningBalanceClicked ?? (_editOpeningBalanceClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _importToCurrrentYearClicked;
        public ICommand ImportToCurrrentYearClicked
        {
            get { return _importToCurrrentYearClicked ?? (_importToCurrrentYearClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class LastYearDatagridRow
    {
        public string HeadName { get; set; }
        public double Amount { get; set; }
    }
}
