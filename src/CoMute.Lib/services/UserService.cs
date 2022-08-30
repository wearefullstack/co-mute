using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ansyl;
using CoMute.Lib.Dao.comute;
using CoMute.Lib.Dto;
using CoMute.Lib.Model;

namespace CoMute.Lib.services
{
    /// <summary>
    /// Implementation of the IUserService interface
    /// </summary>
    public class UserService : IUserService
    {
        private User GetUser(string email)
        {
            return OneTask<User>.Get(e => e.Email == email);
        }

        public UserDto GetLogin(string email, string password)
        {
            var user = GetUser(email);

            if (user == null)
                throw new Exception("Unknown user");

            if (user.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase) == false)
                throw new Exception("Wrong password");

            var dto = user.ToDto();
            dto.Password = null;
            return dto;

            //var sql = "SELECT * FROM `User` WHERE Email=@Email AND `Password`=@Password";
            //var data = new { email, password };
            //var user = new DataConnector().GetList<User>(sql, data).SingleOrDefault();
            //return user;
        }

        public UserDto AddUser(UserDto dto)
        {
            if (dto == null) return null;

            var email = dto.Email;
            var password = dto.Password;
            var name = dto.Name;
            var surname = dto.Surname;
            var phone = dto.Phone;

            var oldUser = GetUser(email);

            if (oldUser != null)
                throw new Exception("User exists");

            var newUser = new User
                          {
                              Email = email,
                              Password = password,
                              Name = name,
                              Surname = surname,
                              Phone = phone,
                              RegisterTime = DateTime.Now,
                              UserId = 0
                          };

            OneTask<User>.Insert(newUser);

            return GetUser(email).ToDto();
        }

        /// <summary>
        /// If an overlap of time is found, throw an exception
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="ownerId"></param>
        private void CheckTimeOverlap(Pool pool, int ownerId)
        {
            var ownedPools = OneTask<Pool>.List(e => e.OwnerId == ownerId);

            var dTime = pool.DepartTime;
            var aTime = pool.ArriveTime;

            foreach (var ownerPool in ownedPools)
            {
                var t1 = ownerPool.DepartTime;
                var t2 = ownerPool.ArriveTime;

                if (dTime >= t1 && dTime <= t2)
                    throw new Exception("Pool time overlap");

                if (aTime >= t1 && aTime <= t2)
                    throw new Exception("Time overlap");
            }
        }

        /// <summary>
        /// Create a Pool for the DTO request
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool CreatePool(PoolDto dto)
        {
            var newPool = dto.ToDao();
            newPool.CreatedTime = DateTime.Now;
            newPool.TotalSeats = dto.AvailableSeats;

            CheckTimeOverlap(newPool, newPool.OwnerId);

            return OneTask<Pool>.Insert(newPool) > 0;
        }

        /// <summary>
        /// Join the User to the Pool
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public bool JoinPool(int userId, int poolId)
        {
            using var ot = new OneTransaction();

            var user = ot.Get<User>(userId);
            var pool = ot.Get<Pool>(poolId);

            if (user == null) throw new Exception("Invalid user");
            if (pool == null) throw new Exception("Invalid pool");

            //  check time overlap
            CheckTimeOverlap(pool, userId);

            //  add user to pool
            var newUserPool = new UserPool
                              {
                                  UserId = userId,
                                  PoolId = poolId,
                                  UserPoolId = 0,
                                  JoinedTime = DateTime.Now
                              };

            //  decrease the record of available seats and update the pool
            pool.AvailableSeats -= 1;
            ot.Update(pool);

            //  insert the new UserPool record
            ot.Insert(newUserPool);

            //  commit all changes to this transaction
            return ot.SaveChanges();
        }

        /// <summary>
        /// Leave the Pool @ any time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public bool LeavePool(int userId, int poolId)
        {
            using var ot = new OneTransaction();

            var user = ot.Get<User>(userId);
            var pool = ot.Get<Pool>(poolId);

            if (user == null) throw new Exception("Invalid user");
            if (pool == null) throw new Exception("Invalid pool");

            //  increase the record of available seats and update the pool
            pool.AvailableSeats += 1;
            ot.Update(pool);

            //  delete the UserPool record
            ot.Delete<UserPool>(e => e.PoolId == poolId && e.UserId == userId);

            //  commit all changes to this transaction
            return ot.SaveChanges();
        }

        /// <summary>
        /// Pools owned by User
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public IEnumerable<PoolDto> GetOwnedPools(int ownerId)
        {
            var sn = 0;
            foreach (var pool in OneTask<Pool>.List(e => e.OwnerId == ownerId))
            {
                var dto = pool.ToDto();
                dto.Sn = ++sn;
                yield return dto;
            }
        }

        //public IEnumerable<PoolDto> GetOtherPools(int userId)
        //{
        //    var sn = 0;
        //    foreach (var pool in OneTask<Pool>.List())
        //    {
        //        var dto = pool.ToDto();
        //        dto.Sn = ++sn;
        //        yield return dto;
        //    }
        //}

        /// <summary>
        /// Package of Data for User - 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserPageModel GetUserPageModel(int userId)
        {
            var allPools = OneTask<Pool>.List().Select(i => i.ToDto()).ToList();
            var joinedPools = GetJoinedPools(userId).ToList();

            var otherPools = allPools.Where(p => joinedPools.Any(i => i.PoolId == p.PoolId) == false).AddSn();

            return new UserPageModel
                   {
                       //  user profile
                       User = GetUserProfile(userId),

                       //  pools that have been joined
                       JoinedPools = joinedPools,

                       //  pools that have NOT been joined
                       OtherPools = otherPools,

                       //  pools owned by user
                       OwnedPools = allPools.Where(i => i.OwnerId == userId).AddSn(),

                       //  all car pool owners
                       Owners = GetOwners().AddSn()
                   };
        }

        /// <summary>
        /// pools that have been joined
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserPoolDto> GetJoinedPools(int userId)
        {
            using var ot = new OneTransaction();

            var userPools = ot.List<UserPool>(e => e.UserId == userId);

            var sn = 0;
            foreach (var up in userPools)
            {
                var pool = ot.Get<Pool>(up.PoolId).ToDto();
                pool.Sn = ++sn;
                yield return new UserPoolDto
                             {
                                 UserId = up.UserId,
                                 PoolId = up.PoolId,
                                 JoinedTime = up.JoinedTime,
                                 Pool = pool,
                             };
            }
        }

        public UserDto GetUserProfile(int userId)
            => OneTask<User>.Get(userId).ToDto();

        /// <summary>
        /// all car pool owners
        /// </summary>
        /// <returns></returns>
        public IList<UserDto> GetOwners()
        {
            using var ot = new OneTransaction();

            var ownerIds = ot.List<Pool>().Select(i => i.OwnerId).ToList();

            if (ownerIds.Count == 0)
                return new List<UserDto>();

            var users = ot.List<User>();
            return users.Where(i => i.UserId.In(ownerIds)).Select(i => i.ToDto()).ToList();
        }

        /// <summary>
        /// update user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public UserDto UpdateProfile(int userId, UserDto dto)
        {
            try
            {
                using var ot = new OneTransaction();

                var user = ot.Get<User>(userId);
                user.Email = (Email) dto.Email;
                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.Phone = dto.Phone;
                ot.Update(user);
                ot.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            return OneTask<User>.Get(userId).ToDto();
        }


    }
}
