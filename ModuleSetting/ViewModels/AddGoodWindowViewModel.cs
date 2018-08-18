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
    public class AddGoodWindowViewModel : BindableBase
    {
        public AddGoodWindowViewModel()
        {
            IDataService dataService = new XmlDataService();
            isHighToxicityList = dataService.IsHighToxicityList();  // 是否高毒
            styleBigList = dataService.StyleBigList();              // 商品类别(大)
            smallUnitList = dataService.SmallUnitList();            // 单位(小)
            bigUnitList = dataService.BigUnitList();                // 单位(大)
        }
        public Window Window { get; set; }

        private Good good;
        public Good Good
        {
            get { return good; }
            set { SetProperty(ref good, value); }
        }

        #region 获取各种值

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
            if (!string.IsNullOrEmpty(parameter))
            {
                IDataService dataService = new XmlDataService();
                StyleSmallList = dataService.StyleSmallList(parameter);   // 商品类别(小)
            }
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
        #endregion

        #region [ 保存 ]

        // 保存
        private DelegateCommand save;
        public DelegateCommand Save => save ?? (save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {

            // 判断未填写
            if (Utils.Utils.IsNullOrEmpty(good.商品名称)) { DXMessageBox.Show("商品名称! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.种类小)) { DXMessageBox.Show(" 商品种类(小)! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.种类大)) { DXMessageBox.Show(" 商品种类(大)! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.是否高毒)) { DXMessageBox.Show("是否高毒! 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.单位大)) { DXMessageBox.Show("单位(大) 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.单位小)) { DXMessageBox.Show("单位(小) 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.内含量) || good.内含量 == 0) { DXMessageBox.Show($"商品每{good.单位大}数量 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.商品追溯码前11位)) { DXMessageBox.Show("商品追溯码 未填写"); return; }
            if (Utils.Utils.IsNullOrEmpty(good.生产厂家)) { DXMessageBox.Show("生产厂家 未填写"); return; }

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
            if (Good.商品ID == 0)
            {
                if (dbDataService.isExistGood(good))
                {
                    DXMessageBox.Show($"添加失败,已存在追溯码为:{Good.商品追溯码前11位}的商品");
                    Window.DialogResult = false;
                    return;
                }
                else
                {
                    dbDataService.InsertGood(good);
                    Window.DialogResult = true;
                }
            }
            else
            {
                dbDataService.UpdateGood(good);
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
