using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Xml.Services
{
    class XmlDataUtil
    {

        public static string Path = "Data/ModulePurchaseSetting.xml";
        /// <summary>
        /// 读取id等于某一数值的节点下的值
        /// </summary>
        public static string QueryElement(string attrValue)
        {
            var query =

          from el in (from ele in XElement.Load(Path).Descendants()
                      where (string)ele.Attribute("id") == attrValue
                      select ele).Elements()

          select el.Value;

            return query.First();
        }
        /// <summary>
        /// 读取id等于某一数值的节点下面的所有值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="element"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static IEnumerable<string> QueryElementsAllowToElementById(string attrValue)
        {
            var query =

                from el in (from ele in XElement.Load(Path).Descendants()
                 where (string)ele.Attribute("id") == attrValue
                 select ele).Elements()
                 
                select el.Value;

            return query;
        }
        /// <summary>
        /// 查询符合某元素的id值
        /// </summary>
        /// <param name="elementValue"></param>
        /// <returns></returns>
        public static IEnumerable<string> QueryElementsById(string elementValue)
        {
            var query =
                 from ele in XElement.Load(Path).Descendants(elementValue)
                 select ele.Attribute("id").Value;
    
            return query ;
        }


    }
}
