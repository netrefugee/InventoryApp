using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting.Services
{
    interface IDataService
    {
        IEnumerable<string> IsHighToxicityList();
        IEnumerable<string> StyleBigList();
        IEnumerable<string> StyleSmallList(string styleBig);
        IEnumerable<string> SmallUnitList();
        IEnumerable<string> BigUnitList();
    }
}
