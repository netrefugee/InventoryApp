using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting.Models
{
    public enum State
    {
        Insert,
        Update,
        None
    }
    public class CurrentState : BindableBase
    {
        private State stateNow;
        public State StateNow
        {
            get { return stateNow; }
            set { SetProperty(ref stateNow, value); }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set { SetProperty(ref info, value); }
        }


    }
}
