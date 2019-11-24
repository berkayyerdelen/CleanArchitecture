using System;
using Hangfire;

namespace Core.Comman.HangFire
{
    public class FireAndForgetJob
    {
        public FireAndForgetJob()
        {
            //Fire and forget
            //var jobId = BackgroundJob.Enqueue(() => ProcessFireAndForgetJob());
        }

        public void ProcessFireAndForgetJob()
        {
            Console.WriteLine("I am a Fire and Forget Job !!");
        }
    }
}