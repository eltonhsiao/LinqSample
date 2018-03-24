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

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_employees_older_than_30_and_index_bigger_than_1()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonWhere((e, index) => e.IsOrderThanThirty() && index > 1);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void replace_http_to_https()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.Replace(url => url.Replace("http://", "https://"));

            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void url_length()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.EltonSelect(url => url.Length);

            var expected = new List<int>
            {
                19,
                20,
                19,
                17
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_age_younger_than_25_employees()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonWhere(x => x.Age < 25).EltonSelect(x => $"{x.Role}:{x.Name}");

            var expected = new List<string>
            {
                "OP:Andy",
                "Engineer:Frank"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }
}

internal static class WithoutLinq
{
    public static IEnumerable<T> Find<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var s in source)
        {
            if (predicate(s))
            {
                yield return s;
            }
        }
    }

    public static IEnumerable<T> Find<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
    {
        int index = 0;
        foreach (var s in source)
        {
            if (predicate(s, index))
            {
                yield return s;
            }

            index++;
        }
    }

    public static IEnumerable<string> Replace(this IEnumerable<string> urls, Func<string, string> act)
    {
        foreach (var url in urls)
        {
            yield return act(url);
        }
    }

    public static IEnumerable<int> GetLength(this IEnumerable<string> urls, Func<string, int> act)
    {
        foreach (var url in urls)
        {
            yield return act(url);
        }
    }
}

internal static class YourOwnLinq
{
    public static IEnumerable<T> EltonWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var s in source)
        {
            if (predicate(s))
            {
                yield return s;
            }
        }
    }

    public static IEnumerable<T> EltonWhere<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
    {
        int index = 0;
        foreach (var s in source)
        {
            if (predicate(s, index))
            {
                yield return s;
            }

            index++;
        }
    }

    public static IEnumerable<TResult> EltonSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }
}