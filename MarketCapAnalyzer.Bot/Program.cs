using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarketCapAnalyzer.Bot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("531660114:AAFjja9vTpbDgQRYMnXhTyUrPrw_p75htFA");

        public static void Main(string[] args)
        {
            Bot.OnMessage += BotOnMessageReceived;
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            Bot.StartReceiving();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }



        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)

        {

            var message = messageEventArgs.Message;



            if (message == null || message.Type != MessageType.TextMessage) return;



            //IReplyMarkup keyboard = new ReplyKeyboardRemove();



            switch (message.Text.Split(' ').First())

            {




                default:

                    const string usage = @"Usage:

/inline   - send inline keyboard

/keyboard - send custom keyboard

/photo    - send a photo

/request  - request location or contact

";



                    await Bot.SendTextMessageAsync(

                        message.Chat.Id,

                        usage,

                        replyMarkup: new ReplyKeyboardRemove());

                    break;

            }

        }



    }
}
