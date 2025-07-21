# dotnet-kafka-consumer
.NET 汎用ホスト の Kadkaコンシューマーアプリケーション サンプル

## Feature
- .NET8 (汎用ホスト)
- Kafka
- Docker

## Note
- docker compose を用いて、ローカル環境用の Kafkaクラスタ、kafka-ui を起動します。

```
docker compose up -d
```

- `http://localhost:8080` にアクセスすることで、kafka-ui へアクセス出来ます。


- Visual Studio より .NETアプリケーション を起動し、Kafkaコンシューマーを有効化します。
  - kafka-ui より メッセージを登録することで、コンシューマーによるメッセージ受信を確認出来ます。

```json
{
  "Timestamp": "16:06:32 ",
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "DotNetKafkaConsumer.KafkaConsumerService",
  "Message": "Kafka Consumer Service running.",
  "State": {
    "Message": "Kafka Consumer Service running.",
    "{OriginalFormat}": "Kafka Consumer Service running."
  },
  "Scopes": []
}
{
  "Timestamp": "16:06:43 ",
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "DotNetKafkaConsumer.KafkaConsumerService",
  "Message": "Consumed message '{ \"key\": 101 }' at: 'my-topic [[0]] @23'.",
  "State": {
    "Message": "Consumed message '{ \"key\": 101 }' at: 'my-topic [[0]] @23'.",
    "Value": "{ \"key\": 101 }",
    "ConsumeResultTopicPartitionOffset": "my-topic [[0]] @23",
    "{OriginalFormat}": "Consumed message '{Value}' at: '{ConsumeResultTopicPartitionOffset}'."
  },
  "Scopes": [
    {
      "Message": "System.Collections.Generic.Dictionary`2[System.String,System.Object]",
      "transactionId": "9f10d6f0-506d-4863-a53e-6ef7b982dded"
    }
  ]
```