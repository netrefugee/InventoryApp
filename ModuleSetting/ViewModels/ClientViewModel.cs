using DevExpress.Xpf.Core;
using Models;
using ModuleSetting.Models;
using ModuleSetting.Services;
using ModuleSetting.Views;
using Prism.Commands;
using Prism.Logging;
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
        
        }
         
        /// ****************************************************
        ///                        客户
        /// ****************************************************

        #region [ 显示添加客户面板 ]
        private DelegateCommand _showAddClientWindow;
        public DelegateCommand ShowAddClientWindow =>
            _showAddClientWindow ?? (_showAddClientWindow = new DelegateCommand(ExecuteShowAddClientWindow));

        void ExecuteShowAddClientWindow()
        {
            AddClientWindow addClientWindow = new AddClientWindow(new Client() {  });
            if (addClientWindow.ShowDialog() == true)
            {
                ExecuteUpdateClients();
            }
        }
        #endregion

        #region [ 修改 ]

        // 修改
        private DelegateCommand<Client> editClient;
        public DelegateCommand<Client> EditClient =>
            editClient ?? (editClient = new DelegateCommand<Client>(ExecuteEditClient));

        void ExecuteEditClient(Client parameter)
        {
            AddClientWindow addClientWindow = new AddClientWindow(parameter);
            // 如果添加成功就更新数据
            if (addClientWindow.ShowDialog() == true)
            {
                ExecuteUpdateClients();
                //UpdateClients.Execute();
            }
        }
        #endregion

        #region [ 刷新 ]
        // 刷新
        private DelegateCommand updateClients;
        public  DelegateCommand UpdateClients =>
            updateClients ?? (updateClients = new DelegateCommand(ExecuteUpdateClients));

        void ExecuteUpdateClients()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Clients select p;
                Clients = query;
            }
        }
        #endregion

        #region " 客户 "
        private IQueryable<Client> clients;
        public IQueryable<Client> Clients
        {
            get { return clients; }
            set { SetProperty(ref clients, value); }
        }
        #endregion

        #region " 当前客户 "
        // 用于刷新客户账户
        private Client currentClient;
        public Client CurrentClient
        {
            get { return currentClient; }
            set { SetProperty(ref currentClient, value); }
        }
        #endregion


        /// ****************************************************
        ///                        客户账户
        /// ****************************************************
        #region " 客户账户 "
        private IQueryable<ClientAccount> clientAccounts;
        public IQueryable<ClientAccount> ClientAccounts
        {
            get { return clientAccounts; }
            set
            {
                ShowAddClientAccountWindow.RaiseCanExecuteChanged();
                SetProperty(ref clientAccounts, value); }
        }
        #endregion

        #region [ 刷新客户账户 ]
        // 载入clientAccount
        private DelegateCommand<Client> updateClientAccounts;
        public DelegateCommand<Client> UpdateClientAccounts =>
            updateClientAccounts ?? (updateClientAccounts = new DelegateCommand<Client>(ExecuteUpdateClientAccounts));

        void ExecuteUpdateClientAccounts(Client parameter)
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.ClientAccounts
                            where parameter.客户ID == p.客户ID
                            select p;
                ClientAccounts = query;
            }
            CurrentClient = parameter;

        }

        #endregion

        #region [ 显示添加客户账户面板 ]

        private DelegateCommand _showAddClientAccountWindow;
        public DelegateCommand ShowAddClientAccountWindow =>
            _showAddClientAccountWindow ?? (_showAddClientAccountWindow = new DelegateCommand(ExecuteShowAddClientAccountWindow, CanExecuteShowAddClientAccountWindow));

        void ExecuteShowAddClientAccountWindow()
        {
            AddClientAccountWindow addClientAccountWindow = new AddClientAccountWindow(new ClientAccount() { 客户ID=CurrentClient.客户ID, 姓名=CurrentClient.姓名 });
            if (addClientAccountWindow.ShowDialog() == true)
            {
                ExecuteUpdateClientAccounts(CurrentClient);
            }
        }

        bool CanExecuteShowAddClientAccountWindow()
        {
            if (ClientAccounts != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region [ 修改 ]

        // 修改
        private DelegateCommand<ClientAccount> editClientAccount;
        public DelegateCommand<ClientAccount> EditClientAccount =>
            editClientAccount ?? (editClientAccount = new DelegateCommand<ClientAccount>(ExecuteEditClientAccount));

        void ExecuteEditClientAccount(ClientAccount parameter)
        {
            //parameter.客户ID = CurrentClient.客户ID;
            //parameter.姓名 = CurrentClient.姓名;
            AddClientAccountWindow addClientAccountWindow = new AddClientAccountWindow(parameter);
            // 如果添加成功就更新数据
            if (addClientAccountWindow.ShowDialog() == true)
            {
                ExecuteUpdateClientAccounts(CurrentClient);
            }
        }
        #endregion

    }
}
