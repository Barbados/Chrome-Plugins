using System.Threading.Tasks;
using capturerservice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using capturerservice.Services;
using Microsoft.Extensions.Options;
using capturerservice.Model;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace WordsFunction
{
    public class WordsFunction
    {
        private static IWordsService _wordsService;

        public WordsFunction(IOptions<SettingsConfig> options, IWordsService service)
        {
            _wordsService = service;
        }

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "capturer")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("UpdateAllClients")]
        public static async Task RefreshWords(
            [ServiceBusTrigger("words", Connection = "SbConnectionString")] WordElement queueItem,
            [SignalR(HubName = "capturer")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (queueItem == null)
            {
                log.LogInformation("Please pass a payload to broadcast in the request body.");
                return;
            }

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessageReceived",
                    Arguments = new[] { queueItem }
                });
            log.LogInformation("Message: " + queueItem.Word);
            log.LogInformation("SignalR messages: " + signalRMessages.ToString());
        }

        [FunctionName("GetWords")]
        public async Task<IActionResult> GetWords(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "words")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetWords triggered....");

            return (ActionResult)new OkObjectResult(_wordsService.Get());
        }

        [FunctionName("CreateWord")]
        [return: ServiceBus("words", Connection = "SbConnectionString")]
        public WordElement CreateWord(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "words/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogInformation("CreateWord triggered....");

            return _wordsService.Create(name);
        }
    }
}
