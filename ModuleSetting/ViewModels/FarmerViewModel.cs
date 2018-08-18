using DevExpress.Xpf.Core;
using Models;
using ModuleSetting.Models;
using ModuleSetting.Services;
using ModuleSetting.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ModuleSetting.ViewModels
{
    public class FarmerViewModel : BindableBase
    {
        public FarmerViewModel()
        {
            IDataService dataService = new XmlDataService();
        }

        #region [ 添加农户 ]
        private DelegateCommand showAddFarmerWindow;
        public DelegateCommand ShowAddFarmerWindow =>
            showAddFarmerWindow ?? (showAddFarmerWindow = new DelegateCommand(ExecuteShowAddFarmerWindow));

        void ExecuteShowAddFarmerWindow()
        {
            AddFarmerWindow addFarmerWindow = new AddFarmerWindow(new Farmer() { 省 = "吉林省", 市 = "辽源市", 县 = "东丰县" });
            if (addFarmerWindow.ShowDialog()==true)
            {
                ExecuteRefreshFarmers();
            }
        }
        #endregion

        #region [ 刷新 ] 
        private DelegateCommand refreshFarmers;
        public DelegateCommand RefreshFarmers =>
            refreshFarmers ?? (refreshFarmers = new DelegateCommand(ExecuteRefreshFarmers));

        void ExecuteRefreshFarmers()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Farmers select p;
                Farmers = query;
            }
        }
        #endregion

        #region " Farmers "
        private IQueryable farmers;
        public IQueryable Farmers
        {
            get { return farmers; }
            set { SetProperty(ref farmers, value); }
        }
        #endregion

        #region [ 修改 ]
        private DelegateCommand<Farmer> editFarmer;
        public DelegateCommand<Farmer> EditFarmer =>
            editFarmer ?? (editFarmer = new DelegateCommand<Farmer>(ExecuteEditFarmer));

        void ExecuteEditFarmer(Farmer parameter)
        {
            AddFarmerWindow addFarmerWindow = new AddFarmerWindow(parameter);
            if (addFarmerWindow.ShowDialog() == true)
            {
                ExecuteRefreshFarmers();
            }
        }
        #endregion

 

    }
}
