using Microsoft.Practices.Unity;
using ModuleSetting.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting
{
    public class ModuleSettingModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public ModuleSettingModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<FarmerView>();
            _container.RegisterTypeForNavigation<GoodsView>();
            _container.RegisterTypeForNavigation<ClienterView>();
            _container.RegisterTypeForNavigation<LoggerView>();
            _container.RegisterTypeForNavigation<SupplierView>();
            _container.RegisterTypeForNavigation<MasterView>();
            _container.RegisterTypeForNavigation<WholesaleView>();
        }
    }
}
