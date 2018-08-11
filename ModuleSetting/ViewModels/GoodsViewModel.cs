using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace ModuleSetting.ViewModels
{
    public class GoodsViewModel : BindableBase
    {
        public GoodsViewModel()
        {
          
        }
        #region 添加商品
        private DelegateCommand _runDialogCommand;
        public DelegateCommand RunDialogCommand =>
            _runDialogCommand ?? (_runDialogCommand = new DelegateCommand(ExecuteRunDialogCommand));

        private async void ExecuteRunDialogCommand()
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new SampleDialog
            {
                DataContext = new SampleDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
 
        #endregion
    }
}
