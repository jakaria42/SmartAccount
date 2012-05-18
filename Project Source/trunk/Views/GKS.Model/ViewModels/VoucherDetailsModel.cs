﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Utils;

namespace GKS.Model.ViewModels
{
    public class VoucherDetailsModel : ViewModelBase
    {
        public VoucherDetailsModel()
        {
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }

        private string _voucherNo;
        public string VoucherNo
        {
            get { return _voucherNo; }
            set
            {
                _voucherNo = value;
                NotifyPropertyChanged("VoucherNo");
            }
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get { return _voucherDate; }
            set
            {
                _voucherDate = value;
                NotifyPropertyChanged("VoucherDate");
            }
        }

        private double _amount;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                TakaInWords = Utilities.NumberToTextInLacCrore(((double)_amount).ToString()) + "Only";
                NotifyPropertyChanged("Amount");
            }
        }

        private string _chequeNo;
        public string ChequeNo
        {
            get { return _chequeNo; }
            set
            {
                _chequeNo = value;
                NotifyPropertyChanged("ChequeNo");
            }
        }

        private string _chequeDate;
        public string ChequeDate
        {
            get { return _chequeDate; }
            set
            {
                _chequeDate = value;
                NotifyPropertyChanged("ChequeDate");
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                NotifyPropertyChanged("BankName");
            }
        }

        private string _takaInWords;
        public string TakaInWords
        {
            get { return _takaInWords; }
            set
            {
                _takaInWords = value;
                NotifyPropertyChanged("TakaInWords");
            }
        }

        private string _voucherNarration;
        public string VoucherNarration
        {
            get { return _voucherNarration; }
            set
            {
                _voucherNarration = value;
                NotifyPropertyChanged("VoucherNarration");
            }
        }

        #region Relay Commands
        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get
            {
                return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish()));
            }
        }

        private RelayCommand _printButtonClicked;
        public ICommand PrintButtonClicked
        {
            get { return _printButtonClicked ?? (_printButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _deleteButtonClicked;
        public ICommand DeleteButtonClicked
        {
            get { return _deleteButtonClicked ?? (_deleteButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
        #endregion
    }
}
