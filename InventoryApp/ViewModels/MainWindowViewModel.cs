using DevExpress.Xpf.Core;
using InventoryApp.Services;
using InventoryApp.Views;
using LinqToDB;
 
using Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace InventoryApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // 初始化
        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            TestCommand = new DelegateCommand<string>(Test);

            Items = new ObservableCollection<HamburgerMenuItemViewModel>();
            Items.Add(new HamburgerMenuItemViewModel("进货", "PurchaseView"));
            Items.Add(new HamburgerMenuItemViewModel("零售", "RetailView"));
            Items.Add(new HamburgerMenuItemViewModel("批发", "WholesaleView"));
            Items.Add(new HamburgerMenuItemViewModel("农户设置", "FarmerView"));
            Items.Add(new HamburgerMenuItemViewModel("商品设置", "GoodsView"));
            Items.Add(new HamburgerMenuItemViewModel("客户设置", "ClientView"));
            Items.Add(new HamburgerMenuItemViewModel("供应商设置", "SupplierView"));
            Items.Add(new HamburgerMenuItemViewModel("日志", "LoggerView"));

        }

        // 标题
        private string _title = "prism";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<HamburgerMenuItemViewModel> items;
        public ObservableCollection<HamburgerMenuItemViewModel> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }


        // 导航命令
        public DelegateCommand<string> NavigateCommand { get; private set; }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                DXSplashScreen.Show(typeof(NavigateSplashScreen));
                _regionManager.RequestNavigate("ContentRegion", navigatePath, NavigationComplete);

            }
        }
        // 导航完成
        private void NavigationComplete(NavigationResult result)
        {

            DXSplashScreen.Close();
        }


        // 测试命令
        public DelegateCommand<string> TestCommand { get; private set; }
        private void Test(string test)
        {
            //string path = Environment.CurrentDirectory+"\\Data\\InventoryData.xml";
            //string storename = QueryXml.QueryElement(path, "storename");
            //IEnumerable<XElement> huafei = QueryXml.QueryElementByAttr(path, "styleBig", "style", "化肥");

            using (var db = new InventoryDB())
            {
                //var q =
                //    from c in db.Goods
                //    select c;

                //foreach (var c in q)
                //    Console.WriteLine(c.Style);

                //db.Goods.Insert(() => new Good() { Name = "哈哈" });

            }
        }

    }
    public class HamburgerMenuItemViewModel
    {
        public string Caption { get; set; }
        public string CommandParameter { get; set; }
        public string Icon { get; set; }


        public HamburgerMenuItemViewModel(string caption, string commandParameter)
        {
            CommandParameter = commandParameter;
            Caption = caption;
            Icon = "../icon/"+CommandParameter + ".png";
           
        }

    }

}
