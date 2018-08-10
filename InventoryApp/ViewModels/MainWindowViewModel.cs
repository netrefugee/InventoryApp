using InventoryApp.Services;
using LinqToDB;
using Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
        }
        // 标题
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        // 导航命令
        public DelegateCommand<string> NavigateCommand { get; private set; }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
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
}
