using System;
using System.Collections.Generic;
using System.Linq;
using TelluriumCore.Reporting.TestRun;

namespace AutomationManagementTool.Models
{
    public class TestReports
    {
        private static readonly string TestCaseReportsPath = @"S:\IS\QA\Automation\Tellurium Results\";
        private IEnumerable<ITestReport> allTestReports;

        /*Add code when directory does not exist*/
        private TestReports(DateTime dateTime)
        {
            if (allTestReports == null)
                allTestReports = TestReport.GetAllInstances(null, TestCaseReportsPath, dateTime);
        }

        public static TestReports getInstance()
        {
            return getInstance(DateTime.Today);
        }

        public static TestReports getInstance(DateTime dateTime)
        {
            return new TestReports(dateTime);
        }

        public IEnumerable<ITestReport> getAllTestReports(DateTime dateTime)
        {
            if (allTestReports == null)
                allTestReports = TestReport.GetAllInstances(null, TestCaseReportsPath, dateTime);
            return allTestReports;
        }

        public IEnumerable<ITestReport> getAllTestReports()
        {
            return getAllTestReports(DateTime.Today);
        }

        public IEnumerable<ITestReport> getFailedTestReports()
        {
            var failedTestReports = allTestReports.Where(report => report.NumberOfFailures > 0);
            return failedTestReports;
        }

        public IEnumerable<ITestReport> getPassedTestReports()
        {
            var passedTestReports = allTestReports.Where(report => report.NumberOfFailures <= 0);
            return passedTestReports;
        }

        public IEnumerable<ITestReport> getNoonRunTestReports()
        {
            return getNoonRunTestReports(this.allTestReports);
        }

        public static IEnumerable<ITestReport> getNoonRunTestReports(IEnumerable<ITestReport> reports)
        {
            var noonRunBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
            var noonRunEndTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(19, 00, 0));
            var newList = new List<ITestReport>();

            foreach (var report in reports)
                newList.Add(report);

            return newList;
        }

        public IEnumerable<ITestReport> getNightTestReports()
        {
            return getNightTestReports(this.allTestReports);
        }

        public IEnumerable<ITestReport> getNightTestReports(IEnumerable<ITestReport> reports)
        {
            var nightRunBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(19, 30, 0));
            var nightRunEndTime = DateTime.Now.Date.Add(new TimeSpan(10, 00, 0));

            var nightTestReports = from report in reports
                                   where DateTime.Equals(report.RunBeginTime, nightRunBeginTime)
                                   where DateTime.Equals(report.RunEndTime, nightRunEndTime)
                                   select report;

            return nightTestReports;
        }
    }
}