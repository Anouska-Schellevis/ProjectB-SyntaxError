using Microsoft.Data.Sqlite;

using Dapper;


public static class VoucherAccess
{

    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "voucher";

    public static void Write(VoucherModel voucher)
    {
        string sql = $"INSERT INTO {Table} (code, description, amount, type, user_id) VALUES (@Code, @Description, @Amount, @Type, @UserId)";
        _connection.Execute(sql, voucher);
    }

    public static VoucherModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<VoucherModel>(sql, new { Id = id });
    }

    public static VoucherModel GetByType(string type)
    {
        string sql = $"SELECT * FROM {Table} WHERE type = @Type";
        return _connection.QueryFirstOrDefault<VoucherModel>(sql, new { Type = type });
    }

    public static List<VoucherModel> GetAllVouchers()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<VoucherModel>(sql).ToList();
    }

    public static List<VoucherModel> GetVouchersByUserId(long userId)
    {
        string sql = $"SELECT id AS Id, code AS Code, description AS Description, amount AS Amount, type AS Type, user_id AS UserId FROM {Table} WHERE user_id = @UserId";
        return _connection.Query<VoucherModel>(sql, new { UserId = userId }).ToList();
    }


    // public static void Update(ShowModel show)
    // {
    //     string sql = $"UPDATE {Table} SET theatre_id = @TheatreId, movie_id = @MovieId, date = @Date WHERE id = @Id";
    //     _connection.Execute(sql, show);
    // }

    // public static void Delete(int id)
    // {
    //     string sql = $"DELETE FROM {Table} WHERE id = @Id";
    //     _connection.Execute(sql, new { Id = id });
    // }
    public static void ClearAllVouchers()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All vouchers have been deleted and auto-increment has been reset.");
    }
}