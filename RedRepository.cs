using Oracle.ManagedDataAccess.Client;
using server_red.Models;

namespace server_red
{
    public class RedRepository : IRedRepository
    {
        private static OracleConnection? con;
        private OracleCommand cmd;
        private OracleDataReader? dr;
        private IConfiguration config;

        public RedRepository(IConfiguration configuration)
        {
            con = DBConnection.getOraCon();
            cmd = con.CreateCommand();
            config = configuration;
        }

        public int AcceptMatch(string cur_session, string org_nick)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("org_nick", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = org_nick;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PACCEPT_FRIEND_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public int AddFriend(string cur_session, int f_id)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PADD_FRIEND";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public string AnsQues(string token, string answer)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("ptoken", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = token;
            cmd.Parameters.Add("panswer", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = answer;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PQUESTION";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            string res;
            if (dr.HasRows)
            {
                dr.Read();
                res = dr.GetValue(0).ToString()!;
            }
            else
            {
                res = "";
            }
            return res!;
        }

        public string ChangePass(string token, string newPass)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("ptoken", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = token;
            cmd.Parameters.Add("pnew_pass", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = newPass;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PCHANGE_PASS";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            string res;
            if (dr.HasRows)
            {
                dr.Read();
                res = dr.GetValue(0).ToString()!;
            }
            else
            {
                res = "";
            }
            return res!;
        }

        public string CreateAccount(string username, string password, string question, string answer)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("ppass", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = password;
            cmd.Parameters.Add("pquestion", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = question;
            cmd.Parameters.Add("panswer", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = answer;
            //cmd.Parameters.Add("pcur_session", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            //cmd.Parameters.Add("res", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PCREATE_ACCOUNT";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            string res;
            if (dr.HasRows)
            {
                dr.Read();
                res = dr.GetValue(0).ToString()!;
            }
            else
            {
                res = "none";
            }
            return res!;
        }

        public int DeleteFriend(string cur_session, int f_id)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PDELETE_FRIEND";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public int EndMatch(string cur_session, string color_win)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pcolor_win", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = color_win;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pend_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public string GetActiveColor(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PGET_ACTIVE_COLOR";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            string res;
            if (dr.HasRows)
            {
                dr.Read();
                res = dr.GetValue(0).ToString()!;
            }
            else
            {
                res = "none";
            }
            return res!;
        }

        public int GetBeatFlag(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PGET_BEAT_FLAG";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = -1;
            }
            return res!;
        }

        public List<User> GetFriendlist(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_FRIENDLIST";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            List<User> res = new List<User>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr.GetValue(0).ToString() != "")
                    {
                        User u = new User();
                        u.uid = Convert.ToInt32(dr.GetValue(0).ToString());
                        u.nick = dr.GetValue(1).ToString()!;
                        u.photo = dr.GetValue(2).ToString()!;
                        res.Add(u);
                    }
                }
            }
            else
            {
                //res = "none";
            }
            return res!;
        }

        public List<Notif> GetNotiflist(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_NOTIF";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            List<Notif> res = new List<Notif>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr.GetValue(0).ToString() != "")
                    {
                        Notif n = new Notif();
                        n.type = Convert.ToInt32(dr.GetValue(1).ToString());
                        n.actEl1 = dr.GetValue(2).ToString()!;
                        n.actEl2 = dr.GetValue(3).ToString()!;
                        res.Add(n);
                    }
                }
            }
            else
            {
                //res = "none";
            }
            return res!;
        }

