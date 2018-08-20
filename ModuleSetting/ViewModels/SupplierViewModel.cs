using Models;
using ModuleSetting.Models;
using ModuleSetting.Services;
using ModuleSetting.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ModuleSetting.ViewModels
{
    public class SupplierViewModel : BindableBase, INavigationAware
    {
        public SupplierViewModel()
        {
 
        }
        /// ****************************************************
        ///                        供应商
        /// ****************************************************

        #region [ 显示添加供应商面板 ]
        private DelegateCommand _showAddSupplierWindow;
        public DelegateCommand ShowAddSupplierWindow =>
            _showAddSupplierWindow ?? (_showAddSupplierWindow = new DelegateCommand(ExecuteShowAddSupplierWindow));

        void ExecuteShowAddSupplierWindow()
        {
            AddSupplierWindow addSupplierWindow = new AddSupplierWindow(new Supplier() { });
            if (addSupplierWindow.ShowDialog() == true)
            {
                ExecuteUpdateSuppliers();
            }
        }
        #endregion

        #region [ 修改 ]

        // 修改
        private DelegateCommand<Supplier> editSupplier;
        public DelegateCommand<Supplier> EditSupplier =>
            editSupplier ?? (editSupplier = new DelegateCommand<Supplier>(ExecuteEditSupplier));

        void ExecuteEditSupplier(Supplier parameter)
        {
            AddSupplierWindow addSupplierWindow = new AddSupplierWindow(parameter);
            // 如果添加成功就更新数据
            if (addSupplierWindow.ShowDialog() == true)
            {
                ExecuteUpdateSuppliers();
            }
        }
        #endregion

        #region [ 刷新 ]
        // 刷新
        private DelegateCommand updateSuppliers;
        public DelegateCommand UpdateSuppliers =>
            updateSuppliers ?? (updateSuppliers = new DelegateCommand(ExecuteUpdateSuppliers));

        void ExecuteUpdateSuppliers()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Suppliers select p;
                Suppliers = query;
            }
        }
        #endregion

        #region " 供应商 "
        private IQueryable<Supplier> suppliers;
        public IQueryable<Supplier> Suppliers
        {
            get { return suppliers; }
            set { SetProperty(ref suppliers, value); }
        }
        #endregion

        #region " 当前供应商 "
        // 用于刷新供应商账户
        private Supplier currentSupplier;
        public Supplier CurrentSupplier
        {
            get { return currentSupplier; }
            set { SetProperty(ref currentSupplier, value); }
        }
        #endregion


        /// ****************************************************
        ///                        供应商账户
        /// ****************************************************
        #region " 供应商账户 "
        private IQueryable<SupplierAccount> supplierAccounts;
        public IQueryable<SupplierAccount> SupplierAccounts
        {
            get { return supplierAccounts; }
            set
            {
                ShowAddSupplierAccountWindow.RaiseCanExecuteChanged();
                SetProperty(ref supplierAccounts, value);
            }
        }
        #endregion

        #region [ 刷新供应商账户 ]
        // 载入supplierAccount
        private DelegateCommand<Supplier> updateSupplierAccounts;
        public DelegateCommand<Supplier> UpdateSupplierAccounts =>
            updateSupplierAccounts ?? (updateSupplierAccounts = new DelegateCommand<Supplier>(ExecuteUpdateSupplierAccounts));

        void ExecuteUpdateSupplierAccounts(Supplier parameter)
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.SupplierAccounts
                            where parameter.供应商ID == p.供应商ID
                            select p;
                SupplierAccounts = query;
            }
            CurrentSupplier = parameter;

        }

        #endregion

        #region [ 显示添加供应商账户面板 ]
        private DelegateCommand _showAddSupplierAccountWindow;
        public DelegateCommand ShowAddSupplierAccountWindow =>
            _showAddSupplierAccountWindow ?? (_showAddSupplierAccountWindow = new DelegateCommand(ExecuteShowAddSupplierAccountWindow, CanExecuteShowAddSupplierAccountWindoww));

        void ExecuteShowAddSupplierAccountWindow()
        {
            AddSupplierAccountWindow addSupplierAccountWindow = new AddSupplierAccountWindow(new SupplierAccount() { 供应商ID = CurrentSupplier.供应商ID,  供应商名称 = CurrentSupplier.供应商名称 });
            if (addSupplierAccountWindow.ShowDialog() == true)
            {
                ExecuteUpdateSupplierAccounts(CurrentSupplier);
            }
        }
        bool CanExecuteShowAddSupplierAccountWindoww()
        {
            if (SupplierAccounts != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region [ 修改 ]

        // 修改
        private DelegateCommand<SupplierAccount> editSupplierAccount;
        public DelegateCommand<SupplierAccount> EditSupplierAccount =>
            editSupplierAccount ?? (editSupplierAccount = new DelegateCommand<SupplierAccount>(ExecuteEditSupplierAccount));

        void ExecuteEditSupplierAccount(SupplierAccount parameter)
        {
 
            AddSupplierAccountWindow addSupplierAccountWindow = new AddSupplierAccountWindow(parameter);
            // 如果添加成功就更新数据
            if (addSupplierAccountWindow.ShowDialog() == true)
            {
                ExecuteUpdateSupplierAccounts(CurrentSupplier);
            }
        }
        #endregion

        /// ****************************************************
        ///                        导航
        /// ****************************************************

        #region 导航

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateSuppliers.Execute();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

    }
}
