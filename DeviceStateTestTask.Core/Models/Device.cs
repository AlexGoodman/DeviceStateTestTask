using System;

namespace DeviceStateTestTask.Core.Models
{
    public class Device
    {
        public string Id {get; set;}
        public string ComputerName {get; set;}
        public string TimeZone {get; set;}
        public string OsName {get; set;}
        public string NetVersion {get; set;}
        public bool IsOnline {get; set;} = false;
        public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;       

        public bool IsStateChanged(Device newDevice)
        {
            if (this.Id != newDevice.Id)
            {
                throw new Exception("Ids not matched!");
            }
        
            if (
                this.ComputerName != newDevice.ComputerName
                || this.TimeZone != newDevice.TimeZone
                || this.OsName != newDevice.OsName
                || this.NetVersion != newDevice.NetVersion
                || this.IsOnline != newDevice.IsOnline
            )
            {
                return true;
            }

            return false;
        }            
    }
}