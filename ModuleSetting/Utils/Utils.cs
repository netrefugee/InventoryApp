using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSetting.Utils
{
    class Utils
    {
        public static bool IsNullOrEmpty(string s)
        {
            if (s == null) { return true; }
            return string.IsNullOrEmpty(s.Trim());
        }
        public static bool IsNullOrEmpty(long? s)
        {
            if (s == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNullOrEmpty(double? s)
        {
            if (s == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNullOrEmpty(int? s)
        {
            if (s == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public static bool removeSpace(Type type)
        //{
        //    type.Attributes
        //}
        /// <summary>
        /// 获取某个对象的[公有属性]的名称,类型,值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static (bool isExistSpace, string name) IsExistSpace<T>(T obj)
        {
            if (obj == null)
            {
                return (false, "");
            }
            Type t = obj.GetType();//获得该类的Type
                                   //再用Type.GetProperties获得PropertyInfo[],然后就可以用foreach 遍历了

            foreach (PropertyInfo pi in t.GetProperties())
            {
                var name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                var value = pi.GetValue(obj, null);//用pi.GetValue获得值
                var type = value?.GetType() ?? typeof(object);//获得属性的类型

                if (type.Equals(typeof(string)))
                {
                    string values = value.ToString();
                    if (values[0] == ' ' || values[values.Length - 1] == ' ')
                    {
                        return (true, name);
                    }
                }

            }
            return (false, "");

        }
    }
}
