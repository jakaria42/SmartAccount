﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Factories;
using System.Windows.Input;

namespace GKS.Model.ViewModels
{
    public class CloseCurrentFinancialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IParameterManager _parameterManager;

        public CloseCurrentFinancialYearModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
            _parameterManager = BLLCoreFactory.GetParameterManager();

            _currentFinancialYearDatagrid = new List<CurrentYearDatagridRow>();
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
            }
        }

        IList<CurrentYearDatagridRow> _currentFinancialYearDatagrid;
        IList<CurrentYearDatagridRow> CurrentFinancialYearDatagrid
        {
            get
            {
                CurrentYearDatagridRow temp = new CurrentYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _currentFinancialYearDatagrid.Add(temp);

                return _currentFinancialYearDatagrid;
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
            _parameterManager.Set("CurrentFinancialYear", "");
        }

        private bool hasOpenFinancialYear
        {
            get
            {
                return _parameterManager.Get("CurrentFinancialYear") != "";
            }
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
        public double Amount { get; set; }
    }
}
