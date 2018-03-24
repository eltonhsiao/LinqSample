using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500);

            var expected = new List<Product>()
            {
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}

internal class WithoutLinq
{
    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary)
    {
        List<Product> productByPrice = new List<Product>();
        foreach (var p in products)
        {
            if (p.Price >= lowBoundary && p.Price <= highBoundary)
            {
                productByPrice.Add(p);
            }
        }

        return productByPrice;
    }
}

internal class YourOwnLinq
{
}