using LINQSamples.RepositoryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQSamples
{
  public class SamplesViewModel
  {
    
        #region Constructor
        public SamplesViewModel()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
            Sales = SalesOrderDetailRepository.GetAll();
        }
        #endregion

        #region Properties
    public bool UseQuerySyntax { get; set; }
    public List<Product> Products { get; set; }
    public List<SalesOrderDetail> Sales { get; set; }

    public string ResultText { get; set; }
    #endregion

        #region GetAllLooping
    /// <summary>
    /// Put all products into a collection via looping, no LINQ
    /// </summary>
    public void GetAllLooping()
    {
      List<Product> list = new List<Product>();
      foreach(Product prod in Products)
      {
          list.Add(prod);
      }
      ResultText = $"Total Products: {list.Count}";
    }
    #endregion

        #region GetAll
    /// <summary>
    /// Put all products into a collection using LINQ
    /// </summary>
    public void GetAll()
    {
      List<Product> list = new List<Product>();

      if (UseQuerySyntax) {
         list = (from prod in Products select prod).ToList();
        
      }
      else {
         list = Products.Select(prod => prod).ToList();
        
      }

      ResultText = $"Total Products: {list.Count}";
    }
    #endregion

        #region GetSingleColumn
    /// <summary>
    /// Select a single column
    /// </summary>
    public void GetSingleColumn()
    {
      StringBuilder sb = new StringBuilder(1024);
      List<string> list = new List<string>();

      if (UseQuerySyntax) {
         list.AddRange(from prod in Products select prod.Name);
      }
      else {
         list.AddRange(Products.Select(prod =>prod.Name));
      }

      foreach (string item in list) {
        sb.AppendLine(item);
      }

      ResultText = $"Total Products: {list.Count}" + Environment.NewLine + sb.ToString();
      Products.Clear();
    }
    #endregion

        #region GetSpecificColumns
    /// <summary>
    /// Select a few specific properties from products and create new Product objects
    /// </summary>
    public void GetSpecificColumns()
    {
      if (UseQuerySyntax) {
        //QuerySyntax
        Products = (from prod in Products
            select new Product
            {
                ProductID = prod.ProductID,
                Name = prod.Name,
                Size = prod.Size
            }).ToList();
       
      }
      else {
        // Method Syntax
        Products = Products.Select(prod => new Product
        {
            ProductID = prod.ProductID,
            Name = prod.Name,
            Size = prod.Size
        }).ToList();
      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

        #region AnonymousClass
    /// <summary>
    /// Create an anonymous class from selected product properties
    /// </summary>
    public void AnonymousClass()
    {
      StringBuilder sb = new StringBuilder(2048);

      if (UseQuerySyntax) {
        // Query Syntax
        var products = (from prod in Products
                        select new
                        {
                            Identifier = prod.ProductID,
                            ProductName = prod.Name,
                            ProductSize = prod.Size
                        }).ToList();
        // Loop through anonymous class
        foreach (var prod in products) {
          sb.AppendLine($"Product ID: {prod.Identifier}");
          sb.AppendLine($"   Product Name: {prod.ProductName}");
          sb.AppendLine($"   Product Size: {prod.ProductSize}");
        }
}
    else {
            // Method Syntax
            var products = Products.Select(prod => new
            {
                Identifier = prod.ProductID,
                ProductName = prod.Name,
                ProductSize = prod.Size
            }).ToList();
            // Loop through anonymous class
            foreach (var prod in products)
            {
                sb.AppendLine($"Product ID: {prod.Identifier}");
                sb.AppendLine($"   Product Name: {prod.ProductName}");
                sb.AppendLine($"   Product Size: {prod.ProductSize}");
            }
            }

      ResultText = sb.ToString();
      Products.Clear();
    }
    #endregion

        #region OrderBy
    /// <summary>
    /// Order products by Name
    /// </summary>
    public void OrderBy()
    {
      if (UseQuerySyntax) {
        // Query Syntax
        Products = (from prod in Products
                    orderby prod.Name
                    select prod).ToList();
    }
      else {
        // Method Syntax
        Products = Products.OrderBy(prod => prod.Name).ToList();
      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

        #region OrderByDescending Method
    /// <summary>
    /// Order products by name in descending order
    /// </summary>
    public void OrderByDescending()
    {
      if (UseQuerySyntax) {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name descending
                            select prod).ToList();
      }
      else {
                // Method Syntax
                Products = Products.OrderByDescending(prod => prod.Name).ToList();
      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

        #region OrderByTwoFields Method
    /// <summary>
    /// Order products by Color descending, then Name
    /// </summary>
    public void OrderByTwoFields()
    {
      if (UseQuerySyntax) {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Color descending,
                            prod.Name
                            select prod).ToList();
      }
      else {
                // Method Syntax
                Products = Products.OrderByDescending(prod => prod.Color)
                            .ThenBy(prod => prod.Name).ToList();
      }

      ResultText = $"Total Products: {Products.Count}";
    }
        #endregion  

        #region Where Expression Method
            /// <summary>
            /// select some products its Name starts with L
            /// </summary>
            public void WhereExpression()
            {
                string search = "Red";
            
                if (UseQuerySyntax)
                {
                    // Query Syntax
                    Products = (from prod in Products
                                select prod).ByColor(search).ToList();
                }
                else
                {
                    // Method Syntax
                    Products = Products.ByColor(search).ToList();
                }

                ResultText = $"Total Products: {Products.Count}";
            }
            #endregion

        #region Where Expression with two fields Method
            /// <summary>
            /// select some products its Name starts with L and the cost = 100 
            /// </summary>
            public void WhereTwoFields()
            {
                string search = "L";
                decimal cost = 100;
                if (UseQuerySyntax)
                {
                    // Query Syntax
                    Products = (from prod in Products
                                where prod.Name.StartsWith(search)&&
                                      prod.StandardCost > cost 
                                select prod).ToList();
                }
                else
                {
                    // Method Syntax
                    Products = Products.Where(prod => prod.Name.StartsWith(search)&&
                                prod.StandardCost > 100).ToList();
                }

                ResultText = $"Total Products: {Products.Count}";
            }
            #endregion

        #region FirstMatched
        /// <summary>
        /// Select the first matched
        /// </summary>
        public void First()
        {
            string search = "Red";
            Product value;

            try
            {
                if (UseQuerySyntax)
                {
                    //Query Syntax
                    value = (from prod in Products
                             select prod).First(prod => prod.Color == search);
                }
                else
                {
                    //Method Syntax
                    value = Products.First(prod => prod.Color == search);
                }

                ResultText = $"Found: {value}";
            }
            catch
            {
                ResultText = $"Not Found";
            }
            
            Products.Clear();
        }

        public void FirstOrDefault()
        {
            string search = "Red";
            Product value;

            
            if (UseQuerySyntax)
            {
                //Query Syntax
                value = (from prod in Products
                            select prod).FirstOrDefault(prod => prod.Color == search);
            }
            else
            {
                //Method Syntax
                value = Products.FirstOrDefault(prod => prod.Color == search);
            }
            
            if(value == null)
            {
                ResultText = $"Not Found";
            }
            else
            {
                ResultText = $"Found: {value}";
            }

            Products.Clear();
        }
        #endregion

        #region LastMatched
            /// <summary>
            /// Select the Last matched
            /// </summary>
            public void Last()
            {
                string search = "Red";
                Product value;

                try
                {
                    if (UseQuerySyntax)
                    {
                        //Query Syntax
                        value = (from prod in Products
                                 select prod).Last(prod => prod.Color == search);
                    }
                    else
                    {
                        //Method Syntax
                        value = Products.Last(prod => prod.Color == search);
                    }

                    ResultText = $"Found: {value}";
                }
                catch
                {
                    ResultText = $"Not Found";
                }

                Products.Clear();
            }

            public void LastOrDefault()
            {
                string search = "Red";
                Product value;


                if (UseQuerySyntax)
                {
                    //Query Syntax
                    value = (from prod in Products
                             select prod).LastOrDefault(prod => prod.Color == search);
                }
                else
                {
                    //Method Syntax
                    value = Products.LastOrDefault(prod => prod.Color == search);
                }

                if (value == null)
                {
                    ResultText = $"Not Found";
                }
                else
                {
                    ResultText = $"Found: {value}";
                }

                Products.Clear();
            }
            #endregion

        #region Single Matched
        /// <summary>
        /// Select the only item matched
        /// </summary>
        public void Single()
        {
            int search = 706;
            Product value;

            try
            {
                if (UseQuerySyntax)
                {
                    //Query Syntax
                    value = (from prod in Products
                             select prod).Single(prod => prod.ProductID == search);
                }
                else
                {
                    //Method Syntax
                    value = Products.Single(prod => prod.ProductID == search);
                }

                ResultText = $"Found: {value}";
            }
            catch
            {
                ResultText = $"Not Found";
            }

            Products.Clear();
        }

        public void SingleOrDefault()
        {
            int search = 706;
            Product value;


            if (UseQuerySyntax)
            {
                //Query Syntax
                value = (from prod in Products
                         select prod).SingleOrDefault(prod => prod.ProductID == search);
            }
            else
            {
                //Method Syntax
                value = Products.SingleOrDefault(prod => prod.ProductID == search);
            }

            if (value == null)
            {
                ResultText = $"Not Found";
            }
            else
            {
                ResultText = $"Found: {value}";
            }

            Products.Clear();
        }
        #endregion

        #region ForEach Method
        /// <summary>
        /// ForEach allows you to iterate over a collection to perform assignments within each object.
        /// In this sample, assign the Length of the Name property to a property called NameLength
        /// When using the Query syntax, assign the result to a temporary variable.
        /// </summary>
        public void ForEach()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            let temp = prod.NameLength = prod.Name.Length
                            select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products.ForEach(prod => prod.NameLength = prod.Name.Length);
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region ForEachCallingMethod Method
        /// <summary>
        /// Iterate over each object in the collection and call a method to set a property
        /// This method passes in each Product object into the SalesForProduct() method
        /// In the SalesForProduct() method, the total sales for each Product is calculated
        /// The total is placed into each Product objects' ResultText property
        /// </summary>
        public void ForEachCallingMethod()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            let temp = prod.TotalSales = SalesForProduct(prod)
                            select prod ).ToList();
            }
            else
            {
                // Method Syntax
                Products.ForEach(prod => prod.TotalSales = SalesForProduct(prod));
            }

            ResultText = $"Total Products: {Products.Count}";
        }

        /// <summary>
        /// Helper method called by LINQ to sum sales for a product
        /// </summary>
        /// <param name="prod">A product</param>
        /// <returns>Total Sales for Product</returns>
        private decimal SalesForProduct(Product prod)
        {
            return Sales.Where(sale => sale.ProductID == prod.ProductID)
                        .Sum(sale => sale.LineTotal);
        }
        #endregion

        #region Take Method
        /// <summary>
        /// Use Take() to select a specified number of items from the beginning of a collection
        /// </summary>
        public void Take()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name
                            select prod)
                            .Take(5).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name)
                            .Take(5).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region TakeWhile Method
        /// <summary>
        /// Use TakeWhile() to select a specified number of items from the beginning of a collection based on a true condition
        /// </summary>
        public void TakeWhile()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name
                            select prod)
                            .TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name)
                            .TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region Skip Method
        /// <summary>
        /// Use Skip() to move past a specified number of items from the beginning of a collection
        /// </summary>
        public void Skip()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name
                            select prod)
                            .Skip(20).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).Skip(20).ToList();    
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region SkipWhile Method
        /// <summary>
        /// Use SkipWhile() to move past a specified number of items from the beginning of a collection based on a true condition
        /// </summary>
        public void SkipWhile()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name
                            select prod)
                            .SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name)
                            .SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

        #region Distinct
        /// <summary>
        /// The Distinct() operator finds all unique values within a collection
        /// In this sample you put distinct product colors into another collection using LINQ
        /// </summary>
        public void Distinct()
        {
            List<string> colors = new List<string>();

            if (UseQuerySyntax)
            {
                // Query Syntax
                colors = (from prod in Products
                          select prod.Color).Distinct().ToList();
            }
            else
            {
                // Method Syntax
                colors = Products.Select(prod => prod.Color)
                            .Distinct().ToList();
            }

            // Build string of Distinct Colors
            foreach (var color in colors)
            {
                Console.WriteLine($"Color: {color}");
            }
            Console.WriteLine($"Total Colors: {colors.Count}");

            // Clear products
            Products.Clear();
        }
        #endregion

        #region All
        /// <summary>
        /// Use All() method to check if all elements in the collection satisfies a specific ceriteria or condition
        /// </summary>
        public void All()
        {
            string search = " ";
            bool value;
            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                          select prod).All(prod => prod.Name.Contains(search));
            }
            else
            {
                // Method Syntax
                value = Products.All(prod => prod.Name.Contains(search));
            }
            ResultText = $"Do all Name Properties contain a '{search}' ? {value}";
            Products.Clear();
        }
        #endregion

        #region Any
        /// <summary>
        /// Use Any() method to check if any elements in the collection satisfies a specific ceriteria or condition
        /// </summary>
        public void Any()
        {
            string search = "z";
            bool value;
            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                         select prod).Any(prod => prod.Name.Contains(search));
            }
            else
            {
                // Method Syntax
                value = Products.Any(prod => prod.Name.Contains(search));
            }
            ResultText = $"Do all Name Properties contain a '{search}'? {value}";
            Products.Clear();
        }
        #endregion

        #region LINQContains
        /// <summary>
        /// Use Contains() method to check if a collection contains a specific element
        /// </summary>
        public void LINQContains()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6 };
            bool value;
            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from num in numbers
                         select num).Contains(3);
            }
            else
            {
                // Method Syntax
                value = numbers.Contains(3);
            }
            ResultText = $"Is the number in collection ? {value}";
            Products.Clear();
        }
        #endregion

        #region LINQContainsUsingComparer
        /// <summary>
        /// Use Contains() method to check if a collection contains a specific 
        /// object comparing to some property
        /// </summary>
        public void LINQContainsUsingComparer()
        {
            int search = 744;
            bool value;
            ProductIdComparer pc = new ProductIdComparer();
            Product prodToFind = new Product { ProductID = search };
            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                         select prod).Contains(prodToFind, pc);
            }
            else
            {
                // Method Syntax
                value = Products.Contains(prodToFind,pc);
            }
            ResultText = $"Product ID : {search} is in products collection? {value}";
            Products.Clear();
        }
        #endregion

        #region SequenceEqualIntegers
        /// <summary>
        /// Use SequenceEqual() method to check if two collections are equal or not
        /// </summary>
        public void SequenceEqualIntegers()
        {
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5, 6 };
            bool value;
            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from num in list1
                         select num).SequenceEqual(list2);
            }
            else
            {
                // Method Syntax
                value = list1.SequenceEqual(list2);
            }
            if(value)
            {
                ResultText = $"the two collections are equal";
            }
            else
            {
                ResultText = $"the two collections are NOT equal";
            }
          
        }

        public void SequenceEqualProducts()
        {
            ProductIdComparer pc = new ProductIdComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            bool value;

            //to test the not equal case

            //list2.RemoveAt(0);

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in list1
                         select prod).SequenceEqual(list2, pc);
            }
            else
            {
                // Method Syntax
                value = list1.SequenceEqual(list2,pc);
            }
            if (value)
            {
                ResultText = $"the two collections are equal";
            }
            else
            {
                ResultText = $"the two collections are NOT equal";
            }

            Products.Clear();
        }
        #endregion

        #region Except
        /// <summary>
        /// Use Except() method to return the items that are exist in one collection and ont exist in the other collection
        /// </summary>
        public void ExceptIntegers()
        {
            List<int> exceptions;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };
            
            if (UseQuerySyntax)
            {
                // Query Syntax
                exceptions = (from num in list1
                         select num).Except(list2).ToList();
            }
            else
            {
                // Method Syntax
                exceptions = list1.Except(list2).ToList();
            }

            ResultText =string.Empty;
            foreach (var item in exceptions)
                ResultText += "Number: " + item + Environment.NewLine;


            Products.Clear();
        }

        public void ExceptProducts()
        {
            
            ProductIdComparer pc = new ProductIdComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();


            //to test the not equal case

            list2.RemoveAll(prod => prod.Color == "Black");

            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in list1
                         select prod).Except(list2, pc).ToList();
            }
            else
            {
                // Method Syntax
                Products = list1.Except(list2, pc).ToList();
            }
            ResultText = $"Total products : {Products.Count()}";

            Products.Clear();
        }
        #endregion

        #region Intersect
        /// <summary>
        /// Use Entersect() method to return the items that are exist both collections
        /// </summary>
        public void IntersectIntegers()
        {
            List<int> exceptions;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };

            if (UseQuerySyntax)
            {
                // Query Syntax
                exceptions = (from num in list1
                              select num).Intersect(list2).ToList();
            }
            else
            {
                // Method Syntax
                exceptions = list1.Intersect(list2).ToList();
            }

            ResultText = string.Empty;
            foreach (var item in exceptions)
                ResultText += "Number: " + item + Environment.NewLine;


            Products.Clear();
        }

        public void IntersectProducts()
        {

            ProductIdComparer pc = new ProductIdComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();


            //to test the not equal case

            list2.RemoveAll(prod => prod.Color == "Black");

            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in list1
                            select prod).Intersect(list2, pc).ToList();
            }
            else
            {
                // Method Syntax
                Products = list1.Intersect(list2, pc).ToList();
            }
            ResultText = $"Total products : {Products.Count()}";

            Products.Clear();
        }
        #endregion

        #region Union with Union
        /// <summary>
        /// Use Union() method to return all the items that are exist in the both collections
        /// </summary>
        public void LINQUnionIntergers()
        {
            List<int> union;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };

            if (UseQuerySyntax)
            {
                // Query Syntax
                union = (from num in list1
                              select num).Union(list2).ToList();
            }
            else
            {
                // Method Syntax
                union = list1.Union(list2).ToList();
            }

            ResultText = string.Empty;
            foreach (var item in union)
                ResultText += "Number: " + item + Environment.NewLine;

        }

        public void LINQUinonProducts()
        {

            ProductIdComparer pc = new ProductIdComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();


            //to test the not equal case

            list2.RemoveAll(prod => prod.Color == "Black");

            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in list1
                            select prod).Union(list2, pc).ToList();
            }
            else
            {
                // Method Syntax
                Products = list1.Union(list2, pc).ToList();
            }
            ResultText = $"Total products : {Products.Count()}";

            Products.Clear();
        }
        #endregion

        #region Union with Conacat
        /// <summary>
        /// Use Union() method to return all the items that are exist in the both collections
        /// </summary>
        public void LINQConcatIntergers()
        {
            List<int> union;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };

            if (UseQuerySyntax)
            {
                // Query Syntax
                union = (from num in list1
                         select num).Concat(list2).ToList();
            }
            else
            {
                // Method Syntax
                union = list1.Concat(list2).ToList();
            }

            ResultText = string.Empty;
            foreach (var item in union)
                ResultText += "Number: " + item + Environment.NewLine;

        }

        public void LINQConcatProducts()
        {

            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();

            //to test the not equal case

            list2.RemoveAll(prod => prod.Color == "Black");

            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in list1
                            select prod).Concat(list2).ToList();
            }
            else
            {
                // Method Syntax
                Products = list1.Concat(list2).ToList();
            }
            ResultText = $"Total products : {Products.Count()}";

            Products.Clear();
        }
        #endregion

        #region Inner Join
        /// <summary>
        /// Use join() method to join two collections base on some ceriteria.
        /// </summary>
        public void InnerJoin()
        {
            StringBuilder sb = new StringBuilder(2084);
            int count = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
                var query = (from prod in Products
                             join sale in Sales
                             on prod.ProductID equals sale.ProductID
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Size,
                                 prod.Color,
                                 prod.StandardCost,
                                 sale.SalesOrderID,
                                 sale.LineTotal,
                                 sale.UnitPrice,
                                 sale.OrderQty
                             }).ToList();

                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }
                    

            }
            else
            {
                // Method Syntax
                var query = Products.Join(Sales, prod => prod.ProductID,
                                sale => sale.ProductID,
                                (prod, sale) => new
                                {
                                    prod.ProductID,
                                    prod.Name,
                                    prod.Size,
                                    prod.Color,
                                    prod.StandardCost,
                                    sale.SalesOrderID,
                                    sale.LineTotal,
                                    sale.UnitPrice,
                                    sale.OrderQty

                                });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }
            }
            
                ResultText =sb.ToString()+ Environment.NewLine +"Total Sales : "+count;

        }
        #endregion

        #region Inner Join with two fields
        /// <summary>
        /// Use join() method to join two collections base on some ceriteria.
        /// </summary>
        public void InnerJoinTwoFields()
        {
            StringBuilder sb = new StringBuilder(2084);
            short qty = 6;
            int count = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
                var query = (from prod in Products
                             join sale in Sales on
                             new { prod.ProductID , Qty = qty}
                             equals 
                             new { sale.ProductID, Qty = sale.OrderQty }
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Size,
                                 prod.Color,
                                 prod.StandardCost,
                                 sale.SalesOrderID,
                                 sale.LineTotal,
                                 sale.UnitPrice,
                                 sale.OrderQty
                             }).ToList();

                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }


            }
            else
            {
                // Method Syntax
                var query = Products.Join(Sales, prod => new { prod.ProductID , Qty = qty},
                                sale => new { sale.ProductID , Qty = sale.OrderQty},
                                (prod, sale) => new
                                {
                                    prod.ProductID,
                                    prod.Name,
                                    prod.Size,
                                    prod.Color,
                                    prod.StandardCost,
                                    sale.SalesOrderID,
                                    sale.LineTotal,
                                    sale.UnitPrice,
                                    sale.OrderQty

                                });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }
            }

            ResultText = sb.ToString() + Environment.NewLine + "Total Sales : " + count;

        }
        #endregion

        #region Group Join
        public void GroupJoin()
        {
            StringBuilder sb = new StringBuilder(2084);
            IEnumerable<ProductSales> grouped;
            int count = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
                grouped = (from prod in Products
                             join sale in Sales on
                             prod.ProductID equals sale.ProductID
                             into sales
                             select new ProductSales
                             {
                                 Product = prod,
                                 Sales = sales
                             });

            }
            else
            {
                // Method Syntax
                grouped = Products.GroupJoin(Sales ,prod => prod.ProductID, 
                                sale => sale.ProductID,
                                (prod, sale) => new ProductSales
                                {
                                    Product = prod,
                                    Sales = sale.ToList()
                                });
            }

            foreach (var ps in grouped)
            {
                
                sb.AppendLine($"Product : {ps.Product}");
                if(ps.Sales.Count() >0)
                {
                    sb.AppendLine("    ** Sales **    ");
                    foreach(var sale in ps.Sales)
                    {
                        sb.AppendLine($"     SalesOrder : {sale.SalesOrderID}");
                        sb.AppendLine($"     Qty : {sale.OrderQty}");
                        sb.AppendLine($"     Total : {sale.LineTotal}");
                    }
                }
                else
                {
                    sb.AppendLine("    ** No Sales **     ");
                }
                sb.AppendLine("");              
            }
            ResultText = sb.ToString() + Environment.NewLine + "Total Sales : " + count;

        }
        #endregion

        #region Left Outer Join
        public void LeftOuterJoin()
        {
            StringBuilder sb = new StringBuilder(2084);
            int count = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
               var query = (from prod in Products
                           join sale in Sales on
                           prod.ProductID equals sale.ProductID
                           into sales
                           from sale in sales.DefaultIfEmpty()
                           select new 
                           {
                               prod.ProductID,
                               prod.Name,
                               prod.Size,
                               prod.Color,
                               prod.StandardCost,
                               sale?.SalesOrderID,
                               sale?.LineTotal,
                               sale?.UnitPrice,
                               sale?.OrderQty
                           }).OrderBy(ps => ps.Name);
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }

            }
            else
            {
                // Method Syntax
                var query = Products.SelectMany(sale =>Sales.Where(s => sale.ProductID == s.ProductID)
                                            .DefaultIfEmpty(),
                                            (prod, sale) => new
                                            {
                                                prod.ProductID,
                                                prod.Name,
                                                prod.Size,
                                                prod.Color,
                                                prod.StandardCost,
                                                sale?.SalesOrderID,
                                                sale?.LineTotal,
                                                sale?.UnitPrice,
                                                sale?.OrderQty
                                            }).OrderBy(ps => ps.Name);

                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order : {item.SalesOrderID}");
                    sb.AppendLine($"Product ID : {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size  : {item.Size}");
                    sb.AppendLine($"Order Qty : {item.OrderQty}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");

                }

            }
            ResultText = sb.ToString() + Environment.NewLine + "Total Sales : " + count;

        }
        #endregion
    }

}
