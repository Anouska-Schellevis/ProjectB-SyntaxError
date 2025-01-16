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

    public static SeatsModel GetById(long id)
    {
        string sql = $"SELECT id AS Id, row_number AS RowNumber, column_number AS ColumnNumber, price AS Price FROM {Table} WHERE id = @Id";
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

    public static void ClearAllSeats()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All movies have been deleted and auto-increment has been reset.");
    }

    public static List<SeatsModel> GetAllSeats()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<SeatsModel>(sql).ToList();
    }

    public static void AddSeat(int rowNumber, int columnNumber, int price)
    {
        string sql = "INSERT INTO seats (row_number, column_number, price) VALUES (@RowNumber, @ColumnNumber, @Price);";
        _connection.Execute(sql, new { RowNumber = rowNumber, ColumnNumber = columnNumber, Price = price });

    }

    public static long InsertSeatAndGetId(SeatsModel seat)
    {
        string sql = $"INSERT INTO {Table} (row_number, column_number, price) VALUES (@RowNumber, @ColumnNumber, @Price);";

        _connection.Execute(sql, seat);

        sql = "SELECT last_insert_rowid();";
        long seatId = _connection.QueryFirst<long>(sql);

        return seatId;
    }
}