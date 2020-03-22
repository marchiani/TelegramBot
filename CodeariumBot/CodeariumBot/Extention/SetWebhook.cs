using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace CodeariumBot.Extention
{
    public static class SetWebhook
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            TelegramBotClient client = new TelegramBotClient(configuration["TelegrabBotSettings:Token"]);
            string webHook = $"{configuration["TelegrabBotSettings:Url"]}/api/message/update";
            client.SetWebhookAsync(webHook).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(x => client);
        }
    }
}
