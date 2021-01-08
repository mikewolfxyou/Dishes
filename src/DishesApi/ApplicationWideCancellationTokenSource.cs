using System.Threading;

namespace DishesApi
{
    public class ApplicationWideCancellationTokenSource
    {
        public CancellationTokenSource TokenSource { get; }

        public ApplicationWideCancellationTokenSource(CancellationTokenSource tokenSource)
        {
            TokenSource = tokenSource;
        }
    }
}