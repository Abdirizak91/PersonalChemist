using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System;

namespace AzureSignalRChat
{
    public static class Negotiate
    {
        [FunctionName("Negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous,"get",Route = "negotiate")]
            HttpRequest req,
            [SignalRConnectionInfo(HubName = "simplechat")]
            SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

      
    }
}
