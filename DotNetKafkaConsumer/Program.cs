using System.Text.Encodings.Web;
using System.Text.Json;

namespace DotNetKafkaConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Logging
                .ClearProviders()
                .AddJsonConsole(options =>
            {
                options.IncludeScopes = true;
                options.TimestampFormat = "HH:mm:ss ";
                options.JsonWriterOptions = new JsonWriterOptions
                {
                    Indented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                };
            });

            builder.Services.AddHostedService<KafkaConsumerService>();
            builder.Services.AddTransient<SampleProcessorService>();

            var host = builder.Build();
            host.Run();
        }
    }
}