using System.IO;
using JsonTestCaseProviders.NUnit.Tests.DataObjects;
using NUnit.Framework;
using Newtonsoft.Json;

namespace JsonTestCaseProviders.NUnit.Tests
{
    public class InputAndResultsJsonDrivenTest
    {
        [TestCaseSource(typeof(InputAndResultsTestProvider))]
        public void DemoInputAndResultsJsonTest(TestCaseInput input, TestCaseResult expectedResult)
        {
            // Get actual result
            // In a more realistic test, this might be something like some calls to web services
            TestCaseResult result = new TestCaseResult
            {
                CalculatedValue = input.X + input.Y
            };

            // Saved result file can be dropped in to replace the expected file.
            SaveResult(input, result, TestContext.CurrentContext.Test.Name);

            // Do actual comparison
            Assert.That(result.CalculatedValue, Is.EqualTo(expectedResult.CalculatedValue));
        }

        private void SaveResult(TestCaseInput input, TestCaseResult result, string name)
        {
            var actualData = new InputAndResultsTestCaseData()
            {
                Categories = TestContext.CurrentContext.Test.Properties["Category"].ToList<string>(),
                TestInput = input,
                // Ignore can never be true to reach here
                ExpectedResult = result
            };
            var path = $"{TestContext.CurrentContext.TestDirectory}{Path.DirectorySeparatorChar.ToString()}ActualResults{Path.DirectorySeparatorChar.ToString()}InputAndResults{Path.DirectorySeparatorChar.ToString()}";
            var fileName = $"{path}{Path.DirectorySeparatorChar.ToString()}{name}.json";
            Directory.CreateDirectory(path);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(actualData, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }
    }
}