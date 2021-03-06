﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Factories;
using BLL.Model.Entity;
using BLL.Model.Managers;

namespace GKS.Model.ViewModels
{
    public class HeadManagementModel : ViewModelBase
    {
        readonly IHeadManager _headManager;
        public HeadManagementModel()
        {
            try
            {
                _headManager = BLLCoreFactory.GetHeadManager();
            }
            catch { }
        }

        public ObservableCollection<Head> Heads
        {
            get { return new ObservableCollection<Head>(_headManager.GetHeads(false)); }
        }

        private Head _selectedGridItem;
        public Head SelectedGridItem
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
            NotifyPropertyChanged("Heads");
        }

        #endregion
    }
}
