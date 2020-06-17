using System.Diagnostics;
using System.Threading.Tasks;

namespace RunTicks
{
    public delegate Task AsyncActionWithStopwatch(Stopwatch stopwatch);
}
