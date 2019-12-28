using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegrammBot.Core;

namespace TelegrammBot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async void SendMessage(Message message, TelegramBotClient client)
        {
            ReplyKeyboardMarkup replyKeyboard = new[]
                {
                    new[] { Identifiers.CalculateAccumulation, Identifiers.CalculateReinvest, Identifiers.CalculateLoop},
                };

            long chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, "Choose action", replyMarkup: replyKeyboard);
        }

        public override bool Contains(string command)
        {
            return command == this.Name;
        }
    }
}
