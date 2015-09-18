using AutomationManagementTool.Models;
using System.Web.Mvc;

namespace AutomationManagementTool.Controllers
{
    public class HomeController : Controller
    {
        private TestReports testReports = TestReports.getInstance();

        public ActionResult Index()
        {
            var failedTestReports = testReports.getFailedTestReports();
            var test = TestReports.getNoonRunTestReports(failedTestReports);

            return View(failedTestReports);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}