﻿using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace server_red
{
    public class DBConnection
    {
        public class UserCon
        {
            private String session;
            private OracleConnection connection;
            private OracleCommand cmd;

            private string conStringUser = "";

            public String GetSession()
            {
                return session;
            }

            public OracleConnection GetOracleConnection()
            {
                return CheckOraCon(connection, cmd, conStringUser);
            }

            public OracleCommand GetOracleCommand()
            {
                return cmd;
            }

            public UserCon(String session, IConfiguration config)
            {
                string? user = config.GetSection("ConnectionStrings").GetSection("user").Value;
                string? pwd = config.GetSection("ConnectionStrings").GetSection("pwd").Value;
                string? db = config.GetSection("ConnectionStrings").GetSection("db").Value;
                string? tail = config.GetSection("ConnectionStrings").GetSection("tail").Value;
                string? pool = config.GetSection("ConnectionStrings").GetSection("pool").Value;

                this.conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";" + tail + ";" + pool;

                this.session = session;
                this.connection = SilentConnection(this.conStringUser);
                var cmd = connection!.CreateCommand();
            }
        }

        private static List<UserCon> connections = new List<UserCon>();

        private static UserCon addUserConnection(String session, IConfiguration config)
        {
            UserCon newUser = new UserCon(session, config);
            connections.Add(newUser);
            return newUser;
        }

        public static UserCon getUserConnection(String session, IConfiguration config)
        {
            bool userExist = connections.Exists(item => item.GetSession().Equals(session));

            if (!userExist)
            {
                UserCon? user = addUserConnection(session, config);
                Console.WriteLine("New user: " + user.GetSession());
                return user;
            }

            UserCon? user = connections.Find(item => item.GetSession().Equals(session));
            Console.WriteLine("Got user: " + user.GetSession());
            return user;
        }


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

        //Чмолиморфизм на уровне параметров
        public static OracleConnection SilentConnection(string conStringUser)
        {
            while (true)
            {
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

        private static OracleConnection CheckOraCon(OracleConnection oraCon, OracleCommand oraCmd, string conStringUser)
        {
            try
            {
                if (oraCon != null && oraCon.State != System.Data.ConnectionState.Open)
                {
                    oraCon.Open();
                }
                oraCmd.ExecuteReader();

                return oraCon;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return SilentConnection(conStringUser);
            }
        }
    }
}
