using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MvcApplication;

namespace MvcApplication.Controllers
{
    public class TasksController : Controller
    {
        private TaskManagementEntities taskManagementEntities = new TaskManagementEntities();
        // GET: Tasks
        /*
        public ActionResult Index()
        {
            if (HttpContext.Session["Project"] == null)
            {
                return Redirect("/Projects/ChooseProject");
            }
            else
            {
                ViewBag.TaskCost = db.GetTaskCost().ToList();
                var proj = int.Parse(HttpContext.Session["Project"].ToString());
                var tasks = db.Tasks.Include(t => t.Project).Where(t => t.ProjectId == proj);
                return View(tasks.ToList());
            }
        }
        */

        public ActionResult Index(string project)
        {
            ViewBag.TaskCost = taskManagementEntities.GetTaskCost().ToList();
            ViewBag.Projects = taskManagementEntities.Projects.ToList();
            if (HttpContext.Session["Project"] != null)
            {
                var proj = int.Parse(HttpContext.Session["Project"].ToString());
                Redirect("/Tasks/?project=" + proj.ToString());
            }
            project = Request.QueryString["project"];
            if (project == null || project == "")
            {
                var tasks = taskManagementEntities.Tasks.Include(t => t.Project);
                return View(tasks.ToList());
            }
            else
            {
                ViewBag.TaskCost = taskManagementEntities.GetTaskCost().ToList();
                var proj = int.Parse(project);
                var tasks = taskManagementEntities.Tasks.Include(t => t.Project).Where(t => t.ProjectId == proj);
                return View(tasks.ToList());
            }
        }

        // POST: Tasks/AssignResource/5
        [HttpPost]
        public ActionResult AssignResource(int resource, int taskId)
        {
            var entity = new ResourceTask();
            entity.ResourceId = resource;
            entity.TaskId = taskId;
            taskManagementEntities.ResourceTasks.Add(entity);
            taskManagementEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tasks/AssignResource/5
        public ActionResult AssignResource(int? id)
        {
     
            var TaskResources = taskManagementEntities.ResourceTasks.Where(rt => rt.TaskId == id).ToList();
            var ResourceList = taskManagementEntities.Resources.ToList();
            var ResourcesThatCanBeAssigned = new List<Resource>();
            var AssignedResources = new List<Resource>();
            foreach (var resource in ResourceList)
            {
                bool ok = true;
                foreach(var taskResource in TaskResources)
                {
                    if (resource.ResourceId == taskResource.ResourceId) ok = false;
                }
                if(ok == true)
                {
                    ResourcesThatCanBeAssigned.Add(resource);
                }
                else
                {
                    AssignedResources.Add(resource);
                }
            }
            ViewBag.AssignedResources = AssignedResources;
            ViewBag.ResourcesThatCanBeAssigned = ResourcesThatCanBeAssigned;
            ViewBag.TaskResources = TaskResources;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManagementEntities.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/ViewResources/5
        public ActionResult ViewResources(int? id)
        {
            var TaskResources = taskManagementEntities.ResourceTasks.Where(rt => rt.TaskId == id).ToList();
            var ResourceList = new List<Resource>();
            foreach (var taskResource in TaskResources)
            {
                var result = taskManagementEntities.Resources.Where(res => res.ResourceId == taskResource.ResourceId);
                if (result != null)
                {
                    ResourceList.Add(result.First());
                }
            }
            ViewBag.Resources = ResourceList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManagementEntities.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManagementEntities.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(taskManagementEntities.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,Name,Duration,Start,Finish,TaskMode,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskManagementEntities.Tasks.Add(task);
                taskManagementEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(taskManagementEntities.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManagementEntities.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(taskManagementEntities.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,Name,Duration,Start,Finish,TaskMode,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskManagementEntities.Entry(task).State = EntityState.Modified;
                taskManagementEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(taskManagementEntities.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManagementEntities.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = taskManagementEntities.Tasks.Find(id);
            taskManagementEntities.Tasks.Remove(task);
            taskManagementEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Tasks/DeassignResource/5
        [HttpPost, ActionName("DeassignResource")]
        public ActionResult DeassignResource(int id)
        {
            ResourceTask resourceTask = taskManagementEntities.ResourceTasks.Find(id);
            taskManagementEntities.ResourceTasks.Remove(resourceTask);
            taskManagementEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tasks/DeassignResource/5
        public ActionResult DeassignResource(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceTask resourceTask = taskManagementEntities.ResourceTasks.Find(id);
            if (resourceTask == null)
            {
                return HttpNotFound();
            }
            return View(resourceTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                taskManagementEntities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
