using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples
{
    public class ProductIdComparer : EqualityComparer<Product>
    {
        public override bool Equals(Product x, Product y)
        {
            return (x.ProductID == y.ProductID&&
                    x.Name == y.Name&&
                    x.StandardCost == y.StandardCost&&
                    x.Size == y.Size&&
                    x.NameLength == y.NameLength&&
                    x.ListPrice == y.ListPrice);
        }
        public override int GetHashCode([DisallowNull] Product obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }
}
