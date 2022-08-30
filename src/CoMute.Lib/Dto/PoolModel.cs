using System;

namespace CoMute.Lib.Dto
{
    /// <summary>
    /// abstract class which adds a field for Serial Number.
    /// This is useful for lists of objects
    /// </summary>
    public abstract class BaseSn
    {
        public int Sn { get; set; }
    }

    /// <summary>
    /// DTO for Pool
    /// </summary>
    public class PoolDto : BaseSn
    {
        public int PoolId { get; set; }
        public int OwnerId { get; set; }
        public string DepartTime { get; set; }
        public string ArriveTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public byte AvailableSeats { get; set; }
        public string Notes { get; set; }
        public string[] AvailableDays { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedDate => (ansyl.Date)CreatedTime.Date;
        public string Route { get; set; }
        public string Duration { get; set; }
    }
}
