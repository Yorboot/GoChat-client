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
    public User getUser(MySqlConnection connection, string username, string password)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "Select id,username,password from users where username=@username and password=@password";
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Connection = connection;
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