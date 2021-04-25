using System.Linq;
using DeviceStateTestTask.Data.Entities;
using DeviceStateTestTask.Data.IRepositories;
using LinqToDB;

namespace DeviceStateTestTask.Data.Repositories
{
    public class DeviceRepository: IDeviceRepository
    {
        private readonly DataConnection _db;

        public DeviceRepository(DataConnection dataConnection)
        {
            this._db = dataConnection;
        }
    
        public Device GetById(string id)
        {
            return this._db.GetTable<Device>()            
                .FirstOrDefault<Device>(d => d.Id == id);           
        } 

        public int Insert(Device device)
        {
            return this._db.Insert<Device>(device);
        }

        public int Update(Device device)
        {
            return this._db.Update<Device>(device);
        }
    }
}