using System;
using System.Data;

namespace DDD.WinForm.Repositories;

public interface IWeatherRepository
{
    DataTable GetLatest(int areaId);
}
