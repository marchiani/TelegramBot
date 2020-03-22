using CodeariumServices.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using CodeariumServices.Settings;

namespace CodeariumServices.Implementations
{
    public class InitialBotService : IInitialBotService
    {
        private readonly TelegramBotConfiguration TelegramBotConfiguration;
        private readonly TelegramBotClient TelegramBotClient;

        public InitialBotService(TelegramBotConfiguration telegramBotConfiguration)
        {
            TelegramBotConfiguration = telegramBotConfiguration;
            TelegramBotClient = new TelegramBotClient(TelegramBotConfiguration.Token);
        }
        public async Task EchoAsync(Update update)
        {

            if (update.Type != UpdateType.Message)
            {
                return;
            }

            Message message = update.Message;

            switch (message.Text.Split(' ').First())
            {
                case "/menu":
                    await TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(500);

                    InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("soon", "soon"),
                            InlineKeyboardButton.WithCallbackData("soon", "soon"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("soon", "soon"),
                            InlineKeyboardButton.WithCallbackData("soon", "soon"),
                        }
                    });
                    await TelegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose",
                        replyMarkup: inlineKeyboard
                    );
                    break;

                case "/fun":
                    ReplyKeyboardMarkup ReplyKeyboard = new[]
                    {
                        new[] { "1.1", "1.2" },
                        new[] { "2.1", "2.2" },
                    };
                    await TelegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose",
                        replyMarkup: ReplyKeyboard
                    );
                    break;


                case "/request":
                    ReplyKeyboardMarkup RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestLocation("Location"),
                        KeyboardButton.WithRequestContact("Contact"),
                    });
                    await TelegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Who or Where are you?",
                        replyMarkup: RequestReplyKeyboard
                    );
                    break;


                case "/Sasha":
                    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"I love you bab");
                    break;

                case "/Nikita":
                    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"You are fag!!! HA-ha");
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
