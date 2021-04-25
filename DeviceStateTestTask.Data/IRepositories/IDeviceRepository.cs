using DeviceStateTestTask.Data.Entities;

namespace DeviceStateTestTask.Data.IRepositories
{
    public interface IDeviceRepository: IRepository<Device>
    {
        Device GetById(string id); 
    }
}