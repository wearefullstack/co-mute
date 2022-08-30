using System;
using ansyl.dao;

namespace CoMute.Lib.Dao.comute
{
    //  this is the data object for the UserPool table
    class UserPool : IDaoObject
    {
        public int UserPoolId { get; set; }
        public int PoolId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedTime { get; set; }
        public long GetPrimaryId() => UserPoolId;
    }
}