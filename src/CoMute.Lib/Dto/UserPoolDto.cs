using System;

namespace CoMute.Lib.Dto
{
    /// <summary>
    /// DTO for User Pool
    /// </summary>
    public class UserPoolDto
    {
        public int UserPoolId { get; set; }
        public int PoolId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedTime { get; set; }
        public string JoinedDate => (ansyl.Date)JoinedTime.Date;
        public PoolDto Pool { get; set; }
    }
}
