﻿using System;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LinqTests;

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

        [TestMethod]
        public void Find_First_Employees_Salary_MoreThan_100_Default_Tom()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.AreEqual("Tom", employees.EltonFirst(e => e.MonthSalary > 100).Name);
        }

        [TestMethod]
        public void Single_Role_Manager_Return_One_Record()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.AreEqual(RoleType.Manager, employees.EltonSingle(employee => employee.Role == RoleType.Manager).Role);
        }

        [TestMethod]
        public void Single_Role_Manager_Return_One_Record1()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.AreEqual(RoleType.Manager, employees.Where(employee => employee.Role == RoleType.Manager).EltonSingle().Role);
        }

        [TestMethod]
        public void Distinct_Employees_Role()
        {
            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Andy", Role=RoleType.OP, MonthSalary=80, Age=22, WorkingYear=2.6}
            };

            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.EltonDistinct(new EmployeeComparer());
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Default_If_Empty()
        {
            var employees = RepositoryFactory.GetEmployees();
            var younger = employees.EltonWhere(e => e.Age <= 15).EltonDefaultIfEmpty(new Employee() { Name = "Lulu" });
            var expected = new List<Employee>()
            {
                new Employee()
                {
                    Name = "Lulu"
                }
            };

            expected.ToExpectedObject().ShouldEqual(younger.ToList());
        }

        [TestMethod]
        public void Contains_Test()
        {
            var colorBalls = RepositoryFactory.GetBalls();
            var luckyBall = new ColorBall() { Color = Color.Purple, Size = "S", Prize = 500 };

            Assert.IsTrue(colorBalls.EltonContains(luckyBall, new ColorBallComparer()));
        }

        [TestMethod]
        public void SequenceEqual_Test1()
        {
            var balls = RepositoryFactory.GetBalls();
            var moreBalls = RepositoryFactory.GetMoreBalls();

            Assert.IsFalse(balls.EltonSequenceEqual(moreBalls, new ColorBallComparer()));
        }

        [TestMethod]
        public void SequenceEqual_Test2()
        {
            var balls = RepositoryFactory.GetBalls();
            var anotherBalls = RepositoryFactory.GetAnotherBalls();

            Assert.IsFalse(balls.EltonSequenceEqual(anotherBalls, new ColorBallComparer()));
        }
    }
}

internal static class EltonLinq
{
    public static bool EltonSequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> target, IEqualityComparer<T> equalityComparer)
    {
        var enumeratorX = source.GetEnumerator();
        var enumeratorY = target.GetEnumerator();

        while (enumeratorX.MoveNext())
        {
            if (!enumeratorY.MoveNext() || !equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                return false;
        }

        return !enumeratorY.MoveNext();

        //while (enumeratorX.MoveNext() && enumeratorY.MoveNext())
        //{
        //    if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
        //        return false;
        //}

        //if (enumeratorX.MoveNext() || enumeratorY.MoveNext())
        //    return false;

        //return true;
    }

    public static bool EltonContains<T>(this IEnumerable<T> source, T target, IEqualityComparer<T> equalityComparer)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (equalityComparer.Equals(enumerator.Current, target))
                return true;
        }

        return false;
    }

    public static IEnumerable<TSource> EltonDistinct<TSource>(this IEnumerable<TSource> source,
        IEqualityComparer<TSource> equalityComparer)
    {
        var enumerator = source.GetEnumerator();
        var hashSet = new HashSet<TSource>(equalityComparer);
        while (enumerator.MoveNext())
        {
            if (hashSet.Add(enumerator.Current))
                yield return enumerator.Current;
        }
    }

    public static T EltonFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        throw new InvalidOperationException();
    }

    public static T EltonFirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        return default(T);
    }

    public static IEnumerable<T> EltonDefaultIfEmpty<T>(this IEnumerable<T> source, T defaultInput)
    {
        var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
            yield return defaultInput;
        else
        {
            do
            {
                yield return enumerator.Current;
            } while (enumerator.MoveNext());
        }
    }

    public static bool EltonAny<T>(this IEnumerable<T> source)
    {
        return source.GetEnumerator().MoveNext();
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

    public static TSource EltonSingle<TSource>(this IEnumerable<TSource> employees)
    {
        var enumerator = employees.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new Exception();
        TSource current = enumerator.Current;
        if (!enumerator.MoveNext())
            return current;

        throw new Exception();
    }

    public static TSource EltonSingle<TSource>(this IEnumerable<TSource> employees, Func<TSource, bool> predicate)
    {
        TSource e = default(TSource);
        foreach (var employee in employees)
        {
            if (predicate(employee))
            {
                if (e != null)
                {
                    throw new InvalidOperationException();
                }
                e = employee;
            }
        }

        return e == null ? throw new InvalidOperationException() : e;
    }

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