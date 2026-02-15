# Serverless - Azure Functions

Azure Functions para o FIAP Cloud Gaming, executando tarefas assincronas e agendadas.

## Functions

| Function | Trigger | Descricao |
|----------|---------|-----------|
| `NotifyPaymentSucceeded` | RabbitMQ (`payment-succeeded-queue`) | Envia notificacao quando pagamento e aprovado |
| `ProcessPendingPayments` | Timer (a cada 5 minutos) | Processa pagamentos pendentes via Payment API |
| `AggregateGameMetrics` | Timer (a cada 1 hora) | Recalcula metricas de jogos via Catalog API reindex |

## Execucao Local

```bash
# Requer Azure Functions Core Tools
cd Serverless/src/Functions
func start

# Ou via .NET
cd Serverless
dotnet run --project src/Functions
```

## Configuracao

Arquivo `local.settings.json`:

```json
{
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "RabbitMQ": "amqp://guest:guest@localhost:5672"
  }
}
```

## Stack

- .NET 10.0 (isolated worker)
- Azure Functions v4
- RabbitMQ trigger
- Timer triggers (cron)
- IHttpClientFactory para chamadas HTTP
