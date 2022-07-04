using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcApplication;

namespace MvcApplication.Controllers
{
    public class ProjectsController : Controller
    {
        private TaskManagementEntities taskManagementEntities = new TaskManagementEntities();

        // GET: Projects
        public ActionResult Index()
        {
            ViewBag.ChooseProject = 0;
            ViewBag.ProjectCost = taskManagementEntities.GetProjectCost().ToList();
            return View(taskManagementEntities.Projects.ToList());
        }


        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = taskManagementEntities.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                taskManagementEntities.Projects.Add(project);
                taskManagementEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = taskManagementEntities.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                taskManagementEntities.Entry(project).State = EntityState.Modified;
                taskManagementEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = taskManagementEntities.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = taskManagementEntities.Projects.Find(id);
            taskManagementEntities.Projects.Remove(project);
            taskManagementEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        // GET: Projects/Choose/5
        public ActionResult Choose(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Choose/5
        [HttpPost, ActionName("Choose")]
        [ValidateAntiForgeryToken]
        public ActionResult Choose(int id)
        {
            HttpContext.Session["Project"] = id;
            return RedirectToAction("Index");
        }

        // GET: Projects/Unchoose/5
        public ActionResult Unchoose(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Unchoose/5
        [HttpPost, ActionName("Unchoose")]
        [ValidateAntiForgeryToken]
        public ActionResult Unchoose(int id)
        {
            HttpContext.Session["Project"] = null;
            return RedirectToAction("Index");
        }
        */
        /*
        // GET: Projects/ChooseProject
        public ActionResult ChooseProject()
        {
            ViewBag.ChooseProject = 1;
            ViewBag.ProjectCost = db.GetProjectCost().ToList();
            return View("~/Views/Projects/Index.cshtml", db.Projects.ToList());
        }*/
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
