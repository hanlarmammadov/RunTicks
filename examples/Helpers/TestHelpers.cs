using System.Threading;
using System.Threading.Tasks;

namespace Examples.Helpers
{
   public static class TestHelpers
    {
        public static void SomeAction()
        {
            Thread.Sleep(10);
        }
        public static Task SomeAsyncAction()
        {
            return Task.CompletedTask;
        }
        public static void SomeInitialization()
        {

        }
        public static void SomeCleanUp()
        {

        }
    }
}
