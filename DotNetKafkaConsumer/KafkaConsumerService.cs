using Confluent.Kafka;

namespace DotNetKafkaConsumer
{
    /// <summary>
    /// Kafkaコンシューマーサービス
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="service"></param>
    public class KafkaConsumerService(ILogger<KafkaConsumerService> logger, SampleProcessorService service)
        : BackgroundService
    {
        private readonly ConsumerConfig _consumerConfig = new()
        {
            BootstrapServers = "localhost:39092",
            GroupId = "my-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = false
        };

        private readonly string _topic = "my-topic";

        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Kafka Consumer Service running.");

            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);

                    using var scope = logger.BeginScope(new Dictionary<string, object>{ { "transactionId", Guid.NewGuid()}});

                    string value = consumeResult.Message.Value;
                    logger.LogInformation("Consumed message '{Value}' at: '{ConsumeResultTopicPartitionOffset}'.", value, consumeResult.TopicPartitionOffset);

                    // ここで消費したメッセージに対する処理を記述
                    // 例: データベースへの保存、別のサービスへの送信など
                    _ = service.ExecuteAsync(value);

                    consumer.Commit();
                }
                catch (ConsumeException e)
                {
                    logger.LogError("Error occured: {ErrorReason}", e.Error.Reason);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("Kafka Consumer Service stopping.");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError("An unexpected error occurred: {ExMessage}", ex.Message);
                }
            }

            consumer.Close();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Kafka Consumer Service is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
