using server_red.Models;

namespace server_red
{
    public interface IRedRepository
    {
        // Авторизация
        //List<string> SignIn(string username, string password);
        string SignIn(string username, string password);

        // Создание аккаунта
        string CreateAccount(string username, string password, string question, string answer);

        // Восстановление пароля - запрос никнейма
        ReqNick ReqNick(string username);

        // Восстановление пароля - ответ на контрольный вопрос
        string AnsQues(string token, string answer);

        // Изменить пароль
        string ChangePass(string token, string newPass);

        // Получение списка друзей
        List<User> GetFriendlist(string cur_session);

        // Получение списка уведомлений
        List<Notif> GetNotiflist(string cur_session);

        // Получение иконки, никнейма, количества очков
        UserScore GetUserScore(string cur_session);

        // Поиск пользователя
        User UserSearch(string username);

        // Добавить пользователя в друзья
        int AddFriend(string cur_session, int f_id);

        // Удалить пользователя из друзей
        int DeleteFriend(string cur_session, int f_id);

        // Отправить приглашеие в пользовательский матч
        int InviteFriend(string cur_session, int f_id, int move_time, int rules_id);

        // Получение цвета текущего хода
        string GetActiveColor (string cur_session);

        // Получение флага ударного хода
        int GetBeatFlag(string cur_session);

        // Получение всех белых шашек
        List<Checker> SessionCheckersWhite(string cur_session);

        // Получение всех черных шашек
        List<Checker> SessionCheckersBlack(string cur_session);

        //Изменение цвета шашек текущего хода
        int SetActiveColor(string cur_session, string color);

        //Задание флага ударного хода
        int SetBeatFlag(string cur_session, int beat_flag);

        // Обновление списка шашек (вызывается последовательно для каждой шашки)
        int UpdateCheckersField(string cur_session, string color, int fx, int fy, int king, int beaten, bool delete_old);

        // Принять приглашение в пользовательский матч
        int AcceptMatch(string cur_session, string org_nick);

        // Отозвать приглашение в пользовательский матч
        int RevokeMatch(string cur_session, int f_id);

        // Отклонить приглашение в пользовательский матч
        int RejectMatch(string cur_session, string org_nick);

        // Сдаться
        int GiveUp(string cur_session);

    }
}
