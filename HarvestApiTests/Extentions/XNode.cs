using System.Linq;

namespace System.Xml.XPath
{
    public static class XNode
    {
        public static int XPathCountElements(this Linq.XNode node, string expression)
        {
            return node == null ? 0 : node.XPathSelectElements(expression).Count();
        }
    }
}