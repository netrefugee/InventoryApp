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

namespace ModuleSetting.ViewModels
{
    public class GoodsViewModel : BindableBase
    {
        public GoodsViewModel()
        {
            good = new Good() { IsHighToxicity = "普通商品", SmallUnit="瓶",BigUnit="箱"};
 
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

        private DelegateCommand _hideAddGoodPanel;
        public DelegateCommand HideAddGoodPanel =>
            _hideAddGoodPanel ?? (_hideAddGoodPanel = new DelegateCommand(ExecuteHideAddGoodPanel));

        void ExecuteHideAddGoodPanel()
        {
            Good = new Good() { IsHighToxicity = "普通商品", SmallUnit = "瓶", BigUnit = "箱" };
            IsShowAddGoodPanel = Visibility.Collapsed;
        }
        // 保存
        private DelegateCommand saveAddGood;
        public DelegateCommand SaveAddGood =>
            saveAddGood ?? (saveAddGood = new DelegateCommand(ExecuteSaveAddGood));

        void ExecuteSaveAddGood()
        {
            //using (var db = new InventoryDB())
            //{
            //    db.Insert(product);
            //}
        }



    }
}
