using CodeariumServices.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace CodeariumBot.Extention
{
    public static class SetWebhook
    {

        public static IServiceCollection AddTelegramBotClient
            (
            this IServiceCollection serviceCollection,
            TelegramBotConfiguration telegramBotConfiguration
            )
        {
            TelegramBotClient client = new TelegramBotClient(telegramBotConfiguration.Token);
            string webHook = $"{telegramBotConfiguration.Url}/api/message/update";
            client.SetWebhookAsync(webHook).Wait();

            return serviceCollection
                .AddTransient<ITelegramBotClient>(x => client);
        }
    }
}
