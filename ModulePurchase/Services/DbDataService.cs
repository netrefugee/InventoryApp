using Models;
using ModulePurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulePurchase.Services
{
    public class DbDataService
    { 
        /// <summary>
        /// 根据ID获取单价
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public double? UnitPrice(long ID, JinXiaoCunType type)
        {
            if (type == JinXiaoCunType.批发)
            {
                using (var db = new InventoryDB())
                {
                    var query = from p in db.Goods
                                where p.商品ID == ID
                                select p.批发价格;
                    return query.First();
                }
            }
            if (type == JinXiaoCunType.零售)
            {
                using (var db = new InventoryDB())
                {
                    var query = from p in db.Goods
                                where p.商品ID == ID
                                select p.零售价格;
                    return query.First();
                }
            }
            if (type == JinXiaoCunType.进货)
            {
                using (var db = new InventoryDB())
                {
                    var query = from p in db.Goods
                                where p.商品ID == ID
                                select p.进货价格;
                    return query.First();
                }
            }
            return null;
        }
    }
}
