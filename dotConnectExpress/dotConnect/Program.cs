using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data.Oracle;

namespace dotConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Server=oracle_db;User Id=web;Password=oracle";

            try
            {
                con.Open();
                Console.WriteLine(con.ServerVersion);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
