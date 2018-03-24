using System;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Find(p => p.IsTopSaleProduct());

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500_with_LINQ()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(p => p.Price >= 200 && p.Price <= 500 && p.Cost > 30).ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_employees_older_than_30()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.Find(p => p.IsOrderThanThirty());
        }
    }
}

internal static class WithoutLinq
{
    public static IEnumerable<T> Find<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var p in source)
        {
            if (predicate(p))
            {
                yield return p;
            }
        }
    }
}

internal class YourOwnLinq
{
}