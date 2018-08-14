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
            good = new Good() { /*IsHighToxicity = "普通商品", SmallUnit = "瓶", BigUnit = "箱" */};

            IDataService dataService = new XmlDataService();
            isHighToxicityList = dataService.IsHighToxicityList();  // 是否高毒
            styleBigList = dataService.StyleBigList();              // 商品类别(大)
            smallUnitList = dataService.SmallUnitList();            // 单位(小)
            bigUnitList = dataService.BigUnitList();                // 单位(大)
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

        // 显示
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
            //Good = new Good() { IsHighToxicity = "普通商品", SmallUnit = "瓶", BigUnit = "箱" };
            IsShowAddGoodPanel = Visibility.Collapsed;

        }
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


        // 保存
        private DelegateCommand saveAddGood;
        public DelegateCommand SaveAddGood =>
            saveAddGood ?? (saveAddGood = new DelegateCommand(ExecuteSaveAddGood));

        void ExecuteSaveAddGood()
        {
            bool isNull = false;
            // 判断未填写
            //if (Utils.Utils.IsNullOrEmpty(good.Name)) { isNull = true; DXMessageBox.Show("商品名称! 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.StyleSmall)) { isNull = true; DXMessageBox.Show(" 商品种类(小)! 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.StyleBig)) { isNull = true; DXMessageBox.Show(" 商品种类(大)! 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.IsHighToxicity)) { isNull = true; DXMessageBox.Show("是否高毒! 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.BigUnit)) { isNull = true; DXMessageBox.Show("单位(大) 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.SmallUnit)) { isNull = true; DXMessageBox.Show("单位(小) 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.Content)||good.Content==0) { isNull = true; DXMessageBox.Show($"商品每{good.BigUnit}数量 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.IdentificationCode)) { isNull = true; DXMessageBox.Show("商品追溯码 未填写"); return; }
            //if (Utils.Utils.IsNullOrEmpty(good.Manufacturer)) { isNull = true; DXMessageBox.Show("商品生产厂家 未填写"); return; }


            DbDataService dbDataService = new DbDataService();
            if (!dbDataService.AddGood(Good))
            {
                //DXMessageBox.Show($"添加失败!数据库中存在追溯码为:{Good.IdentificationCode}的商品");
            }
            // 更新数据
            ExecuteUpdateGoods();
        }



    }
}
