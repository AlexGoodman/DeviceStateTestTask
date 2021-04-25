using System.Linq;
using DeviceStateTestTask.Data.Entities;
using LinqToDB;

namespace DeviceStateTestTask.Data
{
    public class DataConnection: LinqToDB.Data.DataConnection
    {
        public DataConnection(string providerName, string connectionString) 
            : base(providerName, connectionString) 
        { 
            var sp = this.DataProvider.GetSchemaProvider();
            var dbSchema = sp.GetSchema(this);
            if(!dbSchema.Tables.Any(t => t.TableName == "Device"))
            {            
                this.CreateTable<Device>();
            }
        }

        public ITable<Device> Product => GetTable<Device>();
    }
}