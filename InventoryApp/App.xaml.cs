using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            //using (var db = new InventoryDB())
            //{
            //    var q =
            //        from c in db.GoodsStyles
            //        select c;

            //    foreach (var c in q)
            //        Console.WriteLine(c.Goods);
            //}
        }
    }
}
