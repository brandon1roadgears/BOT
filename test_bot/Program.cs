using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

using System;
using System.IO;
using System.Text;
using System.Globalization;

using static Keyboards;

namespace bot_Application
{
    class Program
    {
        private static TelegramBotClient bot = new TelegramBotClient("6124120673:AAE8WOnQ0VOq00bVsQKwUnrJnaCYshNUCMs");
        private static List<string> hobbies_user = new List<string>();
        private static List<List<InlineKeyboardButton>> tempHobbiesList = new List<List<InlineKeyboardButton>>();
        private static string mode = "";
        private static string userInfo = "";
        private static short countOfHobbies = 0;

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
                        case string temp when temp.Contains("/restart"):
                            mode = "";
                        break;

                        case "/start":
                            mode = "delete_message";
                            await client.SendTextMessageAsync
                            (
                                update.Message.Chat.Id, 
                                "Вас приветствует БОТ для поиска друзей!!!\nВы уже создали свою анкету?", 
                                replyMarkup: ShowInlineButtons
                            );
                        break;

                        case string temp when temp.Contains("r1e2g3i4s5t6r7a8t9ion"):
                            userInfo = temp.Remove(temp.Length-21);
                            short f0 = 0, f1 = 0, f2 = 0, f3 = 0, f4 = 0, f5 = 0;
                            if(IsCorrectData(ref userInfo,ref f0,  ref f1, ref f2, ref f3, ref f4, ref f5))
                            {
                                mode = "delete_message";
                                tempHobbiesList.Clear();
                                hobbies_user.Clear();
                                for(int i = 0; i < hobbiesList.Count; ++i)
                                {
                                    tempHobbiesList.Add(new List<InlineKeyboardButton>(hobbiesList[i].Count));
                                    for(int j = 0; j < hobbiesList[i].Count; ++j)
                                    {
                                        tempHobbiesList[i].Add(hobbiesList[i][j]);
                                    }
                                }
                                await UpdateHobbiesKeyboard(client, update, update.Message.Chat.Id);
                            }
                            else
                            {
                                await client.SendTextMessageAsync
                                (
                                    update.Message.Chat.Id, 
                                    "Введённые вами данные НЕКОРРЕКТНЫ!!!\nПожалуйста проверьте всё внимательно!!!"
                                );
                                if(f0 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Вы ввели не все данные\nПервые 5 полей оязательны для заполнения"
                                    );
                                    return;
                                }
                                if(f1 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Некорректно введено ИМЯ"
                                    );
                                }
                                if(f2 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Некорректно введен ВОЗРАСТ"
                                    );
                                }
                                if(f3 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Некорректно введен ТЕЛЕФОН"
                                    );
                                }
                                if(f4 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Некорректно введена ПОЧТА"
                                    );
                                }
                                if(f5 == 0)
                                {
                                    await client.SendTextMessageAsync
                                    (
                                        chatId: update.Message.Chat.Id,
                                        text: "Некорректно введен ГОРОД"
                                    );
                                }
                            }
                        break;

                        default:
                            await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                        break;
                    }
                break;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:             
                    switch(update.CallbackQuery.Data)
                    {
                        case "yes":
                            mode = "delete_message";
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            if(!IsUserExist(update.CallbackQuery.Message.Chat.Id))
                            {   
                                mode = "r1e2g3i4s5t6r7a8t9ion";
                                await client.SendTextMessageAsync
                                (
                                    chatId : update.CallbackQuery.Message.Chat.Id,
                                    text: "Простите, но я не могу найти ваш профиль!!!\nДля корректной работы стоит зарегистрироваться!!!"
                                );
                                await ExplainForSingUp(client, update);    
                            }
                            else
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.CallbackQuery.Message.Chat.Id, 
                                    text: "Удачи!!!", 
                                    replyMarkup: MainMenuKeyBoard 
                                );
                            }
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "no":
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            if(IsUserExist(update.CallbackQuery.Message.Chat.Id))
                            {
                                mode = "delete_message";
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.CallbackQuery.Message.Chat.Id,
                                    text: "Извините, но у вас уже есть профиль в нашем сервисе!!!\nНи каких действий от вас не требуется. Вы уже можете пользоваться основными функциями нашего сервиса\n\nУДАЧИ!!!",
                                    replyMarkup: MainMenuKeyBoard
                                );
                            }
                            else
                            {
                                mode = "r1e2g3i4s5t6r7a8t9ion";
                                await ExplainForSingUp(client, update);
                            }
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "help":
                            await DeleteMessage(client, chatId: update.CallbackQuery.Message.Chat.Id, messageId: update.CallbackQuery.Message.MessageId-1);
                            await DeleteMessage(client, chatId: update.CallbackQuery.Message.Chat.Id, messageId: update.CallbackQuery.Message.MessageId);
                            ExplainForSingUp(client, update);
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case string temp when temp.Contains("/h"):
                            ++countOfHobbies;
                            for(int i = 0; i < tempHobbiesList.Count; ++i)
                            {
                                for(int j = 0; j < tempHobbiesList[i].Count; ++j)
                                {
                                    if(tempHobbiesList[i][j].CallbackData.Contains(temp))
                                    {
                                        tempHobbiesList[i].RemoveAt(j);    
                                    }
                                }
                            }
                            hobbies_user.Add(temp.Remove(temp.Length - 3));
                            if(countOfHobbies == 5)
                            {
                                countOfHobbies = 0;
                                await DeleteMessage(client, chatId: update.CallbackQuery.Message.Chat.Id, messageId: update.CallbackQuery.Message.MessageId);
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.CallbackQuery.Message.Chat.Id,
                                    text: "Замечательно!!! Регистрация прошла успешно!!!\nТеперь вам доступны следующие функции",
                                    replyMarkup: MainMenuKeyBoard
                                );
                                SignUpUser(userInfo, update.CallbackQuery.Message.Chat.Id, hobbies_user);
                                mode = "delete_message";
                                await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                            }
                            else
                            {
                                await DeleteMessage(client, chatId: update.CallbackQuery.Message.Chat.Id, messageId: update.CallbackQuery.Message.MessageId);
                                await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                                await UpdateHobbiesKeyboard(client, update, update.CallbackQuery.Message.Chat.Id);
                            }
                        break;
                        case "instruction" :
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "ВСЁ очень просто!!!\n1. Нажав на кнопку найти друзей, бот выдаст вам набор анкет пользователей, чьи интересы наиболее близки к вам.\n2. После этого вы можете нажать на любую из показанных анкет, а владелец анкеты получит уведомление о том, что его анкета была выбрана вами.\n3. Далее если этот пользователь согласится продолжить общение, вам обоим придут ссылки на профили друг друга!\n\nВсего хорошего!!!",
                                replyMarkup: MainMenuKeyBoard
                            );
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "findFriend":
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            await GetForms(client: client, update: update, userID: update.CallbackQuery.Message.Chat.Id);
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "FriendshipRequest":
                        break;

                        case "deleteForm":
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Вы уверены что хотите удалить вашу анкету?",
                                replyMarkup: AgreeWithDelete
                            );
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "yes_delete":
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            DeleteForm(update.CallbackQuery.Message.Chat.Id);
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "no_delete":
                            await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                            await client.SendTextMessageAsync
                            (
                                text: "Удачи!!!",
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                replyMarkup: MainMenuKeyBoard
                            );
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);

                        break;
                    }
                break;
            }
        }


        async private static Task ExplainForSingUp(ITelegramBotClient client, Update update)
        {
            await client.SendTextMessageAsync
            (
                chatId: update.CallbackQuery.Message.Chat.Id, 
                text: "Ничего страшного, сейчас быстро создадим анкету!!!\nВот что необходимо сделать:\nНеобходимо одним сообщением указать следующие данные\n\n1. Имя Фамилия\n2. Возраст (только число)\n3. Телефон (начинается с +)\n4. Почта\n5. Город\n6.Немного о себе\n\n!!!ПЕРВЫЕ 5 ПУНКТОВ ЗАПОЛНЯЮТСЯ С НОВОЙ СТРОКИ, НО В ОДНОМ СООБЩЕНИИ!!!\n6 ПУНКТ МОЖНО ЗАПОЛНЯТЬ СВОБОДНО\n\nДля перехода на новую строку на ПК, необходимо нажать комбинацию клавишь SHIFT + ENTER."
            );
            await client.SendTextMessageAsync
            (
                chatId: update.CallbackQuery.Message.Chat.Id, 
                text: "Вот пример для наглядности!"
            );
            using (var fileStream = new FileStream("D:/Tusur/PROJECT_TEST/B/BOT/registrationExample.PNG", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await client.SendPhotoAsync
                (
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    photo: new InputOnlineFile(fileStream)
                );
                fileStream.Close();
            }
        }
        async private static Task DeleteMessage(ITelegramBotClient client, long chatId, int messageId)
        {
            await client.DeleteMessageAsync
            (
                chatId : chatId,
                messageId : messageId
            );
        }
        async private static Task UpdateHobbiesKeyboard(ITelegramBotClient client, Update update, long chatId)
        {
            await client.SendTextMessageAsync
            (
                    chatId, 
                    "Всё круто!!!\nТеперь осталось выбрать круг ваших интересов!\nВыберите не больше 5 интересов\nОСТАЛОСЬ" + "  " + "("+(5 - countOfHobbies) + ")",
                    replyMarkup: new InlineKeyboardMarkup(tempHobbiesList)
            );
        }
        private static bool IsCorrectData(ref string UserInfo, ref short f0, ref short f1, ref short f2, ref short f3, ref short f4, ref short f5) //Проверка корректности данных, которые ввёл пользователь
        {
            short test_code = 0;
            string[] data = UserInfo.Split(char.ConvertFromUtf32(10));
            userInfo = "";
            if(data.Length < 5)
            {
                return false;
            }
            f0 = 1;
            f1 = checkName(ref data[0]);  // имя
            f2 = checkAge(ref data[1]);   // возраст
            f3 = checkPhone(ref data[2]); // телефон
            f4 = checkMail(ref data[3]);  // почта
            f5 = checkCity(ref data[4]); // Город
            test_code = (short)(f1 + f2 + f3 + f4 + f5);
            foreach(string i in data)
            {
                userInfo += i + char.ConvertFromUtf32(10);
            }
            if(test_code != 5) return false;
            return true;
        }
        static short checkName(ref string name)
        {
            name.Trim();
            string[] temp = name.Split(' ');
            if(temp.Length != 2) return 0;
            for(int i = 0; i < temp.Length; ++i)
            {
                for(int j = 0; j < temp[i].Length; ++j)
                {
                    if(!char.IsLetter(temp[i][j])) return 0;    
                }
            }
            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            return 1;
        }
        static short checkPhone(ref string phone)
        {
            phone = phone.Replace(' '.ToString(), string.Empty);
            if(!phone.StartsWith('+')) return  0;
            for(int i = 1; i < phone.Length; ++i)
            {
                if(!char.IsDigit(phone[i])) return 0;
            }
            if(phone.Length < 12 || phone.Length > 17)
            {
                return 0;
            }
            return 1;
        }
        static short checkAge(ref string age)
        {
            age.Trim();
            if(age.Length != 2) return 0;
            foreach(char i in age)
            {
                if(!char.IsDigit(i)) return 0;
            }
            if((age.Length == 2 && age.StartsWith('1')) || age.EndsWith('0') || age.EndsWith('5') || age.EndsWith('6') || age.EndsWith('7') || age.EndsWith('8') || age.EndsWith('9'))
            {
                age += " лет";
                return 1;
            }
            if(age.EndsWith('1'))
            {
                age += " год";
                return 1;
            }
            if(age.EndsWith('2') || age.EndsWith('3') || age.EndsWith('4'))
            {
                    age += " года";
                    return 1;
            }
            return 1;
        }
        static short checkMail(ref string mail)
        {
            if(!mail.Contains('@')) return 0;
            mail = mail.Trim();
            int dog_pos = 0;
            for(int i = 0; i < mail.Length; ++i)
            {
                if(mail[i] == '@')
                {
                    dog_pos = i;
                    break;
                }
            }
            short flag = 0;
            for(int i = dog_pos; i >= 0; --i)
            {
                if(mail[i] != ' ')
                {
                    ++flag;
                    break;
                }
            }
            for(int i = dog_pos; i < mail.Length; ++i)
            {
                if(mail[i] != ' ')
                {
                    ++flag;
                    break;
                }
            }
            if(flag != 2) return 0;
            return 1;
        }
        static short checkCity(ref string city)
        {
            foreach(char i in city)
            {
                if(char.IsDigit(i)) return 0;
            }
            city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
            return 1;
        }
        private static bool IsUserExist(long userID) //Проверка на существование анкеты пользвателя
        {
            string path = "D:/Tusur/PROJECT_TEST/B/UserData/" + userID.ToString();
            DirectoryInfo dir = new DirectoryInfo(path);
            if(!dir.Exists)
            {
                return false;
            }
            return true;
        }
        private static void SignUpUser(string UserInfo, long userID, List<string> UserHobbies) //Добавления данных о пользователе в файл
        {
            string path = "D:/Tusur/PROJECT_TEST/B/UserData";
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.CreateSubdirectory(userID.ToString());
            path += "/" + userID.ToString() + "/User_info.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine("ID: " + userID);
                foreach(var i in UserHobbies)
                {
                    writer.WriteLine(i);
                }
                writer.WriteLine(userInfo);
                writer.Close();
            }
        }
        async private static Task GetForms (ITelegramBotClient client, Update update, long userID)
        {
            string path = "D:/Tusur/PROJECT_TEST/B/UserData/";
            foreach(var i in new DirectoryInfo(path).GetDirectories())
            {
                if(i.Name == userID.ToString())
                {
                    continue;
                }
                using (StreamReader reader = new StreamReader(path + i.Name + "/User_info.txt"))
                {
                    userInfo = reader.ReadToEnd();
                    string[] user = userInfo.Split(char.ConvertFromUtf32(10));
                    userInfo = "";
                    userInfo += user[6] + char.ConvertFromUtf32(10) + user[7] + char.ConvertFromUtf32(10) + user[10] + "\n\n======ИНТЕРЕСЫ======\n\n" + user[1] + char.ConvertFromUtf32(10) + user[2] + char.ConvertFromUtf32(10) + user[3] + char.ConvertFromUtf32(10) + user[4] + char.ConvertFromUtf32(10) + user[5] + char.ConvertFromUtf32(10);
                    for(int j = 11; j < user.Length; ++j)
                    {
                        userInfo += user[j] + char.ConvertFromUtf32(10);
                    }
                    reader.Close();
                }
                await client.SendTextMessageAsync
                (
                    chatId: userID,
                    text: userInfo,
                    replyMarkup: FriendShipButton
                );
            }
        }
        private static void DeleteForm(long userID) //Удаление анкеты пользователя
        {   
            new DirectoryInfo("D:/Tusur/PROJECT_TEST/B/UserData/" + userID.ToString()).Delete(true);            
        }
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
             throw new NotImplementedException();
        }
    }
}


