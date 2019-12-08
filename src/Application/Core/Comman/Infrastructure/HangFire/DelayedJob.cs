using System;

namespace Core.Comman.Infrastructure.HangFire
{
    public class DelayedJob
    {
        public DelayedJob()
        {
            //Delayed job
            //var jobId = BackgroundJob.Schedule(() => ProcessDelayedJob(), TimeSpan.FromMinutes(4));
        }

        public void ProcessDelayedJob()
        {
            Console.WriteLine("I am a Delayed Job !!");
        }
    }
}
