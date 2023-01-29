using Oracle.ManagedDataAccess.Client;
using System;
using System.Runtime.CompilerServices;
using server_red;

namespace server_red
{
    public class RedTimers
    {
        private static IRedRepository? _db;
        private static OracleConnection? con;
        private static OracleCommand? cmd;
        private static OracleDataReader? dr;
        private static IConfiguration? config;

        public static async Task TimersAsync(IConfiguration configuration)
         {
             DBConnection.StartupInitNewConnection(configuration);
             con = DBConnection.getOraCon(configuration);
             cmd = con!.CreateCommand();
             config = configuration;

             while (true) // like таймер
             {
                 await Task.Delay(2000); // 2 сек
                 await Task.Run(() => NewMatch(con, cmd));
             }
         }
         public static void NewMatch(OracleConnection cn, OracleCommand cd)
         {
             cd!.Parameters.Clear();
             cd = con!.CreateCommand();
             cd.CommandType = System.Data.CommandType.StoredProcedure;
             cd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
             cd.CommandText = "PCK_RED.PNEW_MATCH";

             if (cn != null && cn.State != System.Data.ConnectionState.Open)
             {
                 cn.Open();
             }
             try
             {
                 dr = cd.ExecuteReader();
                dr!.Close();
                //Console.WriteLine("NewMatch timer");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            //cn!.Close();
            //dr!.Close();
         }
    }
}
