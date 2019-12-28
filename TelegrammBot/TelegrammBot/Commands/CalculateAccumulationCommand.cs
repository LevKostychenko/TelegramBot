using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegrammBot.Core;
using TelegrammBot.Models;

namespace TelegrammBot.Commands
{
    public class CalculateAccumulationCommand : Command
    {
        public override string Name => Identifiers.CalculateAccumulation;

        public override async void SendMessage(Message message, TelegramBotClient client)
        {
            int pzmCount = 0;
            int daysToAccumulate = 0;

            await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterDaysToAccumulate);
            string daysResponse = GetResponse(message, client);

            if (!Int32.TryParse(daysResponse, out daysToAccumulate))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Some error has occured");
                return;
            }

            if (daysToAccumulate >= 200)
            {
                await client.SendTextMessageAsync(message.Chat.Id, Identifiers.TheNumberIsTooBig);
                return;
            }

            await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterPZMCount);
            string pzmResponse = GetResponse(message, client);

            if (!Int32.TryParse(pzmResponse, out pzmCount))
            {
                await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterCorrectPZMCount);
                return;
            }

            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.ExecuteFile(Identifiers.ReinvestLogicPath, scope);
            dynamic function = scope.GetVariable("calculateReinvest");
            dynamic result = function(daysToAccumulate, pzmCount);

            await client.SendTextMessageAsync(message.Chat.Id, result.ToString());
        }

        public override bool Contains(string command)
        {
            return command == this.Name || command.StartsWith("/accumdays");
        }
    }
}
