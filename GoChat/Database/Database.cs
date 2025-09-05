using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GoChat.Database;

public static class Database
{
    private static MySql.Data.MySqlClient.MySqlConnection _conn;
    private static string connectionString;
    public static int LastMessageId;

    static Database()
    {
         _conn = InitDb();
    }
    private static MySql.Data.MySqlClient.MySqlConnection InitDb()
    {
        connectionString ="Server=127.0.0.1;Port=3306;Database=gochat;User ID=root;Password=;";
        try
        {
            _conn = new MySql.Data.MySqlClient.MySqlConnection();
            _conn.ConnectionString = connectionString;
            _conn.Open();
            Console.WriteLine("Connection established");
            return _conn;
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.WriteLine("Error sending request: +" + ex.Message);
        }
        return null;
    }
    

    public static List<Message> getMessages()
    {
        List<Message> messages = new();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select id,user_id,content from messages";
        cmd.Connection = _conn;
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
    public static User getUser(string username, string password)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select id,username,password from users where username=@username and password=@password";
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Connection = _conn;
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                if (reader["username"].ToString() != string.Empty && reader["username"].ToString() != null &&
                    reader["password"].ToString() != string.Empty && reader["password"].ToString() != null &&
                    reader["id"] != DBNull.Value)
                {
                    User u = new User()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString()
                    };
                    return u;
                }
            }
        }

        return new User();
    }
}

public struct Message
{
    public int id { get; set; }

    public int userId { get; set; }
    public string content { get; set; }
}

public struct User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}