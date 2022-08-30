using System;
using ansyl.dao;

namespace CoMute.Lib.Dao.comute
{
    //  this is the data object for the Pool table
    class Pool : IDaoObject
    {
        public int PoolId { get; set; }
        public int OwnerId { get; set; }
        public TimeSpan DepartTime { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public byte AvailableSeats { get; set; }
        public string Notes { get; set; }
        public string AvailableDays { get; set; }
        public DateTime CreatedTime { get; set; }
        public byte TotalSeats { get; set; }
        public long GetPrimaryId() => PoolId;
    }
}