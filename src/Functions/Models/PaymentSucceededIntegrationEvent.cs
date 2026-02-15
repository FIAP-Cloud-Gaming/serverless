namespace Functions.Models;

public record PaymentSucceededIntegrationEvent
{
    public Guid PaymentId { get; init; }
    public string OrderId { get; init; } = default!;
    public string UserId { get; init; } = default!;
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = default!;
    public string? TransactionId { get; init; }
    public string? AuthorizationCode { get; init; }
    public DateTime ProcessedAt { get; init; }
}
