using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherNet.Clients;
using WeatherNet.Model;

namespace CodeariumBot.BotCommands
{
    public class WeatherBotCommand
    {
        private readonly CurrentWeather CurrentWeather;
        private readonly TelegramBotClient TelegramBotClient;
        private readonly Update Update;
        public WeatherBotCommand(TelegramBotClient telegramBotClient, Update update)
        {
            CurrentWeather = new CurrentWeather();
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

            var weather = CurrentWeather.GetByCityName("Kiev", "Ukrain");
            var forecast = FiveDaysForecast.GetByCoordinates(location.Latitude, location.Longitude);


            await TelegramBotClient.SendTextMessageAsync(
                chatId: Update.Message.Chat.Id,
                text: $"{weather.Item.Temp}"
            );

            var weather1 = CurrentWeather.GetByCoordinates(location.Latitude, location.Longitude);

            await TelegramBotClient.SendTextMessageAsync(
                chatId: Update.Message.Chat.Id,
                text: $"{weather1.Item.Temp}"
            );
        }
    }
}
