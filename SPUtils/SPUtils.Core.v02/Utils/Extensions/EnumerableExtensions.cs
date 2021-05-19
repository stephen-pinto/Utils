using System;
using System.Collections.Generic;
using System.Text;

namespace SPUtils.Core.v02.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static CustListType ToCustomList<CustListType, ElementType>(this IEnumerable<ElementType> elements)
            where CustListType : List<ElementType>
        {
            CustListType list = Activator.CreateInstance<CustListType>();
            list.AddRange(elements);
            return list;
        }
    }
}