        public UserScore GetUserScore(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_UINFO";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            UserScore u = new UserScore();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr.GetValue(0).ToString() != "")
                {
                    u.nick = dr.GetValue(0).ToString()!;
                    u.photo = dr.GetValue(1).ToString()!;
                    if (dr.GetValue(2).ToString().Length == 0)
                    {
                        return u;
                    }
                    u.score = Convert.ToInt32(dr.GetValue(2).ToString());
                    return u;
                }
            }
            else
            {
                //res = "none";
            }
            return u;
        }

        public int GiveUp(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pgive_up";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public int InviteFriend(string cur_session, int f_id, int move_time, int rules_id)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("pmtime", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = move_time;
            cmd.Parameters.Add("prulesid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = rules_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PINVITE_FRIEND_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public int RejectMatch(string cur_session, string org_nick)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("org_nick", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = org_nick;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.preject_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public ReqNick ReqNick(string username)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PNICK";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            //List <string> res = new List<string>();
            ReqNick r = new ReqNick();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr.GetValue(0).ToString() != "")
                {
                    r.token = dr.GetValue(0).ToString()!;
                    //res.Add(dr.GetValue(0).ToString()!);
                    r.question = dr.GetValue(1).ToString()!;
                    //res.Add(dr.GetValue(1).ToString()!);
                }
            }
            else
            {
                //res = "none";
            }
            return r;// res!;
        }

        public int RevokeMatch(string cur_session, int f_id)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.prevoke_friend_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public List<Checker> SessionCheckersBlack(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSESSION_CHECKERS_BLACK";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            List<Checker> res = new List<Checker>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        if (dr.GetValue(0).ToString() != "")
                        {
                            Checker c = new Checker();
                            c.color = dr.GetValue(0).ToString();
                            c.horiz = Convert.ToInt32(dr.GetValue(1).ToString());
                            c.vertic = Convert.ToInt32(dr.GetValue(2).ToString());
                            if (Convert.ToInt32(dr.GetValue(3).ToString()) == 1)
                            {
                                c.isQueen = true;
                            }
                            else
                            {
                                c.isQueen = false;
                            }
                            if (Convert.ToInt32(dr.GetValue(4).ToString()) == 1)
                            {
                                c.isBeaten = true;
                            }
                            else
                            {
                                c.isBeaten = false;
                            }
                            res.Add(c);
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            else
            {
                //res = "none";
            }
            return res!;
        }

        public List<Checker> SessionCheckersWhite(string cur_session)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSESSION_CHECKERS_WHITE";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            List<Checker> res = new List<Checker>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        if (dr.GetValue(0).ToString() != "")
                        {
                            Checker c = new Checker();
                            c.color = dr.GetValue(0).ToString();
                            c.horiz = Convert.ToInt32(dr.GetValue(1).ToString());
                            c.vertic = Convert.ToInt32(dr.GetValue(2).ToString());
                            if (Convert.ToInt32(dr.GetValue(3).ToString()) == 1)
                            {
                                c.isQueen = true;
                            }
                            else
                            {
                                c.isQueen = false;
                            }
                            if (Convert.ToInt32(dr.GetValue(4).ToString()) == 1)
                            {
                                c.isBeaten = true;
                            }
                            else
                            {
                                c.isBeaten = false;
                            }
                            res.Add(c);
                        }
                    }
                    catch(Exception ex) { }
                }
            }
            else
            {
                //res = "none";
            }
            return res!;
        }

        public int SetActiveColor(string cur_session, string color)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("color", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = color;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSET_ACTIVE_COLOR";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public int SetBeatFlag(string cur_session, int beat_flag)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pbeatf", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = beat_flag;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSET_BEAT_FLAG";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public string SignIn(string username, string password)
        {

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("ppass", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = password;
            //cmd.Parameters.Add("pcur_session", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            //cmd.Parameters.Add("res", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PAUTHORIZATION";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                // cmd.ExecuteReader();
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            //List<string> res_list = new List<string>(1);
            string res;
            if (dr.HasRows)
            {
                dr.Read();
                //res_list.Add(dr.GetValue(0).ToString()!);
                res = dr.GetValue(0).ToString()!;
            }
            else
            {
                //res_list.Add("0");
                res = "none";
            }

            //List<string> res_list = new List<string>(2);
            //string res = cmd.Parameters["res"].Value.ToString()!;
            //if (res != null)
            //{
            //res_list.Add(item: res.ToString()!);
            //    res_list.Add(res);
            //    if (Convert.ToInt32(res) != 0)
            //     {
            //object cur_session = cmd.Parameters["pcur_session"].Value;
            //        string cur_session = cmd.Parameters["pcur_session"].Value.ToString()!;
            //res_list.Add(item: cur_session.ToString()!);
            //         res_list.Add(cur_session);
            //      }
            /*
            if (Convert.ToInt32(res) != 0) // здесь вылетает ошибка
            {
                object cur_session = cmd.Parameters["pcur_session"].Value;
                res_list.Add(item: cur_session.ToString());
            }
            */
            //    }
            //    else
            //   {
            //        res_list.Add("0");
            //   }
            //if (con.State == System.Data.ConnectionState.Open)
            //{
            //    con.Close();
            //}

            //return res_list;
            return res!;
        }

        public int UpdateCheckersField(string cur_session, string color, int fx, int fy, int king, int beaten, bool delete_old)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pcolor", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = color;
            cmd.Parameters.Add("pfx", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = fx;
            cmd.Parameters.Add("pfy", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = fy;
            cmd.Parameters.Add("pking", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = king;
            cmd.Parameters.Add("pbeaten", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = beaten;
            cmd.Parameters.Add("pdelete_old", OracleDbType.Boolean, System.Data.ParameterDirection.Input).Value = delete_old;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pupdate_checkers_field";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;
        }

        public User UserSearch(string username)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PUSER_SEARCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            User u = new User();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr.GetValue(0).ToString() != "")
                {
                    u.uid = Convert.ToInt32(dr.GetValue(0).ToString()!);
                    u.nick = dr.GetValue(1).ToString()!;
                    u.photo = dr.GetValue(2).ToString()!;
                }
            }
            return u;
        }
    }
}
