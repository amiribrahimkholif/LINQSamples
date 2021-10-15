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

    }
}
