namespace DotNetKafkaConsumer
{
    /// <summary>
    /// サンプルサービス
    /// </summary>
    /// <param name="logger"></param>
    public class SampleProcessorService(ILogger<SampleProcessorService> logger)
    {
        /// <summary>
        /// ビジネスロジック処理
        /// </summary>
        /// <param name="value">サンプル値</param>
        /// <returns><see cref="Task"/></returns>
        public async Task ExecuteAsync(string value)
        {
            logger.LogInformation("SampleProcessorService start. value:{value}.", value);

            await Task.Delay(5000);

            logger.LogInformation("SampleProcessorService end. value:{value}.", value);
        }
    }
}
