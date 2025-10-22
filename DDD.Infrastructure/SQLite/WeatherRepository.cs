using System.Data;
using Microsoft.Data.Sqlite;
using DDD.Domain.Repositories;
using DDD.Domain.Entities;

namespace DDD.Infrastructure.SQLite;

public class WeatherRepository : IWeatherRepository
{
    public IReadOnlyList<WeatherEntity> GetData()
    {
        string sql = @"
        SELECT W.AreaId, A.AreaName, W.DataDate, W.Condition, W.Temperature 
        FROM Weather w
        INNER JOIN Areas A
        ON w.AreaId = A.AreaID;";

        return SQLiteHelper.Query(sql, reader =>
        {
            return new WeatherEntity(
                reader.GetInt32("AreaId"),
                reader.GetString("AreaName"),
                reader.GetDateTime("DataDate"),
                reader.GetInt32("Condition"),
                reader.GetFloat("Temperature")
            );
        });
    }

    public WeatherEntity? GetLatest(int areaId)
    {
        string sql = @"SELECT * FROM Weather WHERE AreaId = @AreaId ORDER BY DataDate DESC LIMIT 1";

        return SQLiteHelper.QuerySingle(sql, new List<SqliteParameter> { new SqliteParameter("@AreaId", areaId) }.ToArray(), reader =>
        {
            return new WeatherEntity(
                reader.GetInt32("AreaId"),
                reader.GetDateTime("DataDate"),
                reader.GetInt32("Condition"),
                reader.GetFloat("Temperature")
            );
        }, null);
    }

    public void Save(WeatherEntity entity)
    {
        string sql = @"INSERT INTO Weather (AreaId, DataDate, Condition, Temperature) VALUES (@AreaId, @DataDate, @Condition, @Temperature)";

        var parameters = new SqliteParameter[]
        {
            new SqliteParameter("@AreaId", entity.AreaId.Value),
            new SqliteParameter("@DataDate", entity.DataDate),
            new SqliteParameter("@Condition", entity.Condition.Value),
            new SqliteParameter("@Temperature", entity.Temperature.Value)
        };

        SQLiteHelper.Execute(sql, parameters);
    }
}
