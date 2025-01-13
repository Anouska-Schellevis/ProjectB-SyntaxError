using Microsoft.Data.Sqlite;
using Dapper;

public static class LocationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "location";

    public static void Write(LocationModel location)
    {
        string sql = $"INSERT INTO {Table} (city, address, postal_code) VALUES (@City, @Address, @PostalCode)";
        _connection.Execute(sql, location);
    }

    public static List<LocationModel> GetAllLocations()
    {
        string sql = $"SELECT Id, City, Address, postal_code AS PostalCode FROM {Table}";
        return _connection.Query<LocationModel>(sql).ToList();
    }
}