using JsonTestCaseProviders.NUnit.Tests.DataObjects;
using NUnit.Framework;

namespace JsonTestCaseProviders.NUnit.Tests
{
    public class InputOnlyJsonDrivenTest
    {
        /// <summary>
        /// An unrealistically simple test to demo loading an input from JSON files to drive tests
        /// </summary>
        /// <param name="input"></param>
        [TestCaseSource(typeof(InputOnlyTestProvider))]
        public void DemoInputAndResultsJsonTest(TestCaseInput input)
        {
            Assert.That(input.X, Is.EqualTo(input.Y));
        }
    }
}