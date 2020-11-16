using Data;
using Newtonsoft.Json;
using Service.Employee;
using Service.Timesheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.TimesheetModels;

namespace Web.Controllers.Timesheet
{
    public class TimesheetController : Controller
    {
        static EmployeeServices employeeService = new EmployeeServices();
        static TicketServices ticketSerices = new TicketServices();
        static IEnumerable<user> employees = employeeService.GetMany();
        static IEnumerable<Ticket> tickets = ticketSerices.GetMany();
        TeamServices teamServices = new TeamServices();
        public static int monthNumber = DateTime.Now.Month;

        // GET: Timesheet
        public ActionResult Report()
        {
            List<string> months = new List<string>();
            months.Add("Janvier");
            months.Add("Février");
            months.Add("Mars");
            months.Add("Avril");
            months.Add("Mai");
            months.Add("Juin");
            months.Add("Juillet");
            months.Add("Août");
            months.Add("Septembre");
            months.Add("Octobre");
            months.Add("Novembre");
            months.Add("Décembre");
            ViewBag.months = months;
            ViewBag.team = teamServices.GetMany().ToList();
            ViewBag.monthNumber = monthNumber;
            return View();
        }

        
        public static string getCurrentMonth()
        {
            
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            return date;
        }

        public ActionResult Month(string month)
        {

            monthNumber = System.DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
           
            //String mounth = System.Web.HttpContext.Current.Request["mounth"];
            return RedirectToAction("Report");
        }

        [HttpPost]
        public  string  dateChanged()
        {
            //string date = collection.GetValue("date").ToString();
            string date = Request.Form["date"];
          
            return date;
        }
        

        public static IEnumerable<user> getEmployeeByTeamID(long id)
        {
            IEnumerable<user> Teamemployees = employees.Where(e => e.team_id.Equals(id)).ToList();
            return Teamemployees; 
        }

        public static int getNbEmployeeByTeamID(long id)
        {
            int nbr = employees.Where(e => e.team_id.Equals(id)).ToList().Count()+1;
            return nbr;
        }

        public static Boolean IsBewteenTwoDates(DateTime? dt, DateTime start, DateTime end)
        {
            return ((dt >= start) && (dt <= end));
        }


        [HttpPost]
        public static double getEmployeeWorkedHoursByWeek(long id, string week)
        {
            DateTime date = new DateTime(DateTime.Now.Year, monthNumber, 1);
            DateTime start = new DateTime();
            DateTime end = new DateTime();


            if ((date.Month == 2)&&(week.Equals("week4"))) {
                start = new DateTime(date.Year, date.Month, 24);
                end = new DateTime(date.Year, date.Month, 28);
            }
            else
            {

            

            switch (week)
            {
                case "week1":
                    start = new DateTime(date.Year, date.Month, 1);
                    end = new DateTime(date.Year, date.Month, 8);
                    break;
                case "week2":
                    start = new DateTime(date.Year, date.Month, 9);
                    end = new DateTime(date.Year, date.Month, 16);
                    break;
                case "week3":
                    start = new DateTime(date.Year, date.Month, 17);
                    end = new DateTime(date.Year, date.Month, 23);
                    break;
                case "week4":
                    start = new DateTime(date.Year, date.Month, 24);
                    end = new DateTime(date.Year, date.Month, 30);
                    break;
                default:
                    break;
            }
            }

            double nbr = (double) tickets.Where(e => ( (e.employee_id.Equals(id)) && (IsBewteenTwoDates(e.dateEnd, start, end)))).Select(t => t.duration).Sum();
            
            return nbr;
        }


        public static double getTotalWorkedHoursByWeek(string week)
        {

           // double nbr = (double) tickets.Where(e => e.employee_id.Equals(id)).Select(t => t.duration).Sum();
            return 40;
        }


        public static double getWorkedHoursPerMonthByEmpID(long id, DateTime date)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (date.Month == 2)
            {
                 start = new DateTime(date.Year, date.Month, 24);
                 end = new DateTime(date.Year, date.Month, 28);
            }
            else
            {
                 start = new DateTime(date.Year, date.Month, 1);
                 end = new DateTime(date.Year, date.Month, 30);
            }
            double nbr = (double)tickets.Where(e => e.employee_id.Equals(id) & IsBewteenTwoDates(e.dateEnd, start, end))
                                         .Select(t => t.duration).Sum();
            return nbr;

        }

        public static user getEmployeeOfTheMonthByTeam(long id)
        {

            DateTime date = new DateTime(DateTime.Now.Year, monthNumber, 1);

             IEnumerable<user> employees = getEmployeeByTeamID(id);

            user employee = employees.OrderBy(emp => getWorkedHoursPerMonthByEmpID(emp.id, date)).Last();

            return employee;
        }


        public ActionResult Details(long id, int Monthnumber)
        {
            IEnumerable<Ticket> ticket = ticketSerices.GetMany(t => t.employee_id == id && t.dateEnd.Value.Month == Monthnumber).ToList();
           
            ViewBag.tickets = ticket;
            var data1 = JsonConvert.SerializeObject(getDataEstimatedHours(ticket));
            var data2 = JsonConvert.SerializeObject(getDataRealizedHours(ticket));

            ViewBag.DataPoints1 = data1;
            ViewBag.DataPoints2 = data2;

            ViewBag.employee = employeeService.GetById(id);

            ViewBag.TotalEstimation = (double) ticket.Select(t => t.estimatedHours).Sum();
            ViewBag.TotalDuration = (double) ticket.Select(t => t.duration).Sum();
            ViewBag.Gab = (double) (ViewBag.TotalDuration - ViewBag.TotalEstimation);
            return View();
        }

        public List<PointModel> getDataEstimatedHours(IEnumerable<Ticket> tickets)
        {
            List<PointModel> dataPoints = new List<PointModel>();

            List<Ticket> ticketts = tickets.Where(s => s.status.Equals("Done")).ToList();
            foreach (Ticket ticket in ticketts)
            {
                double y = (double)ticket.estimatedHours;
                string label = ticket.title;
                var point = new PointModel(y, label);
                dataPoints.Add(point);
            }
            return dataPoints;
        }


        public List<PointModel> getDataRealizedHours(IEnumerable<Ticket> tickets)
        {
            List<PointModel> dataPoints = new List<PointModel>();

            List<Ticket> ticketts = tickets.Where(s => s.status.Equals("Done")).ToList();
            foreach (Ticket ticket in ticketts)
            {
                double y = (double)ticket.duration;
                string label = ticket.title;
                var point = new PointModel(y, label);
                dataPoints.Add(point);
            }
            return dataPoints;
        }

    }




   


}