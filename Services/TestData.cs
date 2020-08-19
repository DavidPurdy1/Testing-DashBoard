using System;
///<summary> Holds data about the test case or test run </summary> 
namespace BlazorApp.Services
{
    public class TestData
    {
        public int TestCaseId { get; set; }
        public int TestRunId { get; set; }
        public int TestsFailed { get; set; }
        public int TestsPassed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string TestName { get; set; }
        public int TestStatus { get; set; }
    }
}