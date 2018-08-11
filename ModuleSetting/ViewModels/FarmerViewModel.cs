using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuleSetting.ViewModels
{
    public class FarmerViewModel : BindableBase
    {
        public FarmerViewModel()
        {
            
        }
        private string fieldName;
        public string PropertyName
        {
            get { return fieldName; }
            set { SetProperty(ref fieldName, value); }
        }
        private DelegateCommand _fieldName;
        public DelegateCommand CommandName =>
            _fieldName ?? (_fieldName = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
           
        }
    }
}
