using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
 
    }
}
