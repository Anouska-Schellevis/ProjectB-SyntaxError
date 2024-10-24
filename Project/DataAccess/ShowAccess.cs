using Microsoft.Data.Sqlite;

using Dapper;


public static class ShowAccess
{
<<<<<<< HEAD
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=../../../../Project/DataSources/project.db");
=======
<<<<<<< HEAD
    private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\\Users\\genil\\OneDrive\\Bureaublad\\OODP\\ProjectB\\ProjectB-SyntaxError\\Project\\DataSources\\project.db");
>>>>>>> origin/main

=======
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    //private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\Users\anouk\Desktop\projectb\ProjectB-SyntaxError\Project\DataSources\project.db");
>>>>>>> 1a2c47d5e9d8eb777f4ca348b1db91d3b9379bf2
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
}