using Microsoft.Data.Sqlite;

using Dapper;


public static class VoucherAccess
{

    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "voucher";

    public static void Write(VoucherModel voucher)
    {
        string sql = $"INSERT INTO {Table} (code, description, amount, type, expiration_date, user_id) VALUES (@Code, @Description, @Amount, @Type, @ExpirationDate, @UserId)";
        _connection.Execute(sql, voucher);
    }

    public static void Update(VoucherModel voucher)
    {
        string sql = $"UPDATE {Table} SET code = @Code, description = @Description, amount = @Amount, type = @Type, expiration_date = @ExpirationDate, user_id = @UserId WHERE id = @Id";
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
        string sql = $"SELECT id AS Id, code AS Code, description AS Description, amount AS Amount, type AS Type, expiration_date AS ExpirationDate, user_id AS UserId FROM {Table}";
        return _connection.Query<VoucherModel>(sql).ToList();
    }

    public static List<VoucherModel> GetVouchersByUserId(UserModel user)
    {
        string sql = $"SELECT id AS Id, code AS Code, description AS Description, amount AS Amount, type AS Type, expiration_date AS ExpirationDate, user_id AS UserId FROM {Table} WHERE user_id = @Id";
        return _connection.Query<VoucherModel>(sql, user).ToList();
    }

    public static void ClearAllVouchers()
    {
        string deleteSql = $"DELETE FROM {Table};";
        _connection.Execute(deleteSql);

        string resetSql = $"UPDATE sqlite_sequence SET seq = 0 WHERE name = @TableName;";
        _connection.Execute(resetSql, new { TableName = Table });

        Console.WriteLine("All vouchers have been deleted and auto-increment has been reset.");
    }
}