using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegrammBot.Commands;
using TelegrammBot.Core;

namespace TelegrammBot.Models
{
    public static class Bot
    {
        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static async Task<TelegramBotClient> GetClient()
        {
            if (client != null)
            {
                return client;
            }
            else
            {
                client = new TelegramBotClient(Identifiers.Token);
                commandsList = new List<Command>();
                commandsList.Add(new StartCommand());
                commandsList.Add(new CalculateAccumulationCommand());
                commandsList.Add(new CalculateReinvestCommand());
                commandsList.Add(new CalculateLoopCommand());
                //commandsList.Add(new EnterDaysToCalculateCommand());

                await client.SetWebhookAsync("");
            }

            return client;
        }
    }
}
