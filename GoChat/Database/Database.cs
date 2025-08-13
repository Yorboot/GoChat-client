using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GoChat.Database;

public class Database
{
    MySql.Data.MySqlClient.MySqlConnection conn;
    private string connectionString;
    public int LastMessageId = 0;
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
    

    public List<Message> getMessages(MySqlConnection connection)
    {
        List<Message> messages = new();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select id,user_id,content from messages";
        cmd.Connection = connection;
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (reader["content"].ToString() != string.Empty && reader["content"].ToString() != null &&
                    reader["id"] != DBNull.Value)
                {
                    int id = Convert.ToInt32(reader["id"]);
                    Message m = new Message()
                    {
                        id = id,
                        userId = Convert.ToInt32(reader["user_id"]),
                        content = reader["content"].ToString(),
                    };
                    messages.Add(m);
                }
            }
        }

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

public struct Message
{
    public int id { get; set; }

    public int userId { get; set; }
    public string content { get; set; }
}