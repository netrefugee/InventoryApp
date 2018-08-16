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
    public class FarmerViewModel : BindableBase
    {
        public FarmerViewModel()
        {
            farmer = farmerInit();

            IDataService dataService = new XmlDataService();
 
            currentState = new CurrentState() { StateNow = State.None, Info = "" };   // 无
        }
       
        private Farmer farmerInit()
        {
            return new Farmer() { 省 = "吉林省", 市 = "辽源市", 县 = "东丰县" };
        }


        private CurrentState currentState;
        public CurrentState CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }

 

        // 农户
        private Farmer farmer;
        public Farmer Farmer
        {
            get { return farmer; }
            set { SetProperty(ref farmer, value); }
        }

 

        // 添加农户
        private Visibility isShowAddFarmerPanel = Visibility.Collapsed;
        public Visibility IsShowAddFarmerPanel
        {
            get { return isShowAddFarmerPanel; }
            set { SetProperty(ref isShowAddFarmerPanel, value); }
        }

        private DelegateCommand _showAddFarmerPanel;
        public DelegateCommand ShowAddFarmerPanel =>
            _showAddFarmerPanel ?? (_showAddFarmerPanel = new DelegateCommand(ExecuteShowAddFarmerPanel));

        void ExecuteShowAddFarmerPanel()
        {
            IsShowAddFarmerPanel = Visibility.Visible;
            Farmer = farmerInit();
            CurrentState = new CurrentState() { StateNow = State.Insert, Info = "正在【添加】农户" };
        }
        // 取消
        private DelegateCommand _hideAddFarmerPanel;
        public DelegateCommand HideAddFarmerPanel =>
            _hideAddFarmerPanel ?? (_hideAddFarmerPanel = new DelegateCommand(ExecuteHideAddFarmerPanel));

        private IQueryable farmers;
        public IQueryable Farmers
        {
            get { return farmers; }
            set { SetProperty(ref farmers, value); }
        }

        void ExecuteHideAddFarmerPanel()
        {
            Farmer = farmerInit();
            IsShowAddFarmerPanel = Visibility.Collapsed;
            CurrentState = new CurrentState() { StateNow = State.None, Info = "" };

        }
        // 刷新
        private DelegateCommand updateFarmers;
        public DelegateCommand UpdateFarmers =>
            updateFarmers ?? (updateFarmers = new DelegateCommand(ExecuteUpdateFarmers));

        void ExecuteUpdateFarmers()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Farmers select p;
                Farmers = query;
            }
        }
        // 修改
        private DelegateCommand<Farmer> editFarmer;
        public DelegateCommand<Farmer> EditFarmer =>
            editFarmer ?? (editFarmer = new DelegateCommand<Farmer>(ExecuteEditFarmer));

        void ExecuteEditFarmer(Farmer parameter)
        {
            MessageBoxResult mr = DXMessageBox.Show($"确定修改农户 {parameter.姓名} 的信息吗?", "修改农户信息", MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                Farmer = parameter;
                // 打开面板
                IsShowAddFarmerPanel = Visibility.Visible;
                CurrentState = new CurrentState() { StateNow = State.Update, Info = $"正在【修改】农户信息-->{parameter.姓名}" };
            }
            else
            {
                ExecuteHideAddFarmerPanel();
            }
        }

        // 保存
        private DelegateCommand saveAddFarmer;
        public DelegateCommand SaveAddFarmer =>
            saveAddFarmer ?? (saveAddFarmer = new DelegateCommand(ExecuteSaveAddFarmer));

        void ExecuteSaveAddFarmer()
        {
            //bool isNull = false;
            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(farmer.姓名)) {  DXMessageBox.Show("姓名! 未填写"); return; }
 


            DbDataService dbDataService = new DbDataService();
            if (CurrentState.StateNow == State.Insert)
            {
                if (dbDataService.isExistFarmer(farmer))
                {
                    DXMessageBox.Show($"添加失败,已存在姓名为:{Farmer.姓名}的农户");
                    return;
                }
                else
                {
                    dbDataService.InsertFarmer(farmer);
                }
            }
            if (CurrentState.StateNow == State.Update)
            {
                dbDataService.UpdateFarmer(farmer);
            }
            // 更新数据
            ExecuteUpdateFarmers();
            // 重置数据
            Farmer = farmerInit();
            // 关闭面板
            ExecuteHideAddFarmerPanel();
        }

    }
}
