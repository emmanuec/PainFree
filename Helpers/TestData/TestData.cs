using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelluriumCore.Reporting.TestRun;
using TelluriumCore.Reporting.TestCase;

namespace PainFree.Helpers.TestData
{
    public static class TestData
    {
        /** Note that ITestReport End Time is invalid **/
        //C:\Users\ecastill\Desktop\test\
        private static readonly string testDataPath = @"S:\IS\QA\Automation\Tellurium Results";

        /*TODO:Add validation that date range is valid for a single day model */
        public static List<ITestReport> getData(DateTime beginTime, DateTime endTime)
        {
            var testReports = TestReport.GetAllInstances(null, testDataPath, beginTime);
            var filteredTestReports = from report in testReports
                                      where report.RunEndTime <= endTime
                                      select report;

            return filteredTestReports.ToList();
        }

        /*TODO:Add Code to update data*/
        public static IList<ITestReport> getNoonData(DateTime beginTime, List<ITestReport> testData)
        {
            var noonStartTime = beginTime;
            var noonEndTime = beginTime.Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm

            var noonTestData = from report in testData
                               where report.RunBeginTime >= noonStartTime
                               where report.RunBeginTime <= noonEndTime
                               where report.MachineName.Contains("VMTEST") || 
                                     report.MachineName.Contains("VMAUTOMATION")
                               select report;

            return noonTestData.ToList();
        }

        public static IList<ITestReport> getNightData(DateTime endTime, List<ITestReport> testData)
        {
            var nightStartTime = endTime.AddYears(-1).Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm
            var nightEndTime = endTime;

            var nightTestReports = from report in testData
                                   where report.RunBeginTime >= nightStartTime
                                   where report.RunBeginTime <= endTime
                                   where report.MachineName.Contains("VMTEST") ||
                                         report.MachineName.Contains("VMAUTOMATION")
                                   select report;

            return nightTestReports.ToList();
        }
    }
}
