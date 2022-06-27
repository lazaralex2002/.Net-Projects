using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/ 

        public ActionResult Index()
        {
            using (var context = new TaskManagementEntities1())
            {
                var query = context.Tasks
                       .Where(s => s.TaskID == 1)
                       .FirstOrDefault<Task>();
                if (query != null)
                    ViewBag.Name = query.Name;
                else ViewBag.Name = "null";
            }
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        public string Welcome(string name, int id = 1)
        {
            
            return HttpUtility.HtmlEncode("Hello " + name + ", id is: " + ViewBag.Name);
        }
    }
}