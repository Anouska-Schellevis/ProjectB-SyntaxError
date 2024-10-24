using Microsoft.Data.Sqlite;

using Dapper;


public static class SeatsAccess
{
<<<<<<< HEAD
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=../../../../Project/DataSources/project.db");
=======
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    //private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\Users\anouk\Desktop\projectb\ProjectB-SyntaxError\Project\DataSources\project.db");
>>>>>>> origin/main

    private static string Table = "seats";

    public static void Write(SeatsModel seat)
    {
        string sql = $"INSERT INTO {Table} (row_number, column_number, price) VALUES (@RowNumber, @ColumnNumber, @Price)";
        _connection.Execute(sql, seat);
    }

    public static SeatsModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<SeatsModel>(sql, new { Id = id });
    }

    public static void Update(SeatsModel seat)
    {
        string sql = $"UPDATE {Table} SET row_number = @RowNumber, column_number = @ColumnNumber, price = @Price WHERE id = @Id";
        _connection.Execute(sql, seat);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

}