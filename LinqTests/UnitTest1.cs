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
            var actual = products.EltonWhere(p => p.Price >= 200 && p.Price <= 500 && p.Cost > 30);

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
            var actual = products.EltonWhere(p => p.Price >= 200 && p.Price <= 500 && p.Cost > 30).ToList();

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
            var actual = employees.EltonWhere(p => p.Age > 30);

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
            var actual = employees.EltonWhere((e, index) => e.Age > 30 && index > 1);

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

            //trace callback log
            //foreach (var a in actual)
            //{
            //    Console.WriteLine(a);
            //}
            var expected = new List<string>
            {
                "OP:Andy",
                "Engineer:Frank"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void take_first_2_employees()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonTake(2);

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6}
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void skip_6_employees()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonSkip(6);

            var expected = new List<Employee>()
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6}
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void employees_salary()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonSum(p => p.MonthSalary, 3);

            var expected = new List<int>()
            {
                620,
                540,
                370
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void take_top_2_employees_salary_bigger_than_150()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonTakeWhile(2, e => e.MonthSalary > 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6}
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void skip_3_employees_salary_lower_than_150()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonSkipWhile(3, e => e.MonthSalary < 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Any_Employees_Salary_MoreThan_500()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonAny(x => x.MonthSalary > 500);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void All_Employees_Salary_MoreThan_200()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonAll(x => x.MonthSalary > 200);
            Assert.IsFalse(actual);
        }
    }
}

internal static class WithoutLinq
{
    public static bool EltonAny<T>(this IEnumerable<T> Source)
    {
        return Source.GetEnumerator().MoveNext();
    }

    public static bool EltonAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return true;
            }
        }

        return false;
        //foreach (var s in Source)
        //{
        //    if (predicate(s))
        //    {
        //        return true;
        //    }
        //}

        //return false;
    }

    public static bool EltonAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!predicate(enumerator.Current))
            {
                return false;
            }
        }

        return true;

        //foreach (var s in source)
        //{
        //    if (!predicate(s))
        //    {
        //        return false;
        //    }
        //}

        //return true;
    }

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
            //Console.WriteLine("Where execute");
            if (predicate(s))
            {
                //Console.WriteLine("Where Find then Return");
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
            //Console.WriteLine("Select Find then Return");
            yield return selector(item);
        }
    }

    public static IEnumerable<T> EltonTake<T>(this IEnumerable<T> source, int count)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (count <= 0)
            {
                yield break;
            }

            yield return enumerator.Current;
            count--;
        }
    }

    public static IEnumerable<T> EltonSkip<T>(this IEnumerable<T> source, int count)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (count <= 0)
                yield return enumerator.Current;
            else
                count--;
        }
    }

    public static IEnumerable<int> EltonSum<T>(this IEnumerable<T> source, Func<T, int> sum, int count)
    {
        int index = 0;

        while (index < source.Count())
        {
            yield return source.EltonSkip(index).EltonTake(count).Sum(sum);
            index += count;
        }
    }

    public static IEnumerable<T> EltonTakeWhile<T>(this IEnumerable<T> source, int count, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        int index = 0;
        while (index < count && enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                yield return enumerator.Current;
                index++;
            }
        }
    }

    public static IEnumerable<T> EltonSkipWhile<T>(this IEnumerable<T> source, int count, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        int index = 0;
        while (enumerator.MoveNext())
        {
            if (index < count && predicate(enumerator.Current))
            {
                index++;
            }
            else
            {
                yield return enumerator.Current;
            }
        }
    }
}