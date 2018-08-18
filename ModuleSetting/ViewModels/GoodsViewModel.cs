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

namespace ModuleSetting.ViewModels
{
    public class GoodsViewModel : BindableBase
    {
        public GoodsViewModel()
        {
            good = goodInit();

            IDataService dataService = new XmlDataService();
            isHighToxicityList = dataService.IsHighToxicityList();  // 是否高毒
            styleBigList = dataService.StyleBigList();              // 商品类别(大)
            smallUnitList = dataService.SmallUnitList();            // 单位(小)
            bigUnitList = dataService.BigUnitList();                // 单位(大)
            currentState =  new CurrentState() {  StateNow= State.None, Info=""} ;   // 无

        }

        private Good goodInit()
        {
            return new Good() { 是否高毒 = "普通商品", 单位小 = "瓶", 单位大 = "箱" };
        }

        private CurrentState currentState;
        public CurrentState CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }

        // 单位(小)
        private IEnumerable<string> smallUnitList;
        public IEnumerable<string> SmallUnitList
        {
            get { return smallUnitList; }
            set { SetProperty(ref smallUnitList, value); }
        }
        // 单位(大)
        private IEnumerable<string> bigUnitList;
        public IEnumerable<string> BigUnitList
        {
            get { return bigUnitList; }
            set { SetProperty(ref bigUnitList, value); }
        }

        // 商品
        private Good good;
        public Good Good
        {
            get { return good; }
            set { SetProperty(ref good, value); }
        }

        // 商品类别(小)List
        private IEnumerable<string> styleSmallList;
        public IEnumerable<string> StyleSmallList
        {
            get { return styleSmallList; }
            set { SetProperty(ref styleSmallList, value); }
        }

        private DelegateCommand<string> loadStyleSmallList;
        public DelegateCommand<string> LoadStyleSmallList =>
            loadStyleSmallList ?? (loadStyleSmallList = new DelegateCommand<string>(ExecuteLoadStyleSmall));

        void ExecuteLoadStyleSmall(string parameter)
        {
            IDataService dataService = new XmlDataService();
            StyleSmallList = dataService.StyleSmallList(parameter);   // 商品类别(小)


        }

        // 商品类别(大)List
        private IEnumerable<string> styleBigList;
        public IEnumerable<string> StyleBigList
        {
            get { return styleBigList; }
            set { SetProperty(ref styleBigList, value); }
        }

        // 是否高毒List
        private IEnumerable<string> isHighToxicityList;
        public IEnumerable<string> IsHighToxicityList
        {
            get { return isHighToxicityList; }
            set { SetProperty(ref isHighToxicityList, value); }

        }

        // 添加商品
        private Visibility isShowAddGoodPanel = Visibility.Collapsed;
        public Visibility IsShowAddGoodPanel
        {
            get { return isShowAddGoodPanel; }
            set { SetProperty(ref isShowAddGoodPanel, value); }
        }

        private DelegateCommand _showAddGoodPanel;
        public DelegateCommand ShowAddGoodPanel =>
            _showAddGoodPanel ?? (_showAddGoodPanel = new DelegateCommand(ExecuteShowAddGoodPanel));

        void ExecuteShowAddGoodPanel()
        {
            IsShowAddGoodPanel = Visibility.Visible;
            Good = goodInit();
            CurrentState = new CurrentState() { StateNow = State.Insert, Info = "正在【添加】商品" };
        }
        // 取消
        private DelegateCommand _hideAddGoodPanel;
        public DelegateCommand HideAddGoodPanel =>
            _hideAddGoodPanel ?? (_hideAddGoodPanel = new DelegateCommand(ExecuteHideAddGoodPanel));

        private IQueryable goods;
        public IQueryable Goods
        {
            get { return goods; }
            set { SetProperty(ref goods, value); }
        }

        void ExecuteHideAddGoodPanel()
        {

            Good = goodInit();
            IsShowAddGoodPanel = Visibility.Collapsed;
            CurrentState = new CurrentState() { StateNow = State.None, Info = "" };

        }
        // 刷新
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
        // 修改
        private DelegateCommand<Good> editGood;
        public DelegateCommand<Good> EditGood =>
            editGood ?? (editGood = new DelegateCommand<Good>(ExecuteEditGood));

        void ExecuteEditGood(Good parameter)
        {
            MessageBoxResult mr = DXMessageBox.Show( $"确定修改商品 {parameter.商品名称} 的信息吗?", "修改商品", MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                Good = parameter;

                // 打开面板
                IsShowAddGoodPanel = Visibility.Visible;
                CurrentState = new CurrentState() {  StateNow= State.Update, Info= $"正在【修改】商品-->{parameter.商品名称}" };
            }
            else
            {
                ExecuteHideAddGoodPanel();
            }
        }

        // 保存
        private DelegateCommand saveAddGood;
        public DelegateCommand SaveAddGood =>
            saveAddGood ?? (saveAddGood = new DelegateCommand(ExecuteSaveAddGood));

        void ExecuteSaveAddGood()
        {
            bool isNull = false;
            
            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(good.商品名称)) { isNull = true; DXMessageBox.Show("商品名称! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.种类小)) { isNull = true; DXMessageBox.Show(" 商品种类(小)! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.种类大)) { isNull = true; DXMessageBox.Show(" 商品种类(大)! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.是否高毒)) { isNull = true; DXMessageBox.Show("是否高毒! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.单位大)) { isNull = true; DXMessageBox.Show("单位(大) 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.单位小)) { isNull = true; DXMessageBox.Show("单位(小) 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.内含量) || good.内含量 == 0) { isNull = true; DXMessageBox.Show($"商品每{good.单位大}数量 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.商品追溯码前11位)) { isNull = true; DXMessageBox.Show("商品追溯码 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.生产厂家)) { isNull = true; DXMessageBox.Show("生产厂家 未填写"); return; }

            // 判断两边是否有空格
            good.商品名称 = good.商品名称.Trim();
            good.种类小 = good.种类小.Trim();
            good.种类大 = good.种类大.Trim();
            good.是否高毒 = good.是否高毒.Trim();
            good.单位大 = good.单位大.Trim();
            good.单位小 = good.单位小.Trim();
            good.商品追溯码前11位 = good.商品追溯码前11位.Trim();
            good.生产厂家 = good.生产厂家.Trim();

            DbDataService dbDataService = new DbDataService();
            if (CurrentState.StateNow== State.Insert)
            {
                if (dbDataService.isExistGood(good))
                {
                    DXMessageBox.Show($"添加失败,已存在追溯码为:{Good.商品追溯码前11位}的商品");
                    return;
                }
                else
                {
                    dbDataService.InsertGood(good);
                }
            }
            if (CurrentState.StateNow== State.Update)
            {
                dbDataService.UpdateGood(good);
            }
            // 更新数据
            ExecuteUpdateGoods();
            // 关闭面板
            ExecuteHideAddGoodPanel();
        }



    }
}
