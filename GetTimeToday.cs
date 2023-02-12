using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Inventofree.UtilityApp
{
    public static class GetTimeToday
    {
        [FunctionName("GetTimeToday")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string timezone = req.Query["timezone"];

            string windowsId;
            var isTimezoneIdValid = TimeZoneInfo.TryConvertIanaIdToWindowsId(timezone, out windowsId);

            if (!isTimezoneIdValid)
                return new BadRequestObjectResult("The timezone that you've entered is not valid");
            
            var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(timezone));

            return new OkObjectResult(date.ToString());
        }
    }
}