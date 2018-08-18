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
    public class AddClientWindowViewModel : BindableBase
    {
        public AddClientWindowViewModel()
        {

        }
        private Client client;
        public Client Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }
        public Window Window { get; set; }

        #region [ 保存 ]
        private DelegateCommand save;
        public DelegateCommand Save => save ?? (save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            if (Utils.Utils.IsNullOrEmpty((string)Client.姓名)) { DXMessageBox.Show("姓名! 未填写"); return; }
            Client.姓名 = Client.姓名.Trim();

            DbDataService dbDataService = new DbDataService();
            // 如果是0,就是添加
            if (Client.客户ID == 0)
            {
                if (dbDataService.isExistClient(Client))
                {
                    DXMessageBox.Show($"添加失败,已存在姓名为:{Client.姓名}的农户");
                    Window.DialogResult = false;
                    return;
                }
                else
                {
                    dbDataService.InsertClient(Client);
                    Window.DialogResult = true;
                }
            }// 更新
            else
            {
                dbDataService.UpdateClient(Client);
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
