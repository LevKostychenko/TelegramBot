using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegrammBot.Core;

namespace TelegrammBot.Commands
{
    public class CalculateLoopCommand : Command
    {
        public override string Name => Identifiers.CalculateLoop;

        public override async void SendMessage(Message message, TelegramBotClient client)
        {
            int pzmCount = 0;
            int daysToAccumulate = 0;
            int addedPzmCount = 0;

            await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterDaysToReinvest);
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

            await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterAddedPZMToLoop);
            string addedPamResponse = GetResponse(message, client);

            if (!Int32.TryParse(addedPamResponse, out addedPzmCount))
            {
                await client.SendTextMessageAsync(message.Chat.Id, Identifiers.EnterCorrectPZMCount);
                return;
            }

            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.ExecuteFile(Identifiers.LoopLogicPath, scope);
            dynamic function = scope.GetVariable("calculateLoop");
            dynamic result = function(daysToAccumulate, pzmCount, addedPzmCount);

            await client.SendTextMessageAsync(message.Chat.Id, result.ToString());
        }

        public override bool Contains(string command)
        {
            return command == this.Name;
        }
    }
}
