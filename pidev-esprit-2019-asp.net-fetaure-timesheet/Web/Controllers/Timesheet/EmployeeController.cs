

using Data;
using Service.Employee;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers.Timesheet
{
    public class EmployeeController : Controller
    {
        static EmployeeServices employeeServices = new EmployeeServices();
        

        // GET: Employee
        public static IEnumerable<user> getListEmployeesByTeamID(long idTeam)
        {
            IEnumerable<user> employees = employeeServices.GetMany(user => user.team_id == idTeam);
            Console.WriteLine(employees);
            return employees;
        
        }

       

    }
}