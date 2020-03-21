using CodeariumServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CodeariumBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IInitialBotService InitialBotService;
        public BotController(IInitialBotService initialBotService, ITelegramBotClient telegramBotClient)
        {
            InitialBotService = initialBotService;
            _telegramBotClient = telegramBotClient;
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


            await InitialBotService.EchoAsync(update);

            return Ok();
        }
    }
}