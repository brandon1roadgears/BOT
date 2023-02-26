using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot_Application
{
    class _MainProgramm
    {
        private static bool isHello = false, isYesOrNo = false, isTypingInfo = false;
        private static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("6218350946:AAHR0Z4Z5M36BNVmkGEmfdU__JA_CKk3Jgs");
            bot.StartReceiving(Update, Error);
            Console.ReadLine();
        }
        async private static Task Update(ITelegramBotClient client, Update upd, CancellationToken token)
        {
            if(upd.Message.Text != null )
            {      
                if(isTypingInfo)
                {
                    string info = upd.Message.Text;
                    /*Проверка корректности ввода данных от пользователя*/
                }
                if (isHello && isYesOrNo)
                {
                    await client.SendTextMessageAsync(
                            upd.Message.Chat.Id, 
                            "Не совсем вас понял, но вот что можно сделать", 
                            replyMarkup: replyMainMenuWithYesKeyboardMarkup);
                }
                if(isYesOrNo)
                {
                    return;
                }
                if(upd.Message.Text == "ДА")
                {
                    isHello = true;
                    isYesOrNo = true;
                        await client.SendTextMessageAsync(
                            upd.Message.Chat.Id, 
                            "Что интересует?", 
                            replyMarkup: replyMainMenuWithYesKeyboardMarkup
                        );
                    /*Проверка анкеты*/
                }
                else if(upd.Message.Text == "НЕТ")
                {
                    isHello = true;
                        await client.SendTextMessageAsync(
                            upd.Message.Chat.Id, 
                            "Ничего страшного, сейчас вместе создадим вашу анкету!\nОдним сообщением введите следующую информацию:\n1. Фамилия Имя\n2. Адрес электронной почты\n3. Номер телефона\n!!!ОБЯЗАТЕЛЬНО!!! каждый пункт заполняйте на новой строке но в одном сообщении!",
                            replyMarkup: new ReplyKeyboardRemove()
                        );
                }
                if(isHello)
                {
                    return;
                }
                await client.SendTextMessageAsync(
                    upd.Message.Chat.Id, 
                    "Вас приветствует бот для поиска полезных контактов! У вас уже есть анкета?", 
                    replyMarkup: replyMainMenuWithNoOrYesKeyboardMarkup);   

            }
        }
        private static bool CheckForm() /*КУСОК В РАЗРАБОТКЕ*/
        {
            return true;
        }
        private static ReplyKeyboardMarkup replyMainMenuWithYesKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Найти друзей", "Изменить анкету"},
            new KeyboardButton[] { "Инструкция", "Удалить анкету"}
        })
        {
            ResizeKeyboard = true
        };
        private static ReplyKeyboardMarkup replyMainMenuWithNoOrYesKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "ДА", "НЕТ"}
        })
        {
            ResizeKeyboard = true
        };
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
             throw new NotImplementedException();
        }
    }
}


