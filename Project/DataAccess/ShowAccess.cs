using Microsoft.Data.Sqlite;

using Dapper;


public static class ShowAccess
{

    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "show";

    public static void Write(ShowModel show)
    {
        string sql = $"INSERT INTO {Table} (theatre_id, movie_id, date) VALUES (@TheatreId, @MovieId, @Date)";
        _connection.Execute(sql, show);
    }

    public static ShowModel GetByID(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ShowModel>(sql, new { Id = id });
    }

    public static void Update(ShowModel show)
    {
        string sql = $"UPDATE {Table} SET theatre_id = @TheatreId, movie_id = @MovieId, date = @Date WHERE id = @Id";
        _connection.Execute(sql, show);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static List<ShowModel> GetAllShows()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<ShowModel>(sql).ToList();
    }

    public static List<ShowModel> GetByMovieID(int movieId)
    {
        string sql = $"SELECT * FROM {Table} WHERE movie_id = @MovieId";
        return _connection.Query<ShowModel>(sql, new { MovieId = movieId }).ToList();
    }

    public static void ClearAllShows()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All shows have been deleted and auto-increment has been reset.");
    }

    public static List<ShowModel> AllOrderedByDate()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY date";
        return _connection.Query<ShowModel>(sql).ToList();
    }
}