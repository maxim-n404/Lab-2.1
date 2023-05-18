using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Lab_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=sqlite.db";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int choose;
            do
            {
                Console.WriteLine("Select an option from the list below");
                Console.WriteLine("0 - Get all tables");
                Console.WriteLine("1 - Add row to the table");
                Console.WriteLine("2 - Close");

                choose = Int32.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 0:
                        {
                            DisplayData(connection, "SELECT * from PLATFORM", 0);
                            DisplayData(connection, "SELECT * from DEVELOPER", 0);
                            DisplayData(connection, "SELECT * from VIDEO", 0);
                            DisplayData(connection, "SELECT * from USER", 0);
                            DisplayData(connection, "SELECT * from ADVERTISING", 0);
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                int id;
                                Console.WriteLine("Write id:");
                                id = Int32.Parse(Console.ReadLine());
                                AddRow(connection, "PLATFORM", new object[] { id, "HD-rezka", "10" });
                                AddRow(connection, "DEVELOPER", new object[] { id, "Eugene Petelchyk", "b63c5b67egrt", "eugenepetelchyk44@ukr.net","2" });
                                AddRow(connection, "VIDEO", new object[] { id, "1+1", "01:52:30", "Eng, UK" });
                                AddRow(connection, "USER", new object[] { id, "Oleg Shalypnyak", "3g6h78B8o32", "olegshalypnyak789@gmail.com" });
                                AddRow(connection, "ADVERTISING", new object[] { id, "KFC", "00:00:60", "Eng, UK" });
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("This id exists");
                            }
                            break;
                        }
                }
            } while (choose != 2);

            connection.Close();

        }

        static void DisplayData(SQLiteConnection connection, string com, int tableName)
        {
            SQLiteCommand command = new SQLiteCommand(com, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("Data from " + reader.GetTableName(tableName).ToString() + ":");

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string a = reader.GetName(i);
                Console.Write("{0, -27}", a);
            }

            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string a = reader[i].ToString();
                    Console.Write("{0, -27}", a);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        static void AddRow(SQLiteConnection connection, string tableName, object[] values)
        {
            string query = "INSERT INTO " + tableName + " VALUES (";
            for (int i = 0; i < values.Length; i++)
            {
                query += "@value" + i + ", ";
            }
            query = query.Remove(query.Length - 2) + ")";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            for (int i = 0; i < values.Length; i++)
            {
                command.Parameters.AddWithValue("@value" + i, values[i]);
            }

            command.ExecuteNonQuery();
        }
    }
}