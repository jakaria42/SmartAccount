﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Factories;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;

namespace GKS.Model.ViewModels
{
    public class ProjectManagementModel : ViewModelBase
    {
        readonly IProjectManager _projectManager;

        public ProjectManagementModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
            }
            catch { }

        }

        public ObservableCollection<Project> Projects
        {
            get { return new ObservableCollection<Project>(_projectManager.GetProjects()); }
        }

        private Project _selectedGridItem;
        public Project SelectedGridItem
        {
            get { return _selectedGridItem; }
            set
            {
                _selectedGridItem = value;
            }
        }

        #region Relay Commands

        private RelayCommand _refreshButtonClicked;
        public ICommand RefreshButtonClicked
        {
            get
            {
                return _refreshButtonClicked ?? (_refreshButtonClicked = new RelayCommand(p1 => this.Reset()));
            }
        }

        public void Reset()
        {
            NotifyPropertyChanged("Projects");
        }
        #endregion
    }
}
