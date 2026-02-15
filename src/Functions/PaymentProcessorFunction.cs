using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions;

public class PaymentProcessorFunction(IHttpClientFactory httpClientFactory)
{
    [Function("ProcessPendingPayments")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo timer,
        FunctionContext context)
    {
        var logger = context.GetLogger<PaymentProcessorFunction>();
        logger.LogInformation("Processing pending payments at {Time}", DateTime.UtcNow);

        using var http = httpClientFactory.CreateClient();
        var response = await http.PostAsync(
            "http://fcg-payment-api:8080/api/payments/process-pending", null);

        logger.LogInformation(
            "Processed pending payments. Status: {Status}", response.StatusCode);
    }
}
