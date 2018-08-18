using Models;
using ModuleSetting.Models;
using ModuleSetting.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ModuleSetting.ViewModels
{
    public class SupplierViewModel : BindableBase
    {
        public SupplierViewModel()
        {
            supplier = supplierInit();

            IDataService dataService = new XmlDataService();

            currentState = new CurrentState() { StateNow = State.None, Info = "" };   // 无
        }
        private Supplier supplierInit()
        {
            return new Supplier() { };
        }

        private CurrentState currentState;
        public CurrentState CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }



        // 供应商
        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set { SetProperty(ref supplier, value); }
        }



        // 添加供应商
        private Visibility isShowAddSupplierPanel = Visibility.Collapsed;
        public Visibility IsShowAddSupplierPanel
        {
            get { return isShowAddSupplierPanel; }
            set { SetProperty(ref isShowAddSupplierPanel, value); }
        }

        private DelegateCommand _showAddSupplierPanel;
        public DelegateCommand ShowAddSupplierPanel =>
            _showAddSupplierPanel ?? (_showAddSupplierPanel = new DelegateCommand(ExecuteShowAddSupplierPanel));

        void ExecuteShowAddSupplierPanel()
        {
            IsShowAddSupplierPanel = Visibility.Visible;
            Supplier = supplierInit();
            CurrentState = new CurrentState() { StateNow = State.Insert, Info = "正在【添加】供应商" };
        }
        // 取消
        private DelegateCommand _hideAddSupplierPanel;
        public DelegateCommand HideAddSupplierPanel =>
            _hideAddSupplierPanel ?? (_hideAddSupplierPanel = new DelegateCommand(ExecuteHideAddSupplierPanel));

        private IQueryable suppliers;
        public IQueryable Suppliers
        {
            get { return suppliers; }
            set { SetProperty(ref suppliers, value); }
        }

        void ExecuteHideAddSupplierPanel()
        {
            Supplier = supplierInit();
            IsShowAddSupplierPanel = Visibility.Collapsed;
            CurrentState = new CurrentState() { StateNow = State.None, Info = "" };

        }
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
        // 修改
        private DelegateCommand<Supplier> editSupplier;
        public DelegateCommand<Supplier> EditSupplier =>
            editSupplier ?? (editSupplier = new DelegateCommand<Supplier>(ExecuteEditSupplier));

        void ExecuteEditSupplier(Supplier parameter)
        {
            MessageBoxResult mr = DevExpress.Xpf.Core.DXMessageBox.Show($"确定修改供应商 {parameter.供应商名称} 的信息吗?", "修改供应商信息", MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                Supplier = parameter;
                // 打开面板
                IsShowAddSupplierPanel = Visibility.Visible;
                CurrentState = new CurrentState() { StateNow = State.Update, Info = $"正在【修改】供应商信息-->{parameter.供应商名称}" };
            }
            else
            {
                ExecuteHideAddSupplierPanel();
            }
        }

        // 保存
        private DelegateCommand saveAddSupplier;
        public DelegateCommand SaveAddSupplier =>
            saveAddSupplier ?? (saveAddSupplier = new DelegateCommand(ExecuteSaveAddSupplier));

        void ExecuteSaveAddSupplier()
        {
            //bool isNull = false;
            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(supplier.供应商名称)) { DevExpress.Xpf.Core.DXMessageBox.Show("供应商名称! 未填写"); return; }

            DbDataService dbDataService = new DbDataService();
            if (CurrentState.StateNow == State.Insert)
            {
                if (dbDataService.isExistSupplier(supplier))
                {
                    DevExpress.Xpf.Core.DXMessageBox.Show($"添加失败,已存在名称为:{Supplier.供应商名称}的供应商");
                    return;
                }
                else
                {
                    dbDataService.InsertSupplier(supplier);
                }
            }
            if (CurrentState.StateNow == State.Update)
            {
                dbDataService.UpdateSupplier(supplier);
            }
            // 更新数据
            ExecuteUpdateSuppliers();
            // 关闭面板
            ExecuteHideAddSupplierPanel();
        }
    }
}
