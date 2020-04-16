using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CodeariumBot.BotCommands
{
    public class WeatherBotCommand
    {
        private readonly TelegramBotClient TelegramBotClient;
        private readonly Update Update;
        public WeatherBotCommand(TelegramBotClient telegramBotClient, Update update)
        {
            TelegramBotClient = telegramBotClient;
            Update = update;
        }
        public async Task RequestLocation()
        {
            ReplyKeyboardMarkup RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestLocation("Location"),
                    });

            await TelegramBotClient.SendTextMessageAsync(
                chatId: Update.Message.Chat.Id,
                text: "Send your location please 😉",
                replyMarkup: RequestReplyKeyboard
            );
        }
        public async Task WeatherForecast(Location location)
        {


            await TelegramBotClient.SendTextMessageAsync(
                chatId: Update.Message.Chat.Id,
                text: $"{""}"
            );


            await TelegramBotClient.SendTextMessageAsync(
                chatId: Update.Message.Chat.Id,
                text: $"{""}"
            );
        }
    }
}
