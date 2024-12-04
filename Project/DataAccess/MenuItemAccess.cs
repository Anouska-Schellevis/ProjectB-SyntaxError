using Microsoft.Data.Sqlite;
using Dapper;

public static class MenuItemAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "menu_items";

    public static void Write(MenuItem item)
    {
        string sql = $"INSERT INTO {Table} (name, price, type) VALUES (@Name, @Price, @Type)";
        _connection.Execute(sql, item);
    }

    public static MenuItem GetByName(string name)
    {
        string sql = $"SELECT * FROM {Table} WHERE name = @Name";
        return _connection.QueryFirstOrDefault<MenuItem>(sql, new { Name = name });
    }

    public static void Update(MenuItem item)
    {
        string sql = $"UPDATE {Table} SET name = @Name, price = @Price, type = @Type WHERE id = @Id";
        _connection.Execute(sql, item);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static MenuItem GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MenuItem>(sql, new { Id = id });
    }

    public static List<MenuItem> GetAllMenuItems()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<MenuItem>(sql).ToList();
    }

    public static void ClearMenuItems()
    {
        string sql = $"DELETE FROM {Table}";
        _connection.Execute(sql);
    }
}