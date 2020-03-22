using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CodeariumBot.BotCommands
{
    public class CommandDefinition
    {
        private readonly TelegramBotClient TelegramBotClient;
        private readonly WeatherBotCommand WeatherBotCommand;
        private readonly Update Update;

        public CommandDefinition(TelegramBotClient telegramBotClient, Update update)
        {
            TelegramBotClient = telegramBotClient;
            Update = update;
            WeatherBotCommand = new WeatherBotCommand(TelegramBotClient, Update);
        }

        public async Task DefineCommand()
        {
            Message message = Update.Message;

           
            switch (message.Text.Split(' ').First().Trim('/'))
            {
                case "Menu":
                    await new MenuBotCommand().Menu(TelegramBotClient, Update);
                    
                    break;

                case "start":
                    await new MenuBotCommand().Menu(TelegramBotClient, Update);
                    
                    break;

                case "Weather":
                    ReplyKeyboardMarkup RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestLocation("Location"),
                    });
                    await TelegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Send your location please 😉",
                        replyMarkup: RequestReplyKeyboard
                    );
                    break;


                case "Sasha":
                    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"I love you bab");
                    break;

                case "Nikita":
                    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"You are fag!!! HA-ha");
                    break;

                case "Emoji":
                    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"😀");
                    break;

                default:
                    const string usage = "Usage:\n" +
                        "/menu   - send inline keyboard\n" +
                        "/request - Test request\n" +
                        "/Nikita - That's for you Nikita";
                    const string usageForSandra = "Usage:\n" +
                        "/Sasha - if you have bad mood enter this command";
                    await TelegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: message.Chat.Username == "Sandra_Nikitina" ? usageForSandra : usage,
                        replyMarkup: new ReplyKeyboardRemove()
                    );
                    break;
            }
        }
    }
}
