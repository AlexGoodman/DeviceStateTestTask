using DeviceStateTestTask.Core.Models;

namespace DeviceStateTestTask.Core.IServices
{
    public interface IDeviceService
    {
         bool SaveChanges(Device device);
    }
}