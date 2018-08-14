using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
