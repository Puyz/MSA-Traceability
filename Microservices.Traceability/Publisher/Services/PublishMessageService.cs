using MassTransit;
using Shared;
using System.Diagnostics;
using System.Text.Json;

namespace Publisher.Services
{
    public class PublishMessageService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<PublishMessageService> _logger;

        public PublishMessageService(IPublishEndpoint publishEndpoint, ILogger<PublishMessageService> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var correlationId = Guid.NewGuid();

            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{++i}. message",
                };

                Trace.CorrelationManager.ActivityId = correlationId;
                _logger.LogDebug("Publisher log");

                await Console.Out.WriteLineAsync($"{JsonSerializer.Serialize(message)} - Correlation Id: {correlationId}");

                await _publishEndpoint.Publish(message, context =>
                {
                    context.Headers.Set("CorrelationId", correlationId);
                });

                await Task.Delay(750, stoppingToken);
            }
        }
    }
}
