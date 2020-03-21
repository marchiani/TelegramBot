using CodeariumServices.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace CodeariumServices.Implementations
{
	public class InitialBotService : IInitialBotService
	{
        private static readonly TelegramBotClient telegramBotClient = new TelegramBotClient("1133650379:AAFp_TD2eZemWCFTIlPf0niCcW1nR7G36VQ");
        public async Task EchoAsync(Update update)
        {

            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;


            switch (message.Text.Split(' ').First())
            {
                case "/menu":
                    await telegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(500);

                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
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
                    await telegramBotClient.SendTextMessageAsync(
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
                    await telegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose",
                        replyMarkup: ReplyKeyboard
                    );
                    break;


                case "/request":
                    var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestLocation("Location"),
                        KeyboardButton.WithRequestContact("Contact"),
                    });
                    await telegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Who or Where are you?",
                        replyMarkup: RequestReplyKeyboard
                    );
                    break;


                case "/Sasha":
                    await telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"I love you bab");
                    break;

                case "/Nikita":
                    await telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"You are fag!!! HA-ha");
                    break;

                default:
                    const string usage = "Usage:\n" +
                        "/menu   - send inline keyboard\n" +
                        "/request - Test request\n" +
                        "/Nikita - That's for you Nikita";
                    const string usageForSandra = "Usage:\n" +
                        "/Sasha - if you have bad mood enter this command";
                    await telegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: message.Chat.Username == "Sandra_Nikitina" ? usageForSandra : usage,
                        replyMarkup: new ReplyKeyboardRemove()
                    );
                    break;
            }
        }

    }
}
