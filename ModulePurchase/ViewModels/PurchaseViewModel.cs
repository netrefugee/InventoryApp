using DevExpress.Xpf.Grid;
using LinqToDB;
using LinqToDB.Data;
using Models;
using ModulePurchase.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace ModulePurchase.ViewModels
{
    public class PurchaseViewModel : BindableBase, INavigationAware
    {
        public PurchaseViewModel()
        {
            //IDataService dataService = new XmlDataService();
            PurchaseOrderInit();
        }

        #region " 初始化账单 PurchaseOrder "
        void PurchaseOrderInit()
        {
            PurchaseOrder = new PurchaseOrder
            {
                商店名称 = "prism",
                编号 = DateTime.Now.ToString("yyyyMMdd"),
                日期 = DateTime.Now.ToString("yyyy/M/d"),

                销赊退 = "销",

                Purchases = new ObservableCollection<Purchase>()
            };
            实收 = 0;
            应收 = 0;
        }
        #endregion

        #region " 制单人 "
        private string _制单人;
        public string 制单人
        {
            get { return _制单人; }
            set { SetProperty(ref _制单人, value); PurchaseOrder.制单人 = value; }
        }
        #endregion

        #region " 制单人s "
        private IEnumerable<string> _制单人s;
        public IEnumerable<string> 制单人s
        {
            get { return _制单人s; }
            set { SetProperty(ref _制单人s, value); }
        }
        #endregion

        #region " 实收 "
        private double _实收;
        public double 实收
        {
            get { return _实收; }
            set { SetProperty(ref _实收, value); 找零 = 实收 - 应收; PurchaseOrder.实收 = value; }
        }
        #endregion 

        #region " 应收 "
        // 应收
        private double _应收;
        public double 应收
        {
            get { return _应收; }
            set { SetProperty(ref _应收, value); PurchaseOrder.应收 = _应收; 找零 = 实收 - 应收; PurchaseOrder.应收 = value; }
        }
        #endregion

        #region " 商品 "
        private IQueryable goodsLookUp;
        public IQueryable GoodsLookUp
        {
            get { return goodsLookUp; }
            set { SetProperty(ref goodsLookUp, value); }
        }
        #endregion

        #region [ 上账 ]
        private DelegateCommand<GridControl> save;
        public DelegateCommand<GridControl> Save =>
            save ?? (save = new DelegateCommand<GridControl>(ExecuteSave));

        void ExecuteSave(GridControl gridControl)
        {

            using (var db = new InventoryDB())
            {
                long id = (long)db.InsertWithIdentity(PurchaseOrder);
                foreach (var item in PurchaseOrder.Purchases)
                {
                    item.进货单ID = id;
                }
                db.BulkCopy(PurchaseOrder.Purchases);
            }
            // 上账之后重新初始化
            PurchaseOrderInit();
            // 刷新下面
            UpdatePurchaseOrders.Execute();
        }
        #endregion

        #region " 找零 "
        private double _找零;
        public double 找零
        {
            get { return _找零; }
            set { SetProperty(ref _找零, value); }
        }
        #endregion

        #region " SuppliersLookUp 用于选取供应商"
        private IQueryable suppliersLookUp;
        public IQueryable SuppliersLookUp
        {
            get { return suppliersLookUp; }
            set { SetProperty(ref suppliersLookUp, value); }
        }
        #endregion

        #region " PurchaseOrder 上面的供应商"
        private PurchaseOrder purchaseOrder;
        public PurchaseOrder PurchaseOrder
        {
            get { return purchaseOrder; }
            set { SetProperty(ref purchaseOrder, value); }
        }
        #endregion

        #region " PurchaseOrders 下面的供应商列表"
        private IQueryable purchaseOrders;
        public IQueryable PurchaseOrders
        {
            get { return purchaseOrders; }
            set { SetProperty(ref purchaseOrders, value); }
        }
        #endregion

        #region [ 编辑 ] 
        private DelegateCommand<PurchaseOrder> editPurchaseOrder;
        public DelegateCommand<PurchaseOrder> EditPurchaseOrder =>
            editPurchaseOrder ?? (editPurchaseOrder = new DelegateCommand<PurchaseOrder>(ExecuteEditPurchase));

        void ExecuteEditPurchase(PurchaseOrder parameter)
        {

        }
        #endregion

        #region [ 刷新 ]
        private DelegateCommand updatePurchaseOrders;
        public DelegateCommand UpdatePurchaseOrders =>
            updatePurchaseOrders ?? (updatePurchaseOrders = new DelegateCommand(ExecuteUpdatePurchaseOrders));

        void ExecuteUpdatePurchaseOrders()
        {
            using (var db = new InventoryDB())
            {
                var query = from p in db.PurchaseOrders
                            join o in db.Suppliers on p.供应商ID equals o.供应商ID
                            select new
                            {
                                p.进货单ID,
                                p.编号,
                                p.日期,
                                p.应收,
                                p.实收,
                                p.制单人,
                                p.备注,
                                p.销赊退,
                                o.供应商名称
                            };
                PurchaseOrders = query;
            }
        }
        #endregion

        /// ****************************************************
        ///                        导航
        /// ****************************************************

        #region 导航
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdatePurchaseOrders.Execute();

            using (var db = new InventoryDB())
            {
                var query = from o in db.Suppliers
                            select o;
                // 供应商
                SuppliersLookUp = query;

                var goodquery = from o in db.Goods
                                select o;
                // 商品
                GoodsLookUp = goodquery;
            }
            // 制单人
            IDataService dataService = new XmlDataService();
            制单人s = dataService.ZhiDanRen();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
