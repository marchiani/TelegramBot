using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CodeariumServices.Interfaces
{
	public interface IInitialBotService
	{
		Task EchoAsync(Update update);
	}
}
