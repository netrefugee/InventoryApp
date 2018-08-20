using DevExpress.Xpf.Core;
using LinqToDB;
using LinqToDB.Mapping;
using MaterialDesignThemes.Wpf;
using Models;
using ModuleSetting.Models;
using ModuleSetting.Services;
using ModuleSetting.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Xml.Services;
using ModuleSetting.Utils;
using Prism.Regions;

namespace ModuleSetting.ViewModels
{
    public class GoodsViewModel : BindableBase, INavigationAware
    {
        public GoodsViewModel()
        {

          
        }


        #region [ 添加商品 ]

        private DelegateCommand _showAddGoodWindow;
        public DelegateCommand ShowAddGoodWindow =>
            _showAddGoodWindow ?? (_showAddGoodWindow = new DelegateCommand(ExecuteShowAddGoodWindow));

        void ExecuteShowAddGoodWindow()
        {
            AddGoodWindow addGoodWindow = new AddGoodWindow(new Good() { 是否高毒 = "普通商品", 种类大 = "农药" });
            if (addGoodWindow.ShowDialog() == true)
            {
                ExecuteUpdateGoods();
            }
        }
        #endregion

        #region " Goods "
        private IQueryable goods;
        public IQueryable Goods
        {
            get { return goods; }
            set { SetProperty(ref goods, value); }
        }
        #endregion

        #region [ 刷新 ]
        private DelegateCommand updateGoods;
        public DelegateCommand UpdateGoods =>
            updateGoods ?? (updateGoods = new DelegateCommand(ExecuteUpdateGoods));

        void ExecuteUpdateGoods()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.Goods select p;
                Goods = query;
            }
        }
        #endregion

        #region [ 编辑 ]
        private DelegateCommand<Good> editGood;
        public DelegateCommand<Good> EditGood =>
            editGood ?? (editGood = new DelegateCommand<Good>(ExecuteEditGood));

        void ExecuteEditGood(Good parameter)
        {
            AddGoodWindow addGoodWindow = new AddGoodWindow(parameter);
            if (addGoodWindow.ShowDialog() == true)
            {
                ExecuteUpdateGoods();
            }
        }
        #endregion

        /// ****************************************************
        ///                        导航
        /// ****************************************************

        #region 导航

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
             UpdateGoods.Execute();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

    }
}
