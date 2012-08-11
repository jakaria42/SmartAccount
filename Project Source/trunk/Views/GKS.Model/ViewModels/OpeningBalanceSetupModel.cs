using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Messaging;

namespace GKS.Model.ViewModels
{
    public class OpeningBalanceSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly IOpeningBalanceManager _openingBalanceManager;
        private readonly IParameterManager _parameterManager;

        public OpeningBalanceSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _openingBalanceManager = BLLCoreFactory.GetOpeningBalanceManager();
                _parameterManager = BLLCoreFactory.GetParameterManager();

                //NotifyOpeningBalanceDataGrid();
                OpeningBalanceAmount = 0;

                AllProjects = _projectManager.GetProjects(false);
            }
            catch
            { }
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
                NotifyPropertyChanged("AllHeads");
                SelectedHead = null;
                NotifyOpeningBalanceDataGrid();
            }
        }

        public IList<Head> AllHeads
        {
            get
            {
                if (SelectedProject == null) return new List<Head>();
                return _headManager.GetCapitalHeads(SelectedProject, true, false);
            }
        }

        private Head _selectedHead;
        public Head SelectedHead
        {
            get
            {
                return _selectedHead;
            }
            set
            {
                _selectedHead = value;
                NotifyPropertyChanged("SelectedHead");
            }
        }

        private string _openingBalanceCurrentYear;
        public string OpeningBalanceCurrentYear
        {
            get
            {
                return _parameterManager.GetCurrentFinancialYear();
                //return _openingBalanceCurrentYear;
            }
            set
            {
                _openingBalanceCurrentYear = value;
                NotifyPropertyChanged("OpeningBalanceCurrentYear");
            }
        }

        private double _openingBalanceAmount;
        public double OpeningBalanceAmount
        {
            get
            {
                return _openingBalanceAmount;
            }
            set
            {
                _openingBalanceAmount = value;
                NotifyPropertyChanged("OpeningBalanceAmount");
            }
        }

        private IList<OpeningBalance> _openingBalanceDataGridItems;
        public IList<OpeningBalance> OpeningBalanceDataGridItems
        {
            get
            {
                return _openingBalanceDataGridItems;
            }
            set
            {
                _openingBalanceDataGridItems = value;
                NotifyPropertyChanged("OpeningBalanceDataGridItems");
                NotifyPropertyChanged("OpeningBalanceDataGrid");
            }
        }
        
        private IList<OpeningBalanceGridRow> _openingBalanceDataGrid;
        public IList<OpeningBalanceGridRow> OpeningBalanceDataGrid
        {
            get
            {
                if (OpeningBalanceDataGridItems == null || OpeningBalanceDataGridItems.Count == 0)
                    return null;

                return OpeningBalanceDataGridItems.Select(ob => /*ob.FinancialYear == OpeningBalanceCurrentYear &&*/ GetOpeningBalaceDridRow(ob)).ToList();                
            }
        }

        private OpeningBalanceGridRow GetOpeningBalaceDridRow(OpeningBalance openingBalance)
        {
            return new OpeningBalanceGridRow
            {
                HeadName = openingBalance.ProjectHead.Head.Name,
                CurrentYearBalance = openingBalance.Balance,
            };
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
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

        private RelayCommand _saveButtonClicked;
        public ICommand SaveButtonClicked
        {
            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.SaveOpeningBalance())); }
        }

        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get { return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private void SaveOpeningBalance()
        {
            if (_openingBalanceManager.Set(SelectedProject, SelectedHead, OpeningBalanceAmount))
                NotifyOpeningBalanceDataGrid();
            ShowMessage(MessageService.Instance.GetLatestMessage());
        }

        private void ShowMessage(Message message)
        {
            MessageText = message.MessageText;
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
        }

        public void NotifyOpeningBalanceDataGrid()
        {
            if (SelectedProject == null)
                return;

            OpeningBalanceDataGridItems = _openingBalanceManager.GetOpeningBalances(SelectedProject);
        }
    }

    public class OpeningBalanceGridRow
    {
        public string HeadName { get; set; }
        public double CurrentYearBalance { get; set; }
        //public double PreviousYearBalance { get; set; }
    }
}
