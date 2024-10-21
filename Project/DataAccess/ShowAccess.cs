using Microsoft.Data.Sqlite;

using Dapper;


public static class ShowAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "show";

    public static void Write(ShowModel show)
    {
        string sql = $"INSERT INTO {Table} (theatre_id, movie_id, reservation_id, start_time) VALUES (@TheatreId, @MovieId, @ReservationId, @StartTime)";
        _connection.Execute(sql, show);
    }

    public static ShowModel GetByID(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ShowModel>(sql, new { Id = id });
    }

    // wanna make this into a get by day but I need to change the table for that
    public static void Update(ShowModel show)
    {
        string sql = $"UPDATE {Table} SET theater_id = @TheaterId, movie_id = @MovieId, start_time = @StartTime WHERE id = @Id";
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