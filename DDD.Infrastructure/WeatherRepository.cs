using System.Data;
using Microsoft.Data.Sqlite;
using DDD.Domain.Repositories;
using DDD.Shared.Constants;

namespace DDD.Infrastructure;

public class WeatherRepository : IWeatherRepository
{
    public DataTable GetLatest(int areaId)
    {
        string sql_ = @"SELECT * FROM Weather WHERE AreaId = @AreaId ORDER BY DataDate DESC LIMIT 1";

        DataTable dt = new DataTable();
        try
        {
            using (var connection = new SqliteConnection(CommonConst.ConnectionString))
            using (var command = new SqliteCommand(sql_, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@AreaId", areaId);

                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("データベース接続エラー", ex);
        }
        return dt;
    }
}
