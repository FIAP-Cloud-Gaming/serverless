using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions;

public class MetricsAggregatorFunction(IHttpClientFactory httpClientFactory)
{
    [Function("AggregateGameMetrics")]
    public async Task Run(
        [TimerTrigger("0 0 * * * *")] TimerInfo timer,
        FunctionContext context)
    {
        var logger = context.GetLogger<MetricsAggregatorFunction>();
        logger.LogInformation("Aggregating game metrics at {Time}", DateTime.UtcNow);

        using var http = httpClientFactory.CreateClient();
        var response = await http.PostAsync(
            "http://fcg-catalog-api:8080/api/games/reindex", null);

        logger.LogInformation(
            "Metrics aggregation completed. Status: {Status}", response.StatusCode);
    }
}
