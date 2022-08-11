﻿using CoMute.Core.Domain;
using System;
using System.Collections.Generic;

namespace CoMute.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void Save(User user);
        User GetById(Guid? userId);
        User GetUserByEmail(string emailAddress);
        void DeleteUser(User user);
        void Update(User user);
        User ValidateUser(string userName, string password);
    }
}
