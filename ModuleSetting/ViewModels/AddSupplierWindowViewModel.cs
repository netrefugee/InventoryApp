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
    public class AddSupplierWindowViewModel : BindableBase
    {
        public AddSupplierWindowViewModel()
        {

        }
        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set { SetProperty(ref supplier, value); }
        }
        public Window Window { get; set; }

        #region [ 保存 ]
        private DelegateCommand save;
        public DelegateCommand Save => save ?? (save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            if (Utils.Utils.IsNullOrEmpty((string)Supplier.供应商名称)) { DevExpress.Xpf.Core.DXMessageBox.Show("供应商名称! 未填写"); return; }
            Supplier.供应商名称 = Supplier.供应商名称.Trim();

            DbDataService dbDataService = new DbDataService();
            // 如果是0,就是添加
            if (Supplier.供应商ID == 0)
            {
                if (dbDataService.isExistSupplier(Supplier))
                {
                    DevExpress.Xpf.Core.DXMessageBox.Show($"添加失败,已存在名称为:{Supplier.供应商名称}的供应商");
                 
                    return;
                }
                else
                {
                    dbDataService.InsertSupplier(Supplier);
                    Window.DialogResult = true;
                }
            }// 更新
            else
            {
                dbDataService.UpdateSupplier(Supplier);
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
