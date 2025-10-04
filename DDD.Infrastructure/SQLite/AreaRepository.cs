using System.Data;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using Microsoft.Data.Sqlite;

namespace DDD.Infrastructure.SQLite;

public class AreaRepository : IAreaRepository
{
    public IReadOnlyList<AreaEntity> GetData()
    {
        string sql_ = @"SELECT * FROM Areas";

        try
        {
            using (var connection = new SqliteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SqliteCommand(sql_, connection))
            {
                connection.Open();
                var areas = new List<AreaEntity>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        int dbAreaId = reader.GetInt32("AreaId");
                        string dbAreaName = reader.GetString("AreaName");
                        areas.Add(new AreaEntity(dbAreaId, dbAreaName));

                    }
                }
                return areas;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("データベース接続エラー", ex);
        }
    }
}
