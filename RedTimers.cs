using Oracle.ManagedDataAccess.Client;
using System;
using System.Runtime.CompilerServices;
using server_red;

namespace server_red
{
    public class RedTimers
    {
        private static IRedRepository? _db;
        private static OracleDataReader? dr;
        private static IConfiguration? config;

        public static async Task TimersAsync(IConfiguration configuration)
        {
            OracleConnection? conNewMatch = DBConnection.InitNewConnection(configuration);
            OracleCommand cmd = conNewMatch!.CreateCommand();

            while (true) // like таймер
            {
                await Task.Delay(2000); // 2 сек
                await Task.Run(() => NewMatch(conNewMatch, cmd));
            }
        }
        public static void NewMatch(OracleConnection conNewMatch, OracleCommand cd)
        {
            cd!.Parameters.Clear();
            cd = conNewMatch!.CreateCommand();
            cd.CommandType = System.Data.CommandType.StoredProcedure;
            cd.Parameters.Add("rfcur", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            cd.CommandText = "PCK_RED.PNEW_MATCH";

            if (conNewMatch != null && conNewMatch.State == System.Data.ConnectionState.Closed)
            {
                conNewMatch.Open();
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
