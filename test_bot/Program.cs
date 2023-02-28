using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace bot_Application
{
    class Program
    {
        private static TelegramBotClient bot = new TelegramBotClient("6124120673:AAE8WOnQ0VOq00bVsQKwUnrJnaCYshNUCMs");
        private static string mode = "";
        private static InlineKeyboardMarkup ShowInlineButtons = new
        (
            new[]
            {
                InlineKeyboardButton.WithCallbackData("ДА", callbackData:"ДА"),
                InlineKeyboardButton.WithCallbackData("НЕТ", callbackData:"НЕТ")
            }
        );
        private static ReplyKeyboardMarkup ShowMainMenu = new(new[]
        {
            new KeyboardButton[] { "Найти друзей", "Изменить анкету"},
            new KeyboardButton[] { "Инструкция", "Удалить анкету"}
        })
        {
            ResizeKeyboard = true
        };
        private static InlineKeyboardMarkup PleaseHelpMe = new
        (
            new[]
            {
                InlineKeyboardButton.WithCallbackData("ПОМОГИТЕ!!! :(", callbackData:"помогите"),
            }
        );
        private static void Main(string[] args)
        {
            bot.StartReceiving(updateHandler: Update, pollingErrorHandler: Error);
            Console.ReadLine(); 
        }
        async private static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            switch(update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    switch(update.Message.Text + mode)
                    {
                        case "/start":
                            mode = "YesOrNo";
                            await client.SendTextMessageAsync
                            (
                                update.Message.Chat.Id, 
                                "Вас приветствует БОТ для поиска друзей!!!\nВы уже создали свою анкету?", 
                                replyMarkup: ShowInlineButtons
                            );
                        break;
                        case string temp when temp.Contains("registration"):
                            if(IsCorrectData(temp))
                            {
                                mode = "ok";
                                await client.SendTextMessageAsync
                                (
                                    update.Message.Chat.Id, 
                                    "Всё круто!!!\nВот какие возможности вам теперь доступны!!!",
                                    replyMarkup: ShowMainMenu
                                );
                            }
                            else
                            {
                                await client.SendTextMessageAsync
                                (
                                    update.Message.Chat.Id, 
                                    "Введённые вами данные НЕКОРРЕКТНЫ!!!\nПожалуйста проверьте всё внимательно!!!",
                                    replyMarkup: PleaseHelpMe
                                );
                            }
                        break;
                        default:
                            await client.DeleteMessageAsync(chatId: update.Message.Chat.Id, update.Message.MessageId);
                        break;
                    }
                break;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    switch(update.CallbackQuery.Data)
                    {
                        case "ДА":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id, 
                                text: "Прекрасно!!! Удачи!!!", 
                                replyMarkup: ShowMainMenu
                            );
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;
                        case "НЕТ":
                            mode = "registration";
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id, 
                                text: "Ничего страшного, сейчас быстро создадим анкету!!!\nВот что необходимо сделать:\nНеобходимо одним сообщением указать следующие данные\n\n1. Фамилия Имя\n2. Почта\n3. Телефон\n4. Немного о себе\n\n В верхней части сообщения укажите команду /registration\n\n!!!КАЖДЫЙ ПУНКТ ЗАПОЛНЯТЬ С НОВОЙ СТРОКИ, НО В ОДНОМ СООБЩЕНИИ!!!"
                                //replyMarkup: ReplyKeyboardRemove
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id, 
                                text: "Вот пример для наглядности!"
                            );
                            using (var fileStream = new FileStream("D:/Tusur/PROJECT_TEST/B/BOT/registrationExample.PNG", FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                await client.SendPhotoAsync(
                                    chatId: update.CallbackQuery.Message.Chat.Id,
                                    photo: new InputOnlineFile(fileStream)
                                );
                                fileStream.Close();
                            }
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;
                        case "помогите":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id, 
                                text: "Вот что необходимо сделать:\nНеобходимо одним сообщением указать следующие данные\n\n1. Фамилия Имя\n2. Почта\n3. Телефон\n4. Немного о себе\n\n В верхней части сообщения укажите команду /registration\n\n!!!КАЖДЫЙ ПУНКТ ЗАПОЛНЯТЬ С НОВОЙ СТРОКИ, НО В ОДНОМ СООБЩЕНИИ!!!"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id, 
                                text: "Вот пример для наглядности!"
                            );
                            using (var fileStream = new FileStream("D:/Tusur/PROJECT_TEST/B/BOT/registrationExample.PNG", FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                await client.SendPhotoAsync(
                                    chatId: update.CallbackQuery.Message.Chat.Id,
                                    photo: new InputOnlineFile(fileStream)
                                );
                                fileStream.Close();
                            }
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;
                    }
                break;
            }
        }
        private static bool IsCorrectData(string UserInfo)//КУСОК В РАЗРАБОТКЕ
        {
            return false;
        }
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
             throw new NotImplementedException();
        }
    }
}


