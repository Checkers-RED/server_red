using Oracle.ManagedDataAccess.Client;
using server_red.Models;
using System.Configuration;

namespace server_red
{
    public class RedRepository : IRedRepository
    {
        private IConfiguration configuration;

        public RedRepository(IConfiguration configuration)
        {
            //con = DBConnection.SilentConnection(configuration);
            //cmd = con!.CreateCommand();
            this.configuration = configuration;
        }

        public int AcceptMatch(string cur_session, string org_nick)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("org_nick", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = org_nick;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PACCEPT_FRIEND_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr!.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0));
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public int AddFriend(string cur_session, int f_id)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PADD_FRIEND";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public string AnsQues(string token, string answer)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("ptoken", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = token;
            cmd.Parameters.Add("panswer", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = answer;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PQUESTION";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string res = "";
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = dr.GetValue(0).ToString()!;
                }
                else
                {
                    res = "";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public string ChangePass(string token, string newPass)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("ptoken", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = token;
            cmd.Parameters.Add("pnew_pass", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = newPass;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PCHANGE_PASS";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string res = "";
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = dr.GetValue(0).ToString()!;
                }
                else
                {
                    res = "";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public string CreateAccount(string username, string password, string question, string answer)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
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
            string res = "none";
            try
            {
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    res = dr.GetValue(0).ToString()!;
                }
                else
                {
                    res = "none";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int DeleteFriend(string cur_session, int f_id)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PDELETE_FRIEND";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int EndMatch(string cur_session, string color_win)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pcolor_win", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = color_win;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pend_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public string GetActiveColor(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_ACTIVE_COLOR";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            string res = "none";
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = dr.GetValue(0).ToString()!;
                }
                else
                {
                    res = "none";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public ActMoveTimeColor GetActMoveTimeColor(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pget_actcolor_dttime";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            ActMoveTimeColor m = new ActMoveTimeColor();
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr.GetValue(0).ToString() != "")
                    {
                        m.color = dr.GetValue(0).ToString();
                        m.dttime = dr.GetBoolean(1).ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return m;
        }

        public int GetBeatFlag(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_BEAT_FLAG";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = -1;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = -1;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public List<User> GetFriendlist(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_FRIENDLIST";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            List<User> res = new List<User>();
            try
            {
                var dr = cmd.ExecuteReader();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public GameInfo GetGameInfo(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_GAME_INFO";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            GameInfo g = new GameInfo();
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr.GetValue(0).ToString() != "")
                    {
                        g.white_nick = dr.GetValue(0).ToString()!;
                        g.white_score = Convert.ToInt32(dr.GetValue(1).ToString()!);
                        g.black_nick = dr.GetValue(2).ToString()!;
                        g.black_score = Convert.ToInt32(dr.GetValue(3).ToString()!);
                        g.move_time = Convert.ToInt32(dr.GetValue(4).ToString()!);
                        g.rules_id = Convert.ToInt32(dr.GetValue(5).ToString()!);
                        //return g;
                    }
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            /*else
            {
                //res = "none";
            }*/
            return g;
        }

        public int GetInvitedFriendId(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_INVITED_FID";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr!.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0)!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public List<Move> GetMovesList(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_MOVESLIST";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            List<Move> res = new List<Move>();
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr.GetValue(0).ToString() != "")
                        {
                            Move m = new Move();
                            m.num = Convert.ToInt32(dr.GetValue(0).ToString());
                            m.note = dr.GetValue(1).ToString()!;
                            res.Add(m);
                        }
                    }
                }
                else
                {
                    //res = "none";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public List<Notif> GetNotiflist(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_NOTIF";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            List<Notif> res = new List<Notif>();
            try
            {
                var dr = cmd.ExecuteReader();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public Opponent GetOpponentInfo(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_OPPONENT_INFO";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            Opponent o = new Opponent();
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr.GetValue(0).ToString() != "")
                    {
                        o.uid = Convert.ToInt32(dr.GetValue(0).ToString());
                        o.nick = dr.GetValue(1).ToString()!;
                        o.photo = dr.GetValue(2).ToString()!;
                        if (dr.GetValue(3).ToString()!.Length == 0)
                        {
                            return o;
                        }
                        o.score = Convert.ToInt32(dr.GetValue(3).ToString());
                        return o;
                    }
                }
                else
                {
                    //res = "none";
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return o;
        }

        public UserScore GetUserScore(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PGET_UINFO";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            UserScore u = new UserScore();
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr.GetValue(0).ToString() != "")
                    {
                        u.nick = dr.GetValue(0).ToString()!;
                        u.photo = dr.GetValue(1).ToString()!;
                        if (dr.GetValue(2).ToString()!.Length == 0)
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return u;
        }

        public int GiveUp(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.pgive_up";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public int InRankedMatch(string cur_session, int rules)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("prules", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = rules;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PIN_RANKED_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr!.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public void InsertMovesList(string cur_session, string note)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pnote", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = note;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PINSERT_MOVES_LIST";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                //cmd.ExecuteReader();
                var dr = cmd.ExecuteReader();
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            /*int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = -1;
            }
            return res!;*/
        }

        public int InviteFriend(string cur_session, int f_id, int move_time, int rules_id)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
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
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int IsInMatch(string cur_session)
        {
            Console.WriteLine("---------->" + cur_session);
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PIS_IN_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int IsInRankedMatch(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PIS_IN_RANKED_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int IsNotInRankedMatch(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PISNOT_IN_RANKED_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public void NewMatch()
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.POUT_RNEW_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                var dr = cmd.ExecuteReader();
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            /*int res;
            if (dr.HasRows)
            {
                dr.Read();
                res = Convert.ToInt32(dr.GetValue(0).ToString()!);
            }
            else
            {
                res = 0;
            }
            return res!;*/
        }

        public int OutRankedMatch(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.POUT_RANKED_MATCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public int RejectMatch(string cur_session, string org_nick)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("org_nick", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = org_nick;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.preject_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }

        public ReqNick ReqNick(string username)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PNICK";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            ReqNick r = new ReqNick();
            try
            {
                var dr = cmd.ExecuteReader();
                //List <string> res = new List<string>();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return r;// res!;
        }

        public int RevokeMatch(string cur_session, int f_id)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pfid", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = f_id;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.prevoke_friend_match";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public List<Checker> SessionCheckersBlack(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSESSION_CHECKERS_BLACK";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            List<Checker> res = new List<Checker>();
            try
            {
                var dr = cmd.ExecuteReader();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public List<Checker> SessionCheckersWhite(string cur_session)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSESSION_CHECKERS_WHITE";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            List<Checker> res = new List<Checker>();
            try
            {
                var dr = cmd.ExecuteReader();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public int SetActiveColor(string cur_session, string color)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("color", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = color;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSET_ACTIVE_COLOR";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public int SetBeatFlag(string cur_session, int beat_flag)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("pcur_s", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = cur_session;
            cmd.Parameters.Add("pbeatf", OracleDbType.Int32, System.Data.ParameterDirection.Input).Value = beat_flag;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PSET_BEAT_FLAG";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res;
        }

        public string SignIn(string username, string password)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
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
            string res = "none";
            try
            {
                // cmd.ExecuteReader();
                var dr = cmd.ExecuteReader();
                //List<string> res_list = new List<string>(1);
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public int UpdateCheckersField(string cur_session, string color, int fx, int fy, int king, int beaten, bool delete_old)
        {
            var userCon = DBConnection.getUserConnection(cur_session, configuration);
            var con = userCon.GetOracleConnection(); var cmd = userCon.GetOracleCommand();
            cmd.Parameters.Clear();
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
            int res = 0;
            try
            {
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    res = Convert.ToInt32(dr.GetValue(0).ToString()!);
                }
                else
                {
                    res = 0;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return res!;
        }

        public User UserSearch(string username)
        {
            var con = DBConnection.SilentConnection(configuration);
            var cmd = con!.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PUSER_SEARCH";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            User u = new User();
            try
            {
                var dr = cmd.ExecuteReader();
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
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return u;
        }
    }
}
