using System;

namespace TehGM.Showcase.StructuredLogging
{
    public class RunnerOptions
    {
        /// <summary>How long runner will wait before starting.</summary>
        public TimeSpan StartDelay { get; set; } = TimeSpan.FromMilliseconds(50);
        /// <summary>Delay after which the task will be cancelled.</summary>
        public TimeSpan CancellationDelay { get; set; } = TimeSpan.FromSeconds(5);
        /// <summary>How often the runner should log a tick.</summary>
        public TimeSpan TickDelay { get; set; } = TimeSpan.FromMilliseconds(350);
    }
}
