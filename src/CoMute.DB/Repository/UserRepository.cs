using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;

namespace CoMute.DB.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly ICoMuteDbContext _coMuteDbContext;
        public UserRepository(ICoMuteDbContext coMuteDbContext)
        {
            _coMuteDbContext = coMuteDbContext ?? throw new ArgumentNullException(nameof(coMuteDbContext));
        }

        public void DeleteUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _coMuteDbContext.Users.Remove(user);
            _coMuteDbContext.SaveChanges();
        }

        public List<User> GetAllUsers() => _coMuteDbContext.Users.ToList();
        
        public User GetById(Guid? userId)
        {
            if (userId == Guid.Empty) throw new ArgumentNullException(nameof(userId));
            return _coMuteDbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User GetUserByEmail(string emailAddress)
        {
            if(string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));
            return _coMuteDbContext.Users.FirstOrDefault(u => u.EmailAddress == emailAddress);
        }

        public void Save(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userExist = GetUserByEmail(user.EmailAddress);
            if (userExist != null) throw new ArgumentNullException("user with this email already exist.");

            _coMuteDbContext.Users.Add(user);
            _coMuteDbContext.SaveChanges();
        }

        public void Update(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _coMuteDbContext.Users.AddOrUpdate(user);
            _coMuteDbContext.SaveChanges();
        }

        public void JoinCarPool(JoinCarPoolsOpportunity joinCarPoolsOpportunity)
        {            
            _coMuteDbContext.JoinCarPoolsOpportunities.Add(joinCarPoolsOpportunity);
            _coMuteDbContext.SaveChanges();
        }
        public void LeaveCarPool(JoinCarPoolsOpportunity joinCarPoolsOpportunity)
        {
            _coMuteDbContext.JoinCarPoolsOpportunities.Remove(joinCarPoolsOpportunity);
            _coMuteDbContext.SaveChanges();
        }

        public User ValidateUser(string userName, string password)
        {
            var user = GetUserByEmail(userName);
            var isValidPassword = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

            return isValidPassword ? user : null;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
