using Quartz;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Jobs
{
    public class SimpleJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, JOb executed");
        }
    }
}
