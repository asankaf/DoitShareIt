using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSH.Main.Web.Models;

namespace DSH.Main.Web.Repositories
{
    public class EmployeesRepository
    {
        public void Initialize()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>(2);
            EmployeeModel emp_1 = new EmployeeModel();
            emp_1.id = "emp1";
            emp_1.firstName = "Supun";
            emp_1.lastName = "Nakandala";
            emp_1.averageSales = "7382";

            employees.Add(emp_1);

            EmployeeModel emp_2 = new EmployeeModel();
            emp_2.id = "emp2";
            emp_2.firstName = "Naresh";
            emp_2.lastName = "Rathnakumara";
            emp_2.averageSales = "8982";

            employees.Add(emp_2);

            EmployeeModel emp_3 = new EmployeeModel();
            emp_3.id = "emp3";
            emp_3.firstName = "Manoj";
            emp_3.lastName = "Kumara";
            emp_3.averageSales = "3462";

            employees.Add(emp_3);

            HttpContext.Current.Session["Employees"] = employees;
        }

        public IList<EmployeeModel> GetEmployees()
        {
            return (List<EmployeeModel>)
                HttpContext.Current.Session["Employees"];
        }

        public void SetEmployees(IList<EmployeeModel> employees)
        {
            HttpContext.Current.Session["Employees"] = employees;
        }

    }
}