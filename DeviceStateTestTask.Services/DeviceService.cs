using AutoMapper;
using DeviceStateTestTask.Core.IServices;
using DeviceModel = DeviceStateTestTask.Core.Models.Device;
using DeviceEntity = DeviceStateTestTask.Data.Entities.Device;
using DeviceStateTestTask.Data.IRepositories;

namespace DeviceStateTestTask.Services
{
    public class DeviceService: IDeviceService
    {

        private readonly IDeviceRepository _repository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository repository)
        {
            this._repository = repository;
            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<DeviceEntity, DeviceModel>();
                config.CreateMap<DeviceModel, DeviceEntity>();
            });
            this._mapper = new Mapper(config);;
        }

        public bool SaveChanges(DeviceModel newDevice)
        {
            DeviceModel currentDevice = this._mapper.Map<DeviceModel>(this._repository.GetById(newDevice.Id));
            DeviceEntity newDeviceEntity = this._mapper.Map<DeviceEntity>(newDevice);

            if (currentDevice == null) 
            {
                return this._repository.Insert(newDeviceEntity) > 0;
            }
            
            if (currentDevice.IsStateChanged(newDevice) == true) 
            {
                return this._repository.Update(newDeviceEntity) > 0;
            }

            return false;
        }
    }
}