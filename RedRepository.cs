using Oracle.ManagedDataAccess.Client;

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

        public List<string> SignIn(string username, string password)
        {

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("plogin", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = username;
            cmd.Parameters.Add("ppass", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = password;
            cmd.Parameters.Add("pcur_session", OracleDbType.Varchar2, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add("res", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            cmd.CommandText = "PCK_RED.PAUTHORIZATION";

            if (con != null && con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                cmd.ExecuteReader();
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            List<string> res_list = new List<string>(2);
            object res = cmd.Parameters["res"].Value;
            if (res != null)
            {
                res_list.Add(item: res.ToString()!);
                /*
                if (Convert.ToInt32(res) != 0) // здесь вылетает ошибка
                {
                    object cur_session = cmd.Parameters["pcur_session"].Value;
                    res_list.Add(item: cur_session.ToString());
                }
                */
            }
            else
            {
                res_list.Add("0");
            }
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            return res_list;
        }
    }
}
