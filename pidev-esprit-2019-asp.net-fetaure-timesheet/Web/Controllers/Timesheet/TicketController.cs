using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Timesheet
{
    public class TicketController : Controller
    {

        public static IEnumerable<Ticket> getListTicketsByProject(long id)
        {

            IEnumerable<Ticket> tickets;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Ticket/" + id).Result;
            
            if (response.ReasonPhrase.Equals("Found"))
            {

                tickets = response.Content.ReadAsAsync<IEnumerable<Ticket>>().Result;

            }
            else
            {
                tickets= null;
            }

            return tickets;
        }
        

        public static int countToDoTickets(IEnumerable<Ticket> tickets)
        {
            int nbr = tickets.Select(t => t.status).Where(s => s.Equals("ToDo")).Count();
            return nbr;
        }

        public static int countInProgressTickets(IEnumerable<Ticket> tickets)
        {
            int nbr = tickets.Select(t => t.status).Where(s => s.Equals("In_Progress")).Count();
            return nbr;
        }

        public static int countDoneTickets(IEnumerable<Ticket> tickets)
        {
            int nbr = tickets.Select(t => t.status).Where(s => s.Equals("Done")).Count();
            return nbr;
        }


        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
