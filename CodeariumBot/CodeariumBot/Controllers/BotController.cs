using CodeariumBot.BotCommands;
using CodeariumServices.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CodeariumBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly TelegramBotConfiguration TelegramBotConfiguration;
        private readonly TelegramBotClient TelegramBotClient;

        public BotController(TelegramBotConfiguration telegramBotConfiguration)
        {
            TelegramBotConfiguration = telegramBotConfiguration;
            TelegramBotClient = new TelegramBotClient(TelegramBotConfiguration.Token);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update == null)
            {
                return Ok();
            }

            await new CommandDefinition(TelegramBotClient, update).DefineCommand();

            return Ok();
        }
    }
}