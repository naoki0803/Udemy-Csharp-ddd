using System.ComponentModel;
using DDD.Domain.Entities;

namespace DDD.Domain.Repositories;

public interface IAreaRepository
{
    public IReadOnlyList<AreaEntity> GetData();
}
