using Microsoft.Data.Sqlite;

using Dapper;


public static class MoviesAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "movie";

    public static void Write(MoviesModel movie)
    {
        string sql = $"INSERT INTO {Table} (time_in_minutes, genre, description, title, director, release_date) VALUES (@TimeInMinutes, @Genre, @Description, @Title, @Director, @ReleaseDate)";
        _connection.Execute(sql, movie);
    }

    public static MoviesModel GetByTitle(string title)
    {
        string sql = $"SELECT * FROM {Table} WHERE title = @Title";
        return _connection.QueryFirstOrDefault<MoviesModel>(sql, new { Title = title });
    }

    public static void Update(MoviesModel movie)
    {
        string sql = $"UPDATE {Table} SET time_in_minutes = @TimeInMinutes, genre = @Genre, description = @Description, title = @Title, director = @Director, release_date = @ReleaseDate WHERE id = @Id";
        _connection.Execute(sql, movie);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static MoviesModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MoviesModel>(sql, new { Id = id });
    }
    public static List<MoviesModel> GetAllMovies()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<MoviesModel>(sql).ToList();
    }
    public static MoviesModel GetByLongId(long id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MoviesModel>(sql, new { Id = id });
    }

    public static void ClearAllMovies()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All movies have been deleted and auto-increment has been reset.");
    }
}