using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TehGM.Showcase.StructuredLogging
{
    class Runner : IHostedService
    {
        private readonly ILogger _log;

        public Runner(ILogger<Runner> log)
        {
            this._log = log;
        }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            try
            {
                // log messages here
            }
            catch (Exception ex)
            {
                // log exception here
            }
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
