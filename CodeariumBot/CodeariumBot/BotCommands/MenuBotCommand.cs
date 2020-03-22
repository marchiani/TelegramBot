using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CodeariumBot.BotCommands
{
    public class MenuBotCommand
    {
        public async Task Menu(TelegramBotClient telegramBotClient, Update update)
        {
            Message message = update.Message;

            ReplyKeyboardMarkup ReplyKeyboard = new[]
            {
                new[] { "Причины приобрести бота именно сейчас?" },
                new[] { "Почему бот нужен именно вам?" },
                new[] {""},
            };

            await telegramBotClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: ReplyKeyboard
            );
        }
    }
}
