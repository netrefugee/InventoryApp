using Models;
using ModuleSetting.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ModuleSetting.ViewModels
{
    public class AddSupplierAccountWindowViewModel : BindableBase
    {
        public AddSupplierAccountWindowViewModel()
        {

        }
        private SupplierAccount supplierAccount;
        public SupplierAccount SupplierAccount
        {
            get { return supplierAccount; }
            set { SetProperty(ref supplierAccount, value); }
        }
        public Window Window { get; set; }

        #region [ 保存 ]
        private DelegateCommand save;
        public DelegateCommand Save => save ?? (save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            if (Utils.Utils.IsNullOrEmpty(SupplierAccount.收入或支出)) { DevExpress.Xpf.Core.DXMessageBox.Show("收入或支出! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(SupplierAccount.日期)) { DevExpress.Xpf.Core.DXMessageBox.Show("日期! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(SupplierAccount.金额)) { DevExpress.Xpf.Core.DXMessageBox.Show("金额! 未填写"); return; }
            SupplierAccount.日期 = SupplierAccount.日期.Trim();


            DbDataService dbDataService = new DbDataService();
            // 如果是0,就是添加
            if (SupplierAccount.供应商账户ID == 0)
            {
                dbDataService.InsertSupplierAccount(SupplierAccount);
                Window.DialogResult = true;

            }// 更新
            else
            {
                dbDataService.UpdateSupplierAccount(SupplierAccount);
                Window.DialogResult = true;
            }
        }
        #endregion

        #region [ 取消按钮 ]
        private DelegateCommand closeWindow;
        public DelegateCommand CloseWindow =>
            closeWindow ?? (closeWindow = new DelegateCommand(ExecuteCloseWindow));

        void ExecuteCloseWindow()
        {
            Window.DialogResult = false;
        }
        #endregion
    }
}
