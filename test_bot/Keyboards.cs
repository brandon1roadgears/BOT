
using Telegram.Bot.Types.ReplyMarkups;

internal class Keyboards
{
    internal static InlineKeyboardMarkup ShowInlineButtons = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("ДА", callbackData:"yes"),
            InlineKeyboardButton.WithCallbackData("НЕТ", callbackData:"no")
        }
    );
    internal static InlineKeyboardMarkup AgreeWithDelete = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("ДА", callbackData:"yes_delete"),
            InlineKeyboardButton.WithCallbackData("НЕТ", callbackData:"no_delete")
        }
    );
    internal static InlineKeyboardMarkup MainMenuKeyBoard = new
    (
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Найти друзей", callbackData: "findFriend"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Инструкция", callbackData: "instruction"),
                InlineKeyboardButton.WithCallbackData("Удалить анкету", callbackData: "deleteForm")
            }
        }
        
    );
    internal static InlineKeyboardMarkup FriendShipButton = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Добавить в друзья", callbackData:"FriendshipRequest"),
        }
    );
    internal static List<List<InlineKeyboardButton>> hobbiesList = new List<List<InlineKeyboardButton>>()
    {
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("футболл", callbackData: "футболл/h0"),
            InlineKeyboardButton.WithCallbackData("волейболл", callbackData: "волейболл/h1"),
            InlineKeyboardButton.WithCallbackData("баскетбол", callbackData: "баскетбол/h2"),
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("хоккей", callbackData: "хоккей/h0"),
            InlineKeyboardButton.WithCallbackData("фитнес", callbackData: "фитнес/h1"),
            InlineKeyboardButton.WithCallbackData("йога", callbackData: "йога/h2"),
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("тренажерный зал", callbackData: "тренажерный зал/h0"),
            InlineKeyboardButton.WithCallbackData("боевые искусства", callbackData: "боевые искусства/h1"),
            InlineKeyboardButton.WithCallbackData("плавание", callbackData: "плавание/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("рок", callbackData: "рок/h0"),
            InlineKeyboardButton.WithCallbackData("поп", callbackData: "poпопp/h1"),
            InlineKeyboardButton.WithCallbackData("русская музыка", callbackData: "русская музыка/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("зарубежная музка", callbackData: "зарубежная музка/h0"),
            InlineKeyboardButton.WithCallbackData("фильмы", callbackData: "фильмы/h1"),
            InlineKeyboardButton.WithCallbackData("сериалы", callbackData: "сериалы/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("аниме", callbackData: "аниме/h0"),
            InlineKeyboardButton.WithCallbackData("мультфильмы", callbackData: "мультфильмы/h1"),
            InlineKeyboardButton.WithCallbackData("книги", callbackData: "книги/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("комиксы", callbackData: "комиксы/h0"),
            InlineKeyboardButton.WithCallbackData("манга", callbackData: "манга/h1"),
            InlineKeyboardButton.WithCallbackData("новеллы", callbackData: "новеллы/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("путешествия", callbackData: "путешествия/h0"),
            InlineKeyboardButton.WithCallbackData("прогулки", callbackData: "прогулки/h1"),
            InlineKeyboardButton.WithCallbackData("велосипедная прогулка", callbackData: "велосипедная прогулка/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("рыбалка", callbackData: "рыбалка/h0"),
            InlineKeyboardButton.WithCallbackData("охота", callbackData: "охота/h1"),
            InlineKeyboardButton.WithCallbackData("бары", callbackData: "бары/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("клубы", callbackData: "клубы/h0"),
            InlineKeyboardButton.WithCallbackData("кофейни", callbackData: "кофейни/h1"),
            InlineKeyboardButton.WithCallbackData("рестораны", callbackData: "рестораны/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("боулинг", callbackData: "боулинг/h0"),
            InlineKeyboardButton.WithCallbackData("бильярд", callbackData: "бильярд/h1"),
            InlineKeyboardButton.WithCallbackData("пиццерия", callbackData: "пиццерия/h1")
        },                
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("караоке", callbackData: "караоке/h0"),
            InlineKeyboardButton.WithCallbackData("рисование", callbackData: "рисование/h1"),
            InlineKeyboardButton.WithCallbackData("фотография", callbackData: "фотография/h1")
        },
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("компьютерные игры", callbackData: "компьютерные игры/h0"),
            InlineKeyboardButton.WithCallbackData("настольные игры", callbackData: "настольные игры/h1"),
            InlineKeyboardButton.WithCallbackData("хендмейд", callbackData: "хендмейд/h1")
        },                
        new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("вокал/пение", callbackData: "вокал/пение/h1")
        }
    };
}