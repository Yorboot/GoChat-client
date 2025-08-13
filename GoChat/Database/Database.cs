using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GoChat.Database;

public class Database
{
    MySql.Data.MySqlClient.MySqlConnection conn;
    private string connectionString;
    public static int lastMessageId;
    public MySql.Data.MySqlClient.MySqlConnection InitDb()
    {
        connectionString ="Server=127.0.0.1;Port=3306;Database=gochat;User ID=root;Password=;";
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            Console.WriteLine("Connection established");
            return conn;
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.WriteLine("Error sending request: +" + ex.Message);
        }
        return null;
    }

    public Dictionary<int,string> getMessages(MySqlConnection connection)
    {
        Dictionary<int,string> messages = new();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select id,content from messages WHERE id > @lastId ORDER BY id";
        cmd.Connection = connection;
        cmd.Parameters.Add("@lastId", MySqlDbType.Int32);
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (reader["content"].ToString() != string.Empty && reader["content"].ToString() != null && reader["id"] != DBNull.Value)
                {
                    int id = Convert.ToInt32(reader["id"]);
                    messages.Add(id,reader["content"].ToString());
                }
                
            };
            if (messages.Count > 0)
            {
                return messages;
            }
            else
            {
                return null;
            }
            
        }
    }
}