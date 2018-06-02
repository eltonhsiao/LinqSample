using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqTests
{
    internal class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Role == y.Role;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Role.GetHashCode();
        }
    }

    internal class Employee
    {
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public int MonthSalary { get; set; }
        public int Age { get; set; }
        public double WorkingYear { get; set; }
    }
}