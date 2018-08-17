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
    public class ClientViewModel : BindableBase
    {
        public ClientViewModel()
        {
            client = clientInit();

            IDataService dataService = new XmlDataService();

            currentState = new CurrentState() { StateNow = State.None, Info = "" };   // 无
        }
        private Client clientInit()
        {
            return new Client() { };
        }

        private CurrentState currentState;
        public CurrentState CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }



        // 客户
        private Client client;
        public Client Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }



        // 添加客户
        private Visibility isShowAddClientPanel = Visibility.Collapsed;
        public Visibility IsShowAddClientPanel
        {
            get { return isShowAddClientPanel; }
            set { SetProperty(ref isShowAddClientPanel, value); }
        }

        private DelegateCommand _showAddClientPanel;
        public DelegateCommand ShowAddClientPanel =>
            _showAddClientPanel ?? (_showAddClientPanel = new DelegateCommand(ExecuteShowAddClientPanel));

        void ExecuteShowAddClientPanel()
        {
            IsShowAddClientPanel = Visibility.Visible;
            Client = clientInit();
            CurrentState = new CurrentState() { StateNow = State.Insert, Info = "正在【添加】客户" };
        }
        // 取消
        private DelegateCommand _hideAddClientPanel;
        public DelegateCommand HideAddClientPanel =>
            _hideAddClientPanel ?? (_hideAddClientPanel = new DelegateCommand(ExecuteHideAddClientPanel));

        private IQueryable<Client> clients;
        public IQueryable<Client> Clients
        {
            get { return clients; }
            set { SetProperty(ref clients, value); }
        }
        private IQueryable<ClientAccount> clientAccounts;
        public IQueryable<ClientAccount> ClientAccounts
        {
            get { return clientAccounts; }
            set { SetProperty(ref clientAccounts, value); }
        }
        void ExecuteHideAddClientPanel()
        {
            Client = clientInit();
            IsShowAddClientPanel = Visibility.Collapsed;
            CurrentState = new CurrentState() { StateNow = State.None, Info = "" };

        }
        // 载入clientAccount
        private DelegateCommand<Client> loadClientAccounts;
        public DelegateCommand<Client> LoadClientAccounts =>
            loadClientAccounts ?? (loadClientAccounts = new DelegateCommand<Client>(ExecuteLoadClientAccounts));

        void ExecuteLoadClientAccounts(Client parameter)
        {
            if (parameter != null)
            {
                using (var db = new InventoryDB())
                {
                    var query = from p in db.ClientAccounts
                                where parameter.客户ID == p.客户ID
                                select p;
                    ClientAccounts = query;
                }
            }
        }
        // 刷新
        private DelegateCommand updateClients;
        public DelegateCommand UpdateClients =>
            updateClients ?? (updateClients = new DelegateCommand(ExecuteUpdateClients));

        void ExecuteUpdateClients()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Clients select p;
                Clients = query;
            }
        }
        // 修改
        private DelegateCommand<Client> editClient;
        public DelegateCommand<Client> EditClient =>
            editClient ?? (editClient = new DelegateCommand<Client>(ExecuteEditClient));

        void ExecuteEditClient(Client parameter)
        {
            MessageBoxResult mr = DevExpress.Xpf.Core.DXMessageBox.Show($"确定修改客户 {parameter.姓名} 的信息吗?", "修改客户信息", MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                Client = parameter;
                // 打开面板
                IsShowAddClientPanel = Visibility.Visible;
                CurrentState = new CurrentState() { StateNow = State.Update, Info = $"正在【修改】客户信息-->{parameter.姓名}" };
            }
            else
            {
                ExecuteHideAddClientPanel();
            }
        }

        // 保存
        private DelegateCommand saveAddClient;
        public DelegateCommand SaveAddClient =>
            saveAddClient ?? (saveAddClient = new DelegateCommand(ExecuteSaveAddClient));

        void ExecuteSaveAddClient()
        {
            //bool isNull = false;
            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(client.姓名)) { DevExpress.Xpf.Core.DXMessageBox.Show("姓名! 未填写"); return; }



            DbDataService dbDataService = new DbDataService();
            if (CurrentState.StateNow == State.Insert)
            {
                if (dbDataService.isExistClient(client))
                {
                    DevExpress.Xpf.Core.DXMessageBox.Show($"添加失败,已存在姓名为:{Client.姓名}的客户");
                    return;
                }
                else
                {
                    dbDataService.InsertClient(client);
                }
            }
            if (CurrentState.StateNow == State.Update)
            {
                dbDataService.UpdateClient(client);
            }
            // 更新数据
            ExecuteUpdateClients();
            // 重置数据
            Client = clientInit();
            // 关闭面板
            ExecuteHideAddClientPanel();
        }

    }
}
