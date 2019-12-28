using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegrammBot.Models;
using System.Collections.Generic;
using TelegrammBot.Commands;
using System.Linq;

namespace TelegrammBot
{
    class Program
    {
        static TelegramBotClient client = Bot.GetClient().Result;

        static void Main(string[] args)
        {
            client.OnMessage += BotOnMessageReceivedAsync;
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }

        public static async void BotOnMessageReceivedAsync(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;           
            
            IReadOnlyList<Command> commands = Bot.Commands;
            Command suitCommand = commands.Where(i => i.Contains(message.Text)).FirstOrDefault();

            if (suitCommand == null)
            {
                return;
            }

            suitCommand.SendMessage(message, client);
        }        
    }
}
