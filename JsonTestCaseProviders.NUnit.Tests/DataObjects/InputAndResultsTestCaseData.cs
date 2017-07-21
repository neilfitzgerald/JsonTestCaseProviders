using System.Collections.Generic;

namespace JsonTestCaseProviders.NUnit.Tests.DataObjects
{
    public class InputAndResultsTestCaseData : ITestCaseData
    {
        public List<string> Categories { get; set; }
        public bool Ignore { get; set; }
        public string IgnoreReason { get; set; }
        public TestCaseInput TestInput { get; set; }
        public TestCaseResult ExpectedResult { get; set; }
    }
}