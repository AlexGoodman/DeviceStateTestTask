using DeviceStateTestTask.Data.Entities;

namespace DeviceStateTestTask.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {        
        int Insert(TEntity entity);
        int Update(TEntity entity);
    }
}