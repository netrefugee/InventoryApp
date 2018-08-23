using ModulePurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulePurchase.Services
{
    public interface IDataService
    {
        IEnumerable<string> ZhiDanRen();
    }
}
