﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Factories;
using GKS.Model.ViewModels;
using GKS.XAML.Pages;
using FixedAssetSchedule = GKS.XAML.Reports.FixedAssetSchedule;


namespace GKS.XAML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FixedAssetScheduleMenuItemClick(object sender, RoutedEventArgs e)
        {
            FixedAssetSchedule fixedAssetSchedule = new FixedAssetSchedule { Owner = this };
            fixedAssetSchedule.ShowDialog();
        }

        private void PostADebitVoucherClicked(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            if (voucherPostContext != null) voucherPostContext.SelectedVoucherType = "DV";
        }

        private void PostACreditVoucherClicked(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            if (voucherPostContext != null) voucherPostContext.SelectedVoucherType = "CV";
        }

        private void PostAJournalVoucherClicked(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            if (voucherPostContext != null) voucherPostContext.SelectedVoucherType = "JV";
        }

        private void PostAContraVoucherClicked(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            if (voucherPostContext != null) voucherPostContext.SelectedVoucherType = "Contra";
        }

        private void ViewAllProjectsClicked(object sender, RoutedEventArgs e)
        {
            tabItemProjects.Focus();
        }

        private void AddANewProjectClicked(object sender, RoutedEventArgs e)
        {
            tabItemProjects.Focus();
            projectMgmtUC1.buttonAdd_Click(sender, e);
        }

        private void ViewAllHeadsClicked(object sender, RoutedEventArgs e)
        {
            tabItemHeads.Focus(); 
        }

        private void AddANewHeadClicked(object sender, RoutedEventArgs e)
        {
            tabItemHeads.Focus();
            headMgmtUC1.buttonAdd_Click(sender, e);
        }

        private void ConfigurationSettingClicked(object sender, RoutedEventArgs e)
        {
            ConfigurationSetting configurationSetting = new ConfigurationSetting { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void SetupBudgetClicked(object sender, RoutedEventArgs e)
        {
            BudgetSetup budgetSetup = new BudgetSetup() { Owner = this };
            budgetSetup.ShowDialog();
        }

        private void StartNewAccountingYearClick(object sender, RoutedEventArgs e)
        {
            StartNewAccountingYear configurationSetting = new StartNewAccountingYear() { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void CloseCurrentAccountingYearClick(object sender, RoutedEventArgs e)
        {
            CloseCurrentAccountingYear configurationSetting = new CloseCurrentAccountingYear { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void FixedAssetManagementClicked(object sender, RoutedEventArgs e)
        {
            FixedAssetManagement configurationSetting = new FixedAssetManagement { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void MenuItemClicked(object sender, RoutedEventArgs e)
        {
            //Application.
        }

    }
}
