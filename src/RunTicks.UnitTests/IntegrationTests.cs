using NUnit.Framework;
using System;
using System.Threading;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Measurement_With_Sync_Action_Assert_Average_Ticks()
        {
            var measurement = Measurement.Create().OfAction(() =>
            {
                Thread.Sleep(1);
            });
            var result = measurement.Run(10);


            Assert.Catch<AssertionException>(() =>
            {
                result.Should.NotExceedAverageTicks(9_000);
            });

            result.Should.NotExceedAverageTicks(25_000);
        }

        [Test]
        public void Measurement_With_Sync_Action_Assert_Milliseconds()
        {
            var result = Measurement.Create().OfAction(() =>
            {
                Thread.Sleep(10);
            }).Run(10);

            Assert.Catch<AssertionException>(() =>
            {
                result.Should.NotExceedAverageMilliseconds(9);
            });

            result.Should.NotExceedAverageMilliseconds(50);
        }

        [Test]
        public void Measurement_With_Sync_Action_That_Throws_An_Exception()
        {
            var measurement = Measurement.Create().OfAction(() =>
            {
                throw new InvalidOperationException();
            });

            Assert.Catch<InvalidOperationException>(() =>
            {
                measurement.Run(10);
            });
        }

        [Test]
        public void Measurement_Run_That_Takes_Canceled_Token()
        {
            var measurement = Measurement.Create().OfAction(() =>
            {

            });

            var cancellationToken = new CancellationToken(canceled: true);

            Assert.Catch<OperationCanceledException>(() =>
            {
                measurement.Run(10, "", cancellationToken);
            });
        }
    }
}