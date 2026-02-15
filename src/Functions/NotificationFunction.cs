using Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions;

public class NotificationFunction(ILogger<NotificationFunction> logger)
{
    [Function("NotifyPaymentSucceeded")]
    public async Task Run(
        [RabbitMQTrigger("payment-succeeded-queue", ConnectionStringSetting = "RabbitMQ")]
        PaymentSucceededIntegrationEvent message)
    {
        logger.LogInformation(
            "Payment {PaymentId} succeeded for user {UserId}. Amount: {Amount} {Currency}",
            message.PaymentId, message.UserId, message.TotalAmount, message.Currency);

        await SendNotificationAsync(message);
    }

    private Task SendNotificationAsync(PaymentSucceededIntegrationEvent evt)
    {
        // Em producao: SendGrid, Azure Communication Services, etc.
        logger.LogInformation(
            "Notification sent to user {UserId} for payment {PaymentId}",
            evt.UserId, evt.PaymentId);
        return Task.CompletedTask;
    }
}
