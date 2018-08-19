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
        #region [ 商品 ]
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
#endregion
        #region  [ 农户 ]
        // 是否存在
        internal bool isExistFarmer(Farmer farmer)
        {
            using (var db = new InventoryDB())
            {

                // 是否存在商品
                var query = from p in db.Farmers
                            where p.姓名 == farmer.姓名
                            select p;
                // 如果存在
                if (query.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }
        // 更新供应商
        internal void UpdateSupplierAccount(SupplierAccount supplierAccount)
        {
            using (var db = new InventoryDB())
            {
                db.Update(supplierAccount);
            }
        }
        // 添加供应商
        internal void InsertSupplierAccount(SupplierAccount supplierAccount)
        {
            using (var db = new InventoryDB())
            {
                db.Insert(supplierAccount);
            }
        }

        // 添加农户
        internal void InsertFarmer(Farmer farmer)
        {
            using (var db = new InventoryDB())
            {
                db.Insert(farmer);
            }
        }
        // 修改农户
        internal void UpdateFarmer(Farmer farmer)
        {
            using (var db = new InventoryDB())
            {
                db.Update(farmer);
            }
        }

        #endregion
        #region 客户
        internal bool isExistClient(Client client)
        {
            using (var db = new InventoryDB())
            {

                // 是否存在商品
                var query = from p in db.Clients
                            where p.姓名 == client.姓名
                            select p;
                // 如果存在
                if (query.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }
        internal void InsertClient(Client client)
        {
            using (var db = new InventoryDB())
            {
                db.Insert(client);
            }
        }

        internal void UpdateClient(Client client)
        {
            using (var db = new InventoryDB())
            {
                db.Update(client);
            }
        }
        #endregion
        #region 供应商
        internal bool isExistSupplier(Supplier supplier)
        {
            using (var db = new InventoryDB())
            {

                // 是否存在商品
                var query = from p in db.Suppliers
                            where p.供应商名称 == supplier.供应商名称
                            select p;
                // 如果存在
                if (query.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal void InsertSupplier(Supplier supplier)
        {
            using (var db = new InventoryDB())
            {
                db.Insert(supplier);
            }
        }

        internal void UpdateSupplier(Supplier supplier)
        {
            using (var db = new InventoryDB())
            {
                db.Update(supplier);
            }
        }
        #endregion

        #region 客户账户
        internal void InsertClientAccount(ClientAccount clientAccount)
        {
            using (var db = new InventoryDB())
            {
                db.Insert(clientAccount);
            }
        }

        internal void UpdateClientAccount(ClientAccount clientAccount)
        {
            using (var db = new InventoryDB())
            {
                db.Update(clientAccount);
            }
        }
        #endregion
    }
}
