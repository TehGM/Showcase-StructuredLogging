using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TehGM.Showcase.StructuredLogging
{
    class Runner : IHostedService
    {
        private readonly ILogger _log;
        private readonly RunnerOptions _options;

        public Runner(ILogger<Runner> log, IOptionsMonitor<RunnerOptions> options)
        {
            this._log = log;
            this._options = options.CurrentValue;
        }

        private async Task RunnerTask(CancellationToken cancellationToken)
        {
            // wait just to let initialization task log what it has to log
            await Task.Delay(this._options.StartDelay, cancellationToken).ConfigureAwait(false);

            // start watch to keep track of elapsed milliseconds
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // initialize log scope
            using IDisposable scope = this._log.BeginScope(new Dictionary<string, object>()
            {
                { "Method", nameof(IHostedService.StartAsync) },
                { "Service", this.GetType().Name },
                { "Date", DateTime.Now.ToLongDateString() },
                { "Time", DateTime.Now.ToLongTimeString() },
                { "Delay", this._options.CancellationDelay.TotalSeconds }
            });

            try
            {
                // log some initial info
                this._log.LogTrace("{Timer}: Method {Method}", watch.ElapsedMilliseconds);
                this._log.LogInformation("{Timer}: {Service} starting", watch.ElapsedMilliseconds);
                this._log.LogDebug("{Timer}: Time is {Date} {Time}", watch.ElapsedMilliseconds);

                // cancel tasks after a few seconds
                this._log.LogWarning("{Service} will cancel after {Delay} seconds.");
                using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(this._options.CancellationDelay);

                // loop until task cancellation, logging a message every now and then
                for (; ; )
                {
                    await Task.Delay(this._options.TickDelay, cts.Token).ConfigureAwait(false);
                    this._log.LogDebug("{Timer}: Tick.", watch.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
                when (ex.LogAsError(this._log, "Exception of type {Type} occured when running {Service}", ex.GetType().Name, this.GetType().Name)) { }
            finally
            {
                this._log.LogInformation("{Timer}: {Service} stopping", watch.ElapsedMilliseconds, this.GetType().Name);
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
