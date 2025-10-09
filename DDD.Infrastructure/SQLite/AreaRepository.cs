using System.Data;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;

namespace DDD.Infrastructure.SQLite;

public class AreaRepository : IAreaRepository
{

    public IReadOnlyList<AreaEntity> GetData()
    {
        string sql = @"SELECT * FROM Areas";
        return SQLiteHelper.Query(sql, reader =>
        {
            return new AreaEntity(reader.GetInt32("AreaId"), reader.GetString("AreaName"));
        });
    }

}
