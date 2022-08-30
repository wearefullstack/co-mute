using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoMute.Lib.Dto;

namespace CoMute.Lib.Model
{
    public class UserPageModel
    {
        public UserDto User { get; set; }
        public IList<PoolDto> OwnedPools { get; set; } = new List<PoolDto>();
        public IList<UserPoolDto> JoinedPools { get; set; } = new List<UserPoolDto>();
        public IList<PoolDto> OtherPools { get; set; } = new List<PoolDto>();
        public IList<UserDto> Owners { get; set; } = new List<UserDto>();
    }
}
