using System;
using Hangfire;

namespace Core.Comman.HangFire
{
    public class ContinuationsJob
    {
        public ContinuationsJob()
        {
            //var parentJobId = "1234";
            //BackgroundJob.ContinueJobWith(parentJobId, () => ProcessContinuationsJob());
        }
        public void ProcessContinuationsJob()
        {
            Console.WriteLine("I am a Recurring Job !!");
        }
    }
}