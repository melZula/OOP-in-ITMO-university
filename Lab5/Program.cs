using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Data
    {
        SQLiteConnection Conn;
        public bool DoCommand(string sql, bool writeOnly)
        {
            using(Conn = new SQLiteConnection("Data Source=" + @"..\..\database.db3" + ";Version=3;"))
            {
                Conn.Open();
                SQLiteCommand command = new SQLiteCommand(sql, Conn);
                if (!writeOnly)
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read()) Console.WriteLine(reader["name"]);
                }
                Conn.Close();
            }
            return false;
        }
        public void test()
        {
            string sql = "SELECT * FROM Shops";
            DoCommand(sql, false);
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Data datab = new Data();
            datab.test();
            Console.ReadKey();
        }
    }
}
