using DevExpress.Xpf.Core;
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

        private CurrentState clientAccountState;
        public CurrentState ClientAccountState
        {
            get { return clientAccountState; }
            set { SetProperty(ref clientAccountState, value); }
        }

        // 客户
        private Client client;
        public Client Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }

        private ClientAccount clientAccount;
        public ClientAccount ClientAccount
        {
            get { return clientAccount; }
            set { SetProperty(ref clientAccount, value); }
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
            set
            {
                ShowAddClientAccountPanel.RaiseCanExecuteChanged();
                SetProperty(ref clientAccounts, value); }
        }
        void ExecuteHideAddClientPanel()
        {
            Client = clientInit();
            IsShowAddClientPanel = Visibility.Collapsed;
            CurrentState = new CurrentState() { StateNow = State.None, Info = "" };

        }

        private DelegateCommand hideAddClientAccountPanel;
        public DelegateCommand HideAddClientAccountPanel =>
            hideAddClientAccountPanel ?? (hideAddClientAccountPanel = new DelegateCommand(ExecuteHideAddClientAccountPanel));

        void ExecuteHideAddClientAccountPanel()
        {

            ClientAccount = new ClientAccount();
            IsShowAddClientAccountPanel = Visibility.Collapsed;
            ClientAccountState = new CurrentState() { StateNow = State.None, Info = "" };
        }

        // 添加 ClientAccount
        private DelegateCommand saveAddClientAccount;
        public DelegateCommand SaveAddClientAccount =>
            saveAddClientAccount ?? (saveAddClientAccount = new DelegateCommand(ExecuteSaveAddClientAccount));

        void ExecuteSaveAddClientAccount()
        {
            if (ClientAccounts == null) { DXMessageBox.Show("未找到当前客户往来,请选择一个客户的金额"); return; }

            //bool isNull = false;
            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(clientAccount.收入或支出)) { DevExpress.Xpf.Core.DXMessageBox.Show("收入或支出! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(clientAccount.日期)) { DevExpress.Xpf.Core.DXMessageBox.Show("日期! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(clientAccount.金额)) { DevExpress.Xpf.Core.DXMessageBox.Show("金额! 未填写"); return; }



            DbDataService dbDataService = new DbDataService();
            if (ClientAccountState.StateNow == State.Insert)
            {

                dbDataService.InsertClientAccount(clientAccount);

            }
            if (ClientAccountState.StateNow == State.Update)
            {
                dbDataService.UpdateClientAccount(clientAccount);
            }
            // 更新数据
            ExecuteUpdateClientAccounts();
            // 关闭面板
            ExecuteHideAddClientAccountPanel();
        }

        // 更新数据
        private void ExecuteUpdateClientAccounts()
        {

            if (clientAccount != null)
            {
                using (var db = new InventoryDB())
                {
                    var query = from p in db.ClientAccounts
                                where clientAccount.客户ID == p.客户ID
                                select p;
                    ClientAccounts = query;
                }
            }
        }

        private Visibility isShowAddClientAccountPanel = Visibility.Collapsed;
        public Visibility IsShowAddClientAccountPanel
        {
            get { return isShowAddClientAccountPanel; }
            set { SetProperty(ref isShowAddClientAccountPanel, value); }
        }



        // 显示clientacouunt 面板
        private DelegateCommand showAddClientAccountPanel;
        public DelegateCommand ShowAddClientAccountPanel =>
            showAddClientAccountPanel ?? (showAddClientAccountPanel = new DelegateCommand(ExecuteShowAddClientAccountPanel, CanExecuteShowAddClientAccountPanel));

        void ExecuteShowAddClientAccountPanel()
        {
            IsShowAddClientAccountPanel = Visibility.Visible;
            ClientAccount = new ClientAccount() { 客户ID = ClientAccount.客户ID, 姓名 = ClientAccount.姓名 };
            ClientAccountState = new CurrentState() { StateNow = State.Insert, Info = $"正在为 {ClientAccount.姓名} 【添加】 往来" };

        }
        bool CanExecuteShowAddClientAccountPanel()
        {
            if (ClientAccounts != null)
            {
                return true;
            }
            return false;
        }

        // 载入clientAccount
        private DelegateCommand<Client> loadClientAccounts;
        public DelegateCommand<Client> LoadClientAccounts =>
            loadClientAccounts ?? (loadClientAccounts = new DelegateCommand<Client>(ExecuteLoadClientAccounts));

        void ExecuteLoadClientAccounts(Client parameter)
        {
            ClientAccount = new ClientAccount() { 客户ID = parameter.客户ID, 姓名 = parameter.姓名 };

            using (var db = new InventoryDB())
            {
                var query = from p in db.ClientAccounts
                            where parameter.客户ID == p.客户ID
                            select p;
                ClientAccounts = query;
            }

            ClientAccountState = new CurrentState() { StateNow = State.Insert, Info = $"正在为 {ClientAccount.姓名} 【添加】 往来" };
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

        private DelegateCommand<ClientAccount> editClientsAccount;
        public DelegateCommand<ClientAccount> EditClientsAccount =>
            editClientsAccount ?? (editClientsAccount = new DelegateCommand<ClientAccount>(ExecuteEditClientsAccount));

        void ExecuteEditClientsAccount(ClientAccount parameter)
        {
            MessageBoxResult mr = DevExpress.Xpf.Core.DXMessageBox.Show($"确定修改吗?", "修改客户账户信息", MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                ClientAccount = parameter;
                // 打开面板
                IsShowAddClientAccountPanel = Visibility.Visible;
                ClientAccountState = new CurrentState() { StateNow = State.Update, Info = $"正在【修改】{parameter.客户} 的客户信息" };
            }
            else
            {
                ExecuteHideAddClientPanel();
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
                CurrentState = new CurrentState() { StateNow = State.Update, Info = $"正在【修改】{parameter.姓名} 的客户信息" };
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
            // 关闭面板
            ExecuteHideAddClientPanel();
        }

    }
}
