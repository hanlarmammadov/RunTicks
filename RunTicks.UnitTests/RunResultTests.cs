using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class RunResultTests
    {
        [Test]
        [Ignore("Not completed.")]
        public void Ctor_When_Called_Sets_Provided_Properties()
        {
            // Arrange
            Int64 elapsedTicks = 23423123;
            Int64 numberOfRuns = 42;
            DateTime startDate = DateTime.Now.AddMinutes(-50);
            DateTime endDate = DateTime.Now.AddMinutes(-30);
            string measurementName = "Measurement name";
            string attemptName = "Attempt name";
            Dictionary<string, object> additionalData = new Dictionary<string, object>();

            // Act
            RunResult runResult = new RunResult(elapsedTicks, numberOfRuns, startDate, endDate, measurementName, attemptName, additionalData);

            // Assert
            Assert.AreEqual(elapsedTicks, runResult.TotalElapsedTicks);
            Assert.AreEqual((Double)(elapsedTicks / numberOfRuns), runResult.AverageTicks);
        }
    }
}
