using Microsoft.Data.Sqlite;
using Dapper;

public static class ReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "reservation";

    public static void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (bar, seats_id, user_id, show_id, snacks) VALUES (@Bar, @SeatsId, @UserId, @ShowId, @Snacks)";
        _connection.Execute(sql, reservation);
    }

    public static ReservationModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public static List<ReservationModel> GetBarReservations()
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserID, show_id AS ShowId FROM {Table} WHERE bar = 1"; // 1 means the bar reservation is true
        return _connection.Query<ReservationModel>(sql).ToList();
    }

    public static void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET bar = @Bar, seats_id = @SeatsId, user_id = @UserId, show_id = @ShowId, snacks = @Snacks WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static List<long> GetReservedSeatsByShowId(long showId)
    {
        string sql = $"SELECT seats_id FROM {Table} WHERE show_id = {showId}";
        return _connection.Query<long>(sql).AsList();
    }

    public static List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserID, show_id AS ShowId, snacks AS Snacks FROM {Table} WHERE user_id = {userId}";
        return _connection.Query<ReservationModel>(sql).AsList();
    }

    public static void ClearAllReservations()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All reservations have been deleted and auto-increment has been reset.");
    }

    public static void AddReservation(int bar, int showId, int seatsId, int userId)
    {
        string sql = "INSERT INTO reservation (bar, show_id, seats_id, user_id, snacks) VALUES (@Bar, @ShowId, @SeatsId, @UserId, @Snacks);";
        _connection.Execute(sql, new { Bar = bar, ShowId = showId, SeatsId = seatsId, UserId = userId, Snacks = "" });
    }

    public static List<ReservationModel> GetAllReservations()
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserID, show_id AS ShowId, snacks AS Snacks FROM {Table}";
        return _connection.Query<ReservationModel>(sql).ToList();
    }
}