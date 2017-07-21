using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JsonTestCaseProviders.NUnit
{
    public abstract class JsonFilesTestCaseProvider<TData> : IEnumerable
    where TData : ITestCaseData
    {
        /// <summary>
        /// Convert the information about the individual test cases into TestCaseParameters.
        /// Must be implemented by concrete class.
        /// </summary>
        /// <param name="testCaseData">Object with the concrete test data.
        /// Will need to be cast to the underlying concrete type.</param>
        /// <returns>TestCaseParameters</returns>
        public abstract object[] GetParameters(object testCaseData);

        /// <summary>
        /// Provide the path to load the tests from.
        /// Must be implemented by concrete class.
        /// </summary>
        /// <returns>The full path of the directory to load the tests from</returns>
        public abstract string GetPath();

        /// <summary>
        /// Provide an enumeration of test cases built from a set of JSON files.
        /// Implements IEnumerable.
        /// </summary>
        /// <returns>An enumeration of Parameters for a set of tests</returns>
        public IEnumerator GetEnumerator()
        {
            // Loop through folder containing all tests
            foreach (string filePath in Directory.GetFiles(GetPath(), "*.json", SearchOption.AllDirectories))
            {
                TestCaseParameters testCaseParameters = null;
                try
                {
                    TData testCaseData;
                    using (var reader = new StreamReader(new FileStream(filePath, FileMode.Open)))
                    {
                        testCaseData = JsonConvert.DeserializeObject<TData>(reader.ReadToEnd());
                    }
                    testCaseParameters = new TestCaseParameters(GetParameters(testCaseData))
                    {
                        TestName = Path.GetFileNameWithoutExtension(filePath)
                    };
                    foreach (string category in testCaseData.Categories)
                    {
                        testCaseParameters.Properties.Add("Category", category);
                    }
                    testCaseParameters.RunState = testCaseData.Ignore ? RunState.Ignored : RunState.Runnable;
                    if (testCaseData.Ignore)
                    {
                        testCaseParameters.Properties.Add("_SKIPREASON", testCaseData.IgnoreReason);
                    }
                }
                catch (Exception)
                {
                    testCaseParameters = new TestCaseParameters()
                    {
                        TestName = Path.GetFileNameWithoutExtension(filePath)
                    };
                    testCaseParameters.RunState = RunState.Ignored;
                    testCaseParameters.Properties.Add("_SKIPREASON", "Unable to load file");
                }

                yield return testCaseParameters;
            }
        }
    }
}