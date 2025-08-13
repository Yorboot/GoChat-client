using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GoChat.Database;

public class Database
{
    MySql.Data.MySqlClient.MySqlConnection conn;
    private string connectionString;
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

    public List<string> getMessages(MySqlConnection connection)
    {
        List<string> messages = new List<string>();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select content from messages";
        cmd.Connection = connection;
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (reader["content"].ToString() != string.Empty || reader["date"].ToString() != null)
                {
                    messages.Add(reader["content"].ToString());
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