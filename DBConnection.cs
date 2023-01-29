﻿using Oracle.ManagedDataAccess.Client;


namespace server_red
{
    public class DBConnection
    {
        private static OracleConnection? oraCon;
        private static OracleCommand? oraCmd;

        private static void SetOraCon(OracleConnection oracleConnection)
        {
            //Эта строка съела у меня полтора часа моего времени
            oraCon = (OracleConnection?)oracleConnection.Clone();
        }

        public static OracleConnection? getOraCon(IConfiguration config)
        {
            try
            {
                oraCmd = oraCon!.CreateCommand();
                oraCmd.CommandType = System.Data.CommandType.Text;
                oraCmd.CommandText = "Update test_table set dt = sysdate where idd = 13";
                if (oraCon != null && oraCon.State != System.Data.ConnectionState.Open)
                {
                    oraCon.Open();
                }
                oraCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                StartupInitNewConnection(config);
            }
            return oraCon;
        }


        public static void StartupInitNewConnection(IConfiguration config)
        {
            string? user = config.GetSection("ConnectionStrings").GetSection("user").Value;
            string? pwd = config.GetSection("ConnectionStrings").GetSection("pwd").Value;
            string? db = config.GetSection("ConnectionStrings").GetSection("db").Value;
            string? tail = config.GetSection("ConnectionStrings").GetSection("tail").Value;
            string? pool = config.GetSection("ConnectionStrings").GetSection("pool").Value;

            InitNewConnection(user, pwd, db, tail, pool);
        }

        public static void InitNewConnection(string? user, string? pwd, string? db, string? tail, string? pool)
        {
            DestroyConnection();

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
                        SetOraCon(con);
                        con.Close();
                        return;
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
