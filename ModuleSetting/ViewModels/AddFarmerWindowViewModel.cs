using DevExpress.Xpf.Core;
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
    public class AddFarmerWindowViewModel : BindableBase
    {
        public AddFarmerWindowViewModel()
        {

        }
        private Farmer farmer;
        public Farmer Farmer
        {
            get { return farmer; }
            set { SetProperty(ref farmer, value); }
        }
        public Window Window { get; set; }



        #region [ 保存 ]
        private DelegateCommand save;
        public DelegateCommand Save => save ?? (save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            if (Utils.Utils.IsNullOrEmpty(Farmer.姓名)) { DXMessageBox.Show("姓名! 未填写"); return; }

            DbDataService dbDataService = new DbDataService();
            // 如果是0,就是添加
            if (Farmer.农户ID == 0)
            {
                if (dbDataService.isExistFarmer(Farmer))
                {
                    DXMessageBox.Show($"添加失败,已存在姓名为:{Farmer.姓名}的农户");
                    Window.DialogResult = false;
                    return;
                }
                else
                {
                    dbDataService.InsertFarmer(Farmer);
                    Window.DialogResult = true;
                }
            }// 更新
            else
            {
                dbDataService.UpdateFarmer(Farmer);
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
