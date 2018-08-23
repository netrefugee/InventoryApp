using DevExpress.Data;
using DevExpress.Xpf.Grid;
using Models;
using ModulePurchase.Services;
using ModulePurchase.ViewModels;
using System;
using System.Windows.Controls;

namespace ModulePurchase.Views
{
    /// <summary>
    /// Interaction logic for PurchaseView
    /// </summary>
    public partial class PurchaseView : UserControl
    {
        public PurchaseView()
        {
            InitializeComponent();
        }

        private void grid_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                int price = Convert.ToInt32(e.GetListSourceFieldValue("单价"));
                int unitsOnOrder = Convert.ToInt32(e.GetListSourceFieldValue("数量"));
                e.Value = price * unitsOnOrder;
            }
        }

        // 单价
        private void TableView_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "商品ID")
            {
                long l = (long)Purchases.GetCellValue(e.RowHandle, "商品ID");

                DbDataService dbDataService = new DbDataService();
                double? unitPrice = dbDataService.UnitPrice(l, Models.JinXiaoCunType.进货);
                Purchases.SetCellValue(e.RowHandle, "单价", unitPrice);
            }

        }
        // 总价 sum
        double zongjia_sum = 0;
        private void Purchases_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (((GridSummaryItem)e.Item).FieldName == "总价")
            {
                if (e.IsTotalSummary)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        zongjia_sum = 0;
                    }
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        if (e.FieldValue != null)
                        {
                            zongjia_sum += double.Parse(e.FieldValue.ToString());
                        }
                        e.TotalValue = zongjia_sum;
                    }
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        ((PurchaseViewModel)this.DataContext).应收 = zongjia_sum;
                    }
                }
            }
        }
    }
}
