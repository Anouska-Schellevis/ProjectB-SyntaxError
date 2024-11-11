using Microsoft.Data.Sqlite;

using Dapper;


public static class SeatsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "seats";

    public static void Write(SeatsModel seat)
    {
        string sql = $"INSERT INTO {Table} (row_number, column_number, price) VALUES (@RowNumber, @ColumnNumber, @Price)";
        _connection.Execute(sql, seat);
    }

    public static SeatsModel GetById(int id)
    {
        string sql = $"SELECT id, row_number AS RowNumber, column_number AS ColumnNumber, price AS Price FROM {Table} WHERE id = @Id";
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