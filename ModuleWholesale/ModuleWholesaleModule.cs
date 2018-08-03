using Microsoft.Practices.Unity;
using ModuleWholesale.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleWholesale
{
    public class ModuleWholesaleModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public ModuleWholesaleModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<WholesaleView>();
 
        }
    }
}
