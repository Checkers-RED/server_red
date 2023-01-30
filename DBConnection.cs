using Oracle.ManagedDataAccess.Client;


namespace server_red
{
    public class DBConnection
    {
        private static OracleConnection DupeConnection(OracleConnection oracleConnection)
        {
            return (OracleConnection?)oracleConnection.Clone();
        }

        public static OracleConnection InitNewConnection(IConfiguration config)
        {
            string? user = config.GetSection("ConnectionStrings").GetSection("user").Value;
            string? pwd = config.GetSection("ConnectionStrings").GetSection("pwd").Value;
            string? db = config.GetSection("ConnectionStrings").GetSection("db").Value;
            string? tail = config.GetSection("ConnectionStrings").GetSection("tail").Value;
            string? pool = config.GetSection("ConnectionStrings").GetSection("pool").Value;

            bool isConnected = false;
            while (!isConnected)
            {
                //При первом запуске считаем, что админ имеет на руках корректные имя пользователя или пароль.
                isConnected = true;

                string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";" + tail + ";" + pool;
                using (OracleConnection con = new OracleConnection(conStringUser))
                {
                    try
                    {
                        con.Open();
                        con.Close();
                        return DupeConnection(con);
                    }
                    catch (Exception ex)
                    {
                        //Если первый запуск провалился, то GG. Тогда вводим логин и пароль с клавиатуры.
                        isConnected = false;
                        Console.WriteLine(ex.Message);

                        Console.WriteLine("User Id=");
                        string? input = Console.In.ReadLine();
                        user = input != null ? input : "";

                        Console.WriteLine("Password=");
                        input = Console.In.ReadLine();
                        pwd = input != null ? input : "";
                    }
                }
            }
            throw new Exception("What? How did you get there?");
        }


        public static OracleConnection SilentConnection(IConfiguration config)
        {
            string? user = config.GetSection("ConnectionStrings").GetSection("user").Value;
            string? pwd = config.GetSection("ConnectionStrings").GetSection("pwd").Value;
            string? db = config.GetSection("ConnectionStrings").GetSection("db").Value;
            string? tail = config.GetSection("ConnectionStrings").GetSection("tail").Value;
            string? pool = config.GetSection("ConnectionStrings").GetSection("pool").Value;


            while (true)
            {
                string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";" + tail + ";" + pool;
                using (OracleConnection con = new OracleConnection(conStringUser))
                {
                    try
                    {
                        con.Open();
                        con.Close();
                        return DupeConnection(con);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connection retry: " + DateTime.Now.ToString());
                    }
                }
            }
            throw new Exception("What? How did you get there?");
        }

        /*
        public static OracleConnection? getOraCon(IConfiguration config)
        {
            try
            {
                oraCmd = oraCon!.CreateCommand();
                oraCmd.CommandType = System.Data.CommandType.Text;
                oraCmd.CommandText = "Update test_table set dt = sysdate where idd = 13";
                if (oraCon != null && oraCon.State == System.Data.ConnectionState.Closed)
                {
                    oraCon.Open();
                }
                oraCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------New Session Start---------------");
                DestroyConnection();
                Console.WriteLine(ex.Message);
                SetOraCon(RestartConnection(config));
                Console.WriteLine("----------------New Session End----------------");
            }
            return oraCon;
        }
        */
    }
}
