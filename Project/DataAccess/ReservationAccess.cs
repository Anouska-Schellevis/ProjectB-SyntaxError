using Microsoft.Data.Sqlite;

using Dapper;


public static class ReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    
    private static string Table = "reservation";

    public static void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (bar, seats_id, user_id, show_id) VALUES (@Bar, @SeatsId, @UserId, @MovieId)";
        _connection.Execute(sql, reservation);
    }

    public static ReservationModel GetById(int id)
    {
        string sql = $"SELECT id, bar AS Bar, seats_id AS SeatsID, user_id AS UserId, show_id AS MovieId FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public static List<ReservationModel> GetBarReservations()
    {
        string sql = $"SELECT id, bar, seats_id AS SeatsID, user_id AS UserId, show_id AS MovieId FROM {Table} WHERE bar = 1"; // 1 means the bar reservation is true
        return _connection.Query<ReservationModel>(sql).ToList();
    }

    public static void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET bar = @Bar, seats_id = @SeatsId, user_id = @UserId, show_id = @MovieId WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

}