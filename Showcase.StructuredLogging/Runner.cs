using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TehGM.Showcase.StructuredLogging
{
    class Runner : IHostedService
    {
        private readonly RunnerOptions _options;

        public Runner(IOptionsMonitor<RunnerOptions> options)
        {
            this._options = options.CurrentValue;
        }

        private async Task RunnerTask(CancellationToken cancellationToken)
        {
            // wait just to let initialization task log what it has to log
            await Task.Delay(this._options.StartDelay, cancellationToken).ConfigureAwait(false);

            // start watch to keep track of elapsed milliseconds
            Stopwatch watch = new Stopwatch();
            watch.Start();

            try
            {
                // log some initial info
                Console.WriteLine($"{watch.ElapsedMilliseconds}: Method {nameof(IHostedService.StartAsync)}");
                Console.WriteLine($"{watch.ElapsedMilliseconds}: {this.GetType().Name} starting");
                Console.WriteLine($"{watch.ElapsedMilliseconds}: Time is {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");

                // cancel tasks after a few seconds
                Console.WriteLine($"{this.GetType().Name} will cancel after {this._options.CancellationDelay}.");
                using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(this._options.CancellationDelay);

                // loop until task cancellation, logging a message every now and then
                for (; ; )
                {
                    await Task.Delay(this._options.TickDelay, cts.Token).ConfigureAwait(false);
                    Console.WriteLine($"{watch.ElapsedMilliseconds}: Tick.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception of type {ex.GetType().Name} occured when running {this.GetType().Name}: {ex}");
            }
            finally
            {
                Console.WriteLine($"{watch.ElapsedMilliseconds}: {this.GetType().Name} stopping");
            }
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            // start as a separate task to not block the initialization task
            _ = this.RunnerTask(cancellationToken);
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
