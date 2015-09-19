using PainFree.Helpers.TestData;
using PainFree.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PainFree.Controllers
{
    public class HomeController : Controller
    {
        public static readonly DateTime dataBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
        public static readonly DateTime dataEndTime = DateTime.Now.Date.Add(new TimeSpan(13, 30, 0));

        public ActionResult Index()
        {
            var testDataItems = TestData.getData(dataBeginTime, dataEndTime);
            var failedTestDataItems = testDataItems.Where(data => data.NumberOfFailures > 0).ToList();

            return View(failedTestDataItems);
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