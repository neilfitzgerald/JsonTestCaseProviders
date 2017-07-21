using System.Collections.Generic;

namespace JsonTestCaseProviders.NUnit
{
    /// <summary>
    /// An interface that represents the common data elements needed to Provide tests.
    /// </summary>
    public interface ITestCaseData
    {
        bool Ignore { get; set; }
        string IgnoreReason { get; set; }
        List<string> Categories { get; set; }
    }
}