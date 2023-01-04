using Oracle.ManagedDataAccess.Client;

namespace server_red
{
    public class DBConnection
    {
        private static OracleConnection? oraCon;

        public static OracleConnection? getOraCon()
        {
            return oraCon;
        }


        public static void StartupInitNewConnection(IConfiguration config)
        {
            string? user = config.GetSection("ConnectionStrings").GetSection("user").Value;
            string? pwd = config.GetSection("ConnectionStrings").GetSection("pwd").Value;
            string? db = config.GetSection("ConnectionStrings").GetSection("db").Value;
            string? tail = config.GetSection("ConnectionStrings").GetSection("tail").Value;

            InitNewConnection(user, pwd, db, tail);
        }

        public static void InitNewConnection(string? user, string? pwd, string? db, string? tail)
        {
            DestroyConnection();

            bool isConnected = false;
            while (!isConnected)
            {
                //При первом запуске считаем, что пользователь ввёл корректные имя пользователя или пароль
                isConnected = true;

                string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";" + tail;
                using (OracleConnection con = new OracleConnection(conStringUser))
                {
                    try
                    {
                        con.Open();
                        oraCon = con;
                        return;
                    }
                    catch (Exception e)
                    {
                        //Если первый запуск провалился, то GG. Тогда вводим все логин и пароль с клавиатуры.
                        isConnected = false;
                        Console.WriteLine(e.Message);

                        Console.WriteLine("User Id=");
                        string? input = Console.In.ReadLine();
                        user = input != null ? input : "";

                        Console.WriteLine("Password=");
                        input = Console.In.ReadLine();
                        pwd = input != null ? input : "";
                    }
                }
            }
            throw new Exception();
        }

        public static void DestroyConnection()
        {
            if (oraCon != null)
            {
                if (oraCon.State != System.Data.ConnectionState.Open)
                {
                    oraCon.Close();
                }
                oraCon = null;
            }
        }
    }
}
