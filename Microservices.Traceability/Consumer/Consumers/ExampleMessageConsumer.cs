using MassTransit;
using Shared;
using System.Diagnostics;

namespace Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<ExampleMessage>
    {
        private readonly ILogger<ExampleMessageConsumer> _logger;

        public ExampleMessageConsumer(ILogger<ExampleMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ExampleMessage> context)
        {
            var correlationId = Guid.NewGuid();
            if (context.Headers.TryGetHeader("CorrelationId", out var _correlationId))
                correlationId = Guid.Parse(_correlationId.ToString());

            Trace.CorrelationManager.ActivityId = correlationId;
            _logger.LogDebug("Consumer Log");

            Console.WriteLine($"Gelen mesaj: {context.Message.Text} - CorrelationId: {correlationId}");
            return Task.CompletedTask;

        }
    }
}
