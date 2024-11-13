using Microsoft.Data.Sqlite;

using Dapper;


public static class ReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "reservation";

    public static void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (bar, seats_id, user_id, movie_id) VALUES (@Bar, @SeatsId, @UserId, @MovieId)";
        _connection.Execute(sql, reservation);
    }

    public static ReservationModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public static void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET bar = @Bar, seats_id = @SeatsId, user_id = @UserId, movie_id = @MovieId WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static List<long> GetReservedSeatsByMovieId(long showId)
    {
        string sql = $"SELECT seats_id FROM {Table} WHERE show_id = {showId}";
        // Console.WriteLine("Executing query: " + sql);
        return _connection.Query<long>(sql).AsList();
    }
}