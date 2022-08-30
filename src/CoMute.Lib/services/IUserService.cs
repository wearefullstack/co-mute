using System.Collections.Generic;
using CoMute.Lib.Dto;
using CoMute.Lib.Model;

namespace CoMute.Lib.services
{
    public interface IUserService
    {
        UserDto GetLogin(string email, string password);
        UserDto AddUser(UserDto dto);
        //UserDto AddUser(string email, string password, string name, string surname, string phone);
        bool CreatePool(PoolDto dto);
        bool JoinPool(int userId, int poolId);
        //bool LeavePool(int userPoolId);
        bool LeavePool(int userId, int poolId);
        IEnumerable<UserPoolDto> GetJoinedPools(int userId);
        IEnumerable<PoolDto> GetOwnedPools(int ownerId);
        //IEnumerable<PoolDto> GetOtherPools(int userId);
        UserPageModel GetUserPageModel(int userId);
        IList<UserDto> GetOwners();
        UserDto UpdateProfile(int userId, UserDto dto);
    }
}