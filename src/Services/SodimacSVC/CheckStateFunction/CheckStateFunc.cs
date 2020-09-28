using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CheckStateFunction
{
    public static class CheckStateFunc
    {
        // REF: https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp

        [FunctionName("CheckStateFunc")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
