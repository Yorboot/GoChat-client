using System;
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
}