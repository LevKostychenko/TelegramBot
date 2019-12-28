using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegrammBot.Core;

namespace TelegrammBot.Commands
{
    public abstract class Command
    {
        protected List<Update> updates = new List<Update>();

        public abstract string Name { get; }

        public abstract void SendMessage(Message message, TelegramBotClient client);

        public virtual bool Contains(string command)
        {
            return command.Contains(this.Name) && command.Contains(Identifiers.Name);
        }

        protected async void RememberUpdates(object sender, UpdateEventArgs updateEventArgs)
        {
            await Task.Run(() => this.updates.Add(updateEventArgs.Update));
        }

        protected string GetResponse(Message message, TelegramBotClient client)
        {
            string response = string.Empty;

            client.OnUpdate += RememberUpdates;

            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < updates.Count; j++)
                {
                    if (updates[j].Message.From.Id == message.From.Id)
                    {
                        response = updates[j].Message.Text;
                        i = 600;
                        break;
                    }
                }

                Thread.Sleep(100);
            }

            client.OnUpdate -= RememberUpdates;
            updates.Clear();

            return response;
        }
    }
}
