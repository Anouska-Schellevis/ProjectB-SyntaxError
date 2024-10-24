using Microsoft.Data.Sqlite;

using Dapper;


public static class ReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
<<<<<<< HEAD
    // private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\Users\anouk\Desktop\projectb\ProjectB-SyntaxError\Project\DataSources\project.db");
=======
    //private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\Users\anouk\Desktop\projectb\ProjectB-SyntaxError\Project\DataSources\project.db");
>>>>>>> 1a2c47d5e9d8eb777f4ca348b1db91d3b9379bf2

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

}