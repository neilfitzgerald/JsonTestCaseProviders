using System.IO;
using JsonTestCaseProviders.NUnit.Tests.DataObjects;
using NUnit.Framework;
using System.Reflection;

namespace JsonTestCaseProviders.NUnit.Tests
{
    public class InputAndResultsTestProvider : JsonFilesTestCaseProvider<InputAndResultsTestCaseData>
    {
        /// <summary>
        /// Convert the specific parameters into the format expected by NUnit
        /// </summary>
        /// <param name="testCaseData">Object with the test case data for a specific case</param>
        /// <returns>A single entry array with the parameters for a specific test case</returns>
        public override object[] GetParameters(object testCaseData)
        {
            return new object[]
            {
                ((InputAndResultsTestCaseData)testCaseData).TestInput,
                ((InputAndResultsTestCaseData)testCaseData).ExpectedResult
            };
        }

        /// <summary>
        /// Provide the path to where the JSON files can be found for this provider.
        /// </summary>
        /// <returns>Path to where the JSON files for the test cases can be found</returns>
        public override string GetPath() => $"{TestContext.CurrentContext.TestDirectory}{Path.DirectorySeparatorChar.ToString()}TestCases{Path.DirectorySeparatorChar.ToString()}InputAndResults{Path.DirectorySeparatorChar.ToString()}";
    }
}