using ExternalServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Models;
using System.Dynamic;
using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace RulesEnginePoC;

public static class Program
{
    static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddWorkflowServices(builder.Configuration);

        using IHost host = builder.Build();

        var basicInfo = "{\"name\": \"hello\",\"email\": \"abcy@xyz.com\",\"creditHistory\": \"good\",\"country\": \"india\",\"loyaltyFactor\": 2,\"totalPurchasesToDate\": 10000}";
        var orderInfo = "{\"totalOrders\": 5,\"recurringItems\": 2}";
        var telemetryInfo = "{\"noOfVisitsPerMonth\": 10,\"percentageOfBuyingToVisit\": 15}";

        var converter = new ExpandoObjectConverter();

        dynamic input1 = JsonConvert.DeserializeObject<ExpandoObject>(basicInfo, converter);
        dynamic input2 = JsonConvert.DeserializeObject<ExpandoObject>(orderInfo, converter);
        dynamic input3 = JsonConvert.DeserializeObject<ExpandoObject>(telemetryInfo, converter);

        var inputs = new dynamic[]
            {
                    input1,
                    input2,
                    input3
            };

        var workflowLoaderService = host.Services.CreateScope().ServiceProvider.GetRequiredService<IWorkflowLoader>();
        var bre = new RulesEngine.RulesEngine(workflowLoaderService.GetWorkflows().ToArray(), null);

        string discountOffered = "No discount offered.";

        List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync("Discount", inputs).Result;

        resultList.OnSuccess((eventName) =>
        {
            discountOffered = $"Discount offered is {eventName} % over MRP.";
        });

        resultList.OnFail(() =>
        {
            discountOffered = "The user is not eligible for any discount.";
        });

        Console.WriteLine(discountOffered);
    }

}
