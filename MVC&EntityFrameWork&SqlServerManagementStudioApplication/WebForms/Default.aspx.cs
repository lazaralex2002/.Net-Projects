using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LazarAlexandruConstantin;
using Newtonsoft.Json;

namespace WebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["project"] != null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallProjectInitialized", "ProjectInitialized()", true);
                Debug.WriteLine("Page_load");
            }
        }

        [WebMethod(EnableSession = true)]
        public static bool Quit()
        {
            HttpContext.Current.Session["project"] = null;
            return true;
        }

        private static Project GetProjectFromSession()
        {
            return (Project)HttpContext.Current.Session["project"];
        }

        [WebMethod]
        public static string Initialize()
        {
            InitializeProject();
            return GetProjectFromSession().Tasks.Count().ToString();
        }

        [WebMethod]
        public static string GetTasks()
        {
            List<TaskString> tasks = new List<TaskString>();
            var project = GetProjectFromSession();
            if (project != null)
            {
                foreach (var task in project.Tasks)
                {
                    TaskString taskString = new TaskString();
                    SetProperties(taskString, task);
                    tasks.Add(taskString);
                }
            }
            var json = JsonConvert.SerializeObject(tasks);
            return json;
        }

        [WebMethod]
        public static bool Deserialize(string filePath)
        {
            var project = GetProjectFromSession();
            try
            {
                project = project.DeserializeFromString(filePath);
                HttpContext.Current.Session["project"] = project;
            }
            catch(FileNotFoundException)
            {
                return false;
            }
            catch(Exception e )
            {
                throw e;
            }
            return true;
        }

        [WebMethod]
        public static string Serialize()
        {
            return GetProjectFromSession().SerializeToString();
        }

        [WebMethod]
        public static bool UpdateField(string rowindex, int colindex, string value)
        {
            var project = GetProjectFromSession();
            bool result = Extensions.Validate(colindex, value);
            if (result == true)
            {
                DateTime dateTime;
                var task = project.GetTask(int.Parse(rowindex));
                switch (colindex)
                {
                    case Columns.TaskName:
                        task.Name = value;
                        Debug.WriteLine(project.GetTask(int.Parse(rowindex)).Name);
                        break;
                    case Columns.Duration:
                        task.Duration = int.Parse(value);
                        break;
                    case Columns.Start:
                        dateTime = DateTime.ParseExact(value.Trim(), "M/dd/yyyy hh:mm:ss tt", null);
                        task.Start = dateTime;
                        break;
                    case Columns.Finish:
                        dateTime = DateTime.ParseExact(value.Trim(), "M/dd/yyyy hh:mm:ss tt", null);
                        task.Finish = dateTime;
                        break;
                    default:
                        break;
                }
                HttpContext.Current.Session["project"] = project ;
            }
            return result;
        }

        private static void SetProperties(TaskString taskString, Task task)
        {
            taskString.TaskID = task.TaskID.ToString();
            taskString.TaskName = task.Name;
            taskString.Duration = task.Duration.ToString();
            taskString.Start = task.Start.ToString();
            taskString.Finish = task.Finish.ToString();
            taskString.ResourceNames = task.ResourceNames.ContentToString();
            taskString.Predecessors = task.Predecessors.ContentToString();
            taskString.TaskMode = task.TaskMode.ToString();
        }

        private static void InitializeProject()
        {
            string fileName=  "input.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Debug.WriteLine(filePath);
            var project = new Project();
            project.Initialize(filePath);
            HttpContext.Current.Session["project"] = project;
        }
    }
}