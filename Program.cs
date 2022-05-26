using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using VoreRandomBot;
using TBParser;

namespace TelegramBotExperiments
{

    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient(configToken.Token);
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message; 
                if(message.Text != null)//проверка на пустоту
                    switch (message.Text.ToLower())//переключатель
                    {
                        case "/start":
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать на борт, добрый путник!");
                            return;
                        };
                        case "/help":
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Сейчас я вам помогу");
                            return;
                        };
                        case "/ping_random":
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, message.Chat.Id.ToString());
                                TBParser.ChatParser.membersParser();
                                return;
                            };
                        default:
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Дайте мне команду (пр. /help)");
                                return;
                            }
                    } 
                else
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
           // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}