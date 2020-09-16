using Quartz;
using Quartz.Impl;
using SchedulerService.Jobs;
using System;
using System.Security.Policy;
using System.Threading;

namespace SchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            //// construct a scheduler factory
            //var scheduler = new StdSchedulerFactory().GetScheduler();

            ////// create job
            ////var job = JobBuilder.Create<SimpleJob>().WithIdentity("MyJob", "MyGroup").Build();

            ////// create trigger
            ////var trigger = TriggerBuilder.Create().WithIdentity("MyTrigger", "MyGroup")
            ////    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();

            //// Schedule the job using the job and trigger 
            //scheduler.ScheduleJob(
            //    JobBuilder.Create<SimpleJob>().WithIdentity("MyJob", "MyGroup").Build(),
            //    TriggerBuilder.Create().WithIdentity("MyTrigger", "MyGroup")
            //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build());

            //scheduler.Start();
            //Thread.Sleep(10000);
            //scheduler.Shutdown(true);

            //Console.ReadLine();

            var scheduler = new StdSchedulerFactory().GetScheduler();

            //// add scheduler listener
            //scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());

            //// add global job listener
            //scheduler.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());

            //// add global trigger listener
            //scheduler.ListenerManager.AddTriggerListener(new TriggerListener(), GroupMatcher<TriggerKey>.AnyGroup());

            // add jobs & triggers
            scheduler.ScheduleJob(
                JobBuilder.Create<SimpleJob>().WithIdentity("MyJob", "MyJobGroup").Build(),
                TriggerBuilder.Create().WithIdentity("MyTrigger", "MyTriggerGroup")
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(2).RepeatForever()).Build());


            scheduler.Start(); // start scheduler
            Thread.Sleep(5000); // sleep the main thread (Quartz will fire the job 3x on other threads)
            scheduler.Shutdown(true); // stop the scheduler

            Console.ReadLine();
        }
    }
}
