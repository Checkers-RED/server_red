using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Core;
using Microsoft.Extensions.Configuration;
using static System.Configuration.ConfigurationManager;
using System.Collections.Generic;
using System;

namespace server_red
{
    public class RedRepository : IRedRepository
    {
        private static OracleConnection? con; 
        private OracleCommand cmd;
        private OracleDataReader? dr;
        private IConfiguration config;
        private string? constr;

        public RedRepository(IConfiguration configuration)
        {
            config = configuration;
            constr =  config.GetSection("ConnectionStrings").GetSection("checkers").Value;
            Console.WriteLine(constr);
            con = new OracleConnection(constr);
            cmd = con.CreateCommand();
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
            catch (Exception ex) { }

            List<string> res_list = new List<string>(2);
            object res = cmd.Parameters["res"].Value;
            if (res != null)
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                res_list.Add(item: res.ToString());
                /*
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                if (Convert.ToInt32(res) != 0) // здесь вылетает ошибка
                {
                    object cur_session = cmd.Parameters["pcur_session"].Value;
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    res_list.Add(item: cur_session.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                }
                */
            }
            else
            {
                res_list.Add("0");
            }
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            con.Close();
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            return res_list;
        }
    }
}
