using System;
using LinqToDB.Mapping;

namespace DeviceStateTestTask.Data.Entities
{
    public class Device: IEntity
    {
        [PrimaryKey, NotNull]     
        public string Id { get; set; }
        
        [NotNull, SkipValuesOnUpdate(null)]
        public string ComputerName {get; set;}
        
        [NotNull, SkipValuesOnUpdate(null)]
        public string TimeZone {get; set;}
        
        [NotNull, SkipValuesOnUpdate(null)]
        public string OsName {get; set;}
        
        [NotNull, SkipValuesOnUpdate(null)]
        public string NetVersion {get; set;}
        
        [NotNull]
        public bool IsOnline {get; set;}
        
        [NotNull]
        public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    }
}