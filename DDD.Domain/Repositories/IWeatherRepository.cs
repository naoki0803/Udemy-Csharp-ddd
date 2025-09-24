using System.Data;

namespace DDD.Domain.Repositories;

public interface IWeatherRepository
{
    DataTable GetLatest(int areaId);
}