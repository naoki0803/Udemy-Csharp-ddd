using System.Data;
using Microsoft.Data.Sqlite;
using DDD.Domain.Repositories;
using DDD.Domain.Entities;

namespace DDD.Infrastructure.SQLite;

public class WeatherRepository : IWeatherRepository
{
    public WeatherEntity GetLatest(int areaId)
    {
        string sql_ = @"SELECT * FROM Weather WHERE AreaId = @AreaId ORDER BY DataDate DESC LIMIT 1";

        try
        {
            using (var connection = new SqliteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SqliteCommand(sql_, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@AreaId", areaId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        int dbAreaId = reader.GetInt32("AreaId");
                        DateTime dbDataDate = reader.GetDateTime("DataDate");
                        int dbCondition = reader.GetInt32("Condition");
                        float dbTemperature = reader.GetFloat("Temperature");

                        return new WeatherEntity(dbAreaId, dbDataDate, dbCondition, dbTemperature);
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("データベース接続エラー", ex);
        }
    }
}
