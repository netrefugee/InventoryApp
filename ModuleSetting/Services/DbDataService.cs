using LinqToDB;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting.Services
{
    class DbDataService
    {
        // 是否存在商品
        public bool isExistGood(Good good)
        {
            using (var db = new InventoryDB())
            {

                // 是否存在商品
                var query = from p in db.Goods
                            where p.商品追溯码前11位 == good.商品追溯码前11位
                            select p;
                // 如果存在
                if (query.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public void InsertGood(Good good)
        {
            using (var db = new InventoryDB())
            {
                    db.Insert(good);
            }
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="good"></param>
        internal void UpdateGood(Good good)
        {
            using (var db = new InventoryDB())
            {
                db.Update(good);
            }
        }
    }
}
