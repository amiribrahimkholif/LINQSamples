using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.RepositoryClasses
{
    public static class ProductHelper
    {
        #region My Custom Extension Method
        public static IEnumerable<Product> ByColor(this IEnumerable<Product> query, string color)
        {
            return query.Where(prod => prod.Color == color);
        }
        #endregion
    }
}
