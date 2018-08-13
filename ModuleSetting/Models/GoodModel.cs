using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting.Models
{
    public class GoodModel : BindableBase
    {
        // 生产厂家
        private string manufacturer;
        public string Manufacturer
        {
            get { return manufacturer; }
            set { SetProperty(ref manufacturer, value); }
        }
        // 商品名称
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        // 是否高毒
        private string isHighToxicity;
        public string IsHighToxicity
        {
            get { return isHighToxicity; }
            set { SetProperty(ref isHighToxicity, value); }
        }
        // 种类(小)
        private string styleSmall;
        public string StyleSmall
        {
            get { return styleSmall; }
            set { SetProperty(ref styleSmall, value); }
        }
        // 种类(大)
        private string styleBig;
        public string StyleBig
        {
            get { return styleBig; }
            set { SetProperty(ref styleBig, value); }
        }
        // 单位(大)
        private string bigUnit;
        public string BigUint
        {
            get { return bigUnit; }
            set { SetProperty(ref bigUnit, value); }
        }
        // 单位(小)
        private string smallUnit;
        public string SmallUnit
        {
            get { return smallUnit; }
            set { SetProperty(ref smallUnit, value); }
        }
        // 内含量
        private int content;
        public int Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }
        // 农药登记证号
        private string identificationCode;
        public string IdentificationCode
        {
            get { return identificationCode; }
            set { SetProperty(ref identificationCode, value); }
        }
        // 主要成分
        private string mainIngredient;
        public string MainIngredient
        {
            get { return mainIngredient; }
            set { SetProperty(ref mainIngredient, value); }
        }
        // 箱重量
        private double boxWeight;
        public double BoxWeight
        {
            get { return boxWeight; }
            set { SetProperty(ref boxWeight, value); }
        }
        // 瓶重量

    }
}
