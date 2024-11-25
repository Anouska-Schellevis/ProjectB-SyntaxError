using Microsoft.Data.Sqlite;

using Dapper;


public static class ReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "reservation";

    public static void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (bar, seats_id, user_id, show_id) VALUES (@Bar, @SeatsId, @UserId, @ShowId)";
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
        string sql = $"UPDATE {Table} SET bar = @Bar, seats_id = @SeatsId, user_id = @UserId, show_id = @ShowId WHERE id = @Id";
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
        // Console.WriteLine("Executing query: " + sql);
        return _connection.Query<long>(sql).AsList();
    }
    // public static List<ReservationModel> GetReservationsByUserId(long userId)
    // {
    //     string sql = $"SELECT * FROM {Table} WHERE user_id = {userId}";
    //     // Console.WriteLine("Executing query: " + sql);
    //     return _connection.Query<ReservationModel>(sql).AsList();
    // }

    public static List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserID, show_id AS ShowId FROM {Table} WHERE user_id = {userId}";
        // Console.WriteLine("Executing query: " + sql);
        return _connection.Query<ReservationModel>(sql).AsList();
    }

    public static void ClearAllReservations()
    {
        string sql = $"DELETE FROM {Table};";
        _connection.Execute(sql);
        Console.WriteLine("All reservations have been deleted.");
    }

    public static void AddReservation(int bar, int showId, int seatsId, int userId)
    {

        string sql = "INSERT INTO reservation (bar, show_id, seats_id, user_id) VALUES (@Bar, @ShowId, @SeatsId, @UserId);";
        _connection.Execute(sql, new { Bar = bar, ShowId = showId, SeatsId = seatsId, UserId = userId });

    }

    public static List<ReservationModel> GetAllReservations()
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserID, show_id AS ShowId FROM {Table}";
        return _connection.Query<ReservationModel>(sql).ToList();
    }


}