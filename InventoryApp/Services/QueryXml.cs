using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryApp.Services
{
    class QueryXml
    {
        /// <summary>
        /// 查询某一节点的值
        /// </summary>
        public static string QueryElement(string path, string element)
        {
            XDocument doc = XDocument.Load(path);
            var query = doc.Descendants(element);
            return query.First().Value;


        }
        /// <summary>
        /// 读取某一节点下的所有值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="element"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> QueryElementByAttr(string path, string element, string attr, string attrValue)
        {
            XElement root = XElement.Load(path);
            IEnumerable<XElement> address =
                from el in root.Descendants(element)
                where (string)el.Attribute(attr) == attrValue
                select el;
            foreach (XElement el in address)
                Console.WriteLine(el);
            return address;
        }

    }
}
