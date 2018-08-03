using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulePurchase
{
    public class ModulePurchaseModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public ModulePurchaseModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<Views.PurchaseView>();
            //_container.RegisterTypeForNavigation<GoodsView>();
            //_container.RegisterTypeForNavigation<ClienterView>();
            //_container.RegisterTypeForNavigation<LoggerView>();
            //_container.RegisterTypeForNavigation<SupplierView>();
            //_container.RegisterTypeForNavigation<MasterView>();
            //_container.RegisterTypeForNavigation<WholesaleView>();
        }
    }
}
