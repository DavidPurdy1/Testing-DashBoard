using System;

namespace BlazorApp.Data
{
    public class TestData
    {
        public DateTime TestTime { get; set; }

        public bool TestStatus { get; set; }

        public int AssemblyId { get; set; }
        
        public int TestId { get; set; }

        public string Tester { get; set; }

        public string ImageLocation {get; set; }
    }
}