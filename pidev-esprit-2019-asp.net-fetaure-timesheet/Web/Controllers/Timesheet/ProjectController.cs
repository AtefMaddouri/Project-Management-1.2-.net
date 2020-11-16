using Data;
using Newtonsoft.Json;
using Service.Employee;
using Service.Timesheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Web.Models.TimesheetModels;

namespace Web.Controllers.Timesheet
{
    public class ProjectController : Controller
    {
        static ProjectServices projectServices = new ProjectServices();
        TeamServices teamServices = new TeamServices();

        public List<PointModel> getDataEstimatedHours()
        {
            List<PointModel> dataPoints = new List<PointModel>();
            
            List<Project> projects =projectServices.GetMany(pr => pr.status.Equals("In_Progress")).ToList();
            foreach(Project p in projects)
            {
                double y  = (double) p.tickets.Select(t => t.estimatedHours).Sum();
                string label = p.title;
                var point = new PointModel(y, label);
                dataPoints.Add(point);
            }       
            return dataPoints;
        }

        public List<PointModel> getDataEstimatedHoursDone()
        {
            List<PointModel> dataPoints = new List<PointModel>();

            List<Project> projects = projectServices.GetMany(pr => pr.status.Equals("In_Progress")).ToList();
            foreach (Project p in projects)
            {
                double y = (double) p.tickets.Where(t=> t.status.Equals("Done"))
                    .Select(t => t.estimatedHours).Sum();
                string label = p.title;
                var point = new PointModel(y, label);
                dataPoints.Add(point);
            }
            return dataPoints;
        }

        public List<PointModel> getDataReelHoursDone()
        {
            List<PointModel> dataPoints = new List<PointModel>();

            List<Project> projects = projectServices.GetMany(pr => pr.status.Equals("In_Progress")).ToList();
            foreach (Project p in projects)
            {
                double y = (double) p.tickets.Where(t => t.status.Equals("Done"))
                    .Select(t => t.duration).Sum();
                string label = p.title;
                var point = new PointModel(y, label);
                dataPoints.Add(point);
            }
            return dataPoints;
        }



        // GET: Project
        public ActionResult Index()
        {

            var data1 = JsonConvert.SerializeObject(getDataEstimatedHours());
            var data2 = JsonConvert.SerializeObject(getDataEstimatedHoursDone());
            var data3 = JsonConvert.SerializeObject(getDataReelHoursDone());
            ViewBag.DataPoints1 = data1;
            ViewBag.DataPoints2 = data2;
            ViewBag.DataPoints3 = data3;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Project").Result;
           
             if (response.ReasonPhrase.Equals("Found"))
            {
                
                ViewBag.Result = response.Content.ReadAsAsync<IEnumerable<Project>>().Result;
               
            }
            else
            {
                ViewBag.Result = "erreur";
            }

            
            return View();

        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            
            IEnumerable<SelectListItem> teams = teamServices.GetMany().Select(t => new SelectListItem { Text = t.teamName, Value = t.id.ToString() });
            ViewBag.SelectListItem = teams;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Project/" + id).Result;
            Project project = new Project();
            if (response.ReasonPhrase.Equals("Found"))
            {
                project = response.Content.ReadAsAsync<Project>().Result;
            }
            else
            {
                ViewBag.project = "erreur";
            }

            return View(project);
        }

        // GET: Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectModel project)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");


            // TODO: Add insert logic here
            client.PostAsJsonAsync<ProjectModel>("rest/Project", project)
                    .ContinueWith((postTask) => postTask.Result.ReasonPhrase.Equals("Created"));
                return RedirectToAction("Index");
        }

        // GET: Project/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Project/" + id).Result;
            Project project = new Project();
            if (response.ReasonPhrase.Equals("Found"))
            {

                project = response.Content.ReadAsAsync<Project>().Result;

            }
            else
            {
                ViewBag.project = "erreur";
            }

            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            ProjectModel project  = new ProjectModel();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Project/" + id).Result;

                if (response.ReasonPhrase.Equals("Found"))
                {
                    project = response.Content.ReadAsAsync<ProjectModel>().Result;
                    UpdateModel(project, collection);

                // TODO: Add insert logic here

                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
                client2.PutAsJsonAsync<ProjectModel>("rest/Project", project).ContinueWith((postTask)  => postTask.Result.ReasonPhrase.Equals("Accepted"));
                return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }   
            
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Project/"+id).Result;
            Project project = new Project();
            if (response.ReasonPhrase.Equals("Found"))
            {

                 project = response.Content.ReadAsAsync<Project>().Result;

            }
            else
            {
                ViewBag.project = "erreur";
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:9080/pidev-web/");

                // TODO: Add insert logic here
                client.DeleteAsync("rest/Project/" + id)
                        .ContinueWith((postTask) => postTask.Result.ReasonPhrase.Equals("Gone"));
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public static double getProgress(IEnumerable<Ticket> tickets)
        {
            double nbr2 = tickets.Count();
            double nbr = tickets.Select(t => t.status).Where(s => s.Equals("Done")).Count();

            double progress = (nbr/nbr2)*100;
            
            return progress;
        }

        public static int countToDoProjects(IEnumerable<Project> projects)
        {
            int nbr = projects.Select(t => t.status).Where(s => s.Equals("ToDo")).Count();
            return nbr;
        }

        public static int countInProgressProjects(IEnumerable<Project> projects)
        {
            int nbr = projects.Select(t => t.status).Where(s => s.Equals("In_Progress")).Count();
            return nbr;
        }

        public static int countDoneProjects(IEnumerable<Project> projects)
        {
            int nbr = projects.Select(t => t.status).Where(s => s.Equals("Done")).Count();
            return nbr;
        }

        public static double WorkedHoursPerEmployeeID(long idProject, long idEmployee)
        {
            double nbr =(double) projectServices.GetById(idProject)
                .tickets.Where(t => (t.status.Equals("Done") && t.employee_id == idEmployee))
                .Select(t => t.estimatedHours)
                .Sum();
            return nbr;
        }

        public ActionResult startProject(long id)
        {
            Project project = new Project();
            project = projectServices.GetById(id);
            project.status = "In_Progress";
            projectServices.Update(project);
            projectServices.Commit();


            //String mounth = System.Web.HttpContext.Current.Request["mounth"];
            return RedirectToAction("Details/"+id);
        }


        public ActionResult setTeam(long idP)
        {

            long id = long.Parse( Request.Form["TeamSelected"]);
            Team team =  teamServices.GetById(id);
            Project project = new Project();
            project = projectServices.GetById(id);
            project.team = team;
            project.team_id = team.id;
            projectServices.Update(project);
            projectServices.Commit();

            return RedirectToAction("Details/" + idP);
        }

    }


}
