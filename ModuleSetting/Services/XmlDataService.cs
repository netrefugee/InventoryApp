using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xml.Services;

namespace ModuleSetting.Services
{
    class XmlDataService : IDataService
    {
        // 单位(大)
        public IEnumerable<string> BigUnitList()
        {
            return XmlDataUtil.QueryElementsAllowToElementById("单位(大)");
        }
        // 是否高毒
        public IEnumerable<string> IsHighToxicityList()
        {
            return XmlDataUtil.QueryElementsAllowToElementById("是否高毒");
        }
        // 单位(小)
        public IEnumerable<string> SmallUnitList()
        {
            return XmlDataUtil.QueryElementsAllowToElementById("单位(小)");
        }

        // 商品类别(大)
        public IEnumerable<string> StyleBigList()
        {
            return XmlDataUtil.QueryElementsById("styleBig");
        }
        // 商品类别(小)
        public IEnumerable<string> StyleSmallList(string styleBig)
        {
            return XmlDataUtil.QueryElementsAllowToElementById(styleBig);
        }

 
    }
}
