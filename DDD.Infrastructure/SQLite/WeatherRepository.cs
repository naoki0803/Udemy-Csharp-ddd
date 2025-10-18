using System.Data;
using Microsoft.Data.Sqlite;
using DDD.Domain.Repositories;
using DDD.Domain.Entities;

namespace DDD.Infrastructure.SQLite;

public class WeatherRepository : IWeatherRepository
{
    public IReadOnlyList<WeatherEntity> GetData()
    {
        throw new NotImplementedException();
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
}
