using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace CoMute.DB.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        { }
        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid? userId)
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
