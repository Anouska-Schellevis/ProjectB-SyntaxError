using Microsoft.Data.Sqlite;

using Dapper;


public static class UserAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "user";

    public static void Write(UserModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, firstname, lastname, phone_number, type) VALUES (@Email, @Password, @FirstName, @LastName, @Phone_Number, @Type)";
        _connection.Execute(sql, account);
    }


    public static UserModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Id = id })!;
    }

    public static UserModel GetByEmail(string email)
    {
        try
        {
            _connection.Open(); // Try to open the connection
            string sql = $"SELECT * FROM {Table} WHERE email = @Email";
            return _connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email })!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening database: {ex.Message}");
            return null!; // Return null in case of an error
        }
        finally
        {
            _connection.Close(); // Ensure the connection is closed after the operation
        }
    }

    public static UserModel GetByType(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email AND type = 1";
        return _connection.QueryFirstOrDefault<UserModel>(sql, new { Email = email })!;
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }



}