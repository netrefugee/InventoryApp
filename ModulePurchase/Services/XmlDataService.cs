using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xml.Services;

namespace ModulePurchase.Services
{
    class XmlDataService : IDataService
    {
        // 制单人
        public IEnumerable<string> ZhiDanRen()
        {
            return XmlDataUtil.QueryElementsAllowToElementById("制单人");
        }
    }
}
