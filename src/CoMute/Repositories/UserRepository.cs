using CoMute.Web.Context;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BC = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Windows;

namespace CoMute.Web.Repositories
{

    //public interface IUserRepository
    //{
    //   void AddUser(User user);
    //    // void Delete(Guid id);
    //}

    public class UserRepository
    {
        private readonly CoMuteDbContext _context;

        public UserRepository()
        {
            //CoMuteDbContext context
            _context = new CoMuteDbContext();

        }

        public void AddUser(User user) 
        {
            user.Password = BC.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public AuthenticateResponse GetUser(string userGuid)
        {
            //string userId = userGuid.ToString();

            var user = _context.Users.Find(Guid.Parse(userGuid));
            AuthenticateResponse response = new AuthenticateResponse();

            if (user != null)
            {
                
                response.FirstName = user.FirstName;
                response.LastName = user.Surname;
                response.UserGuid = user.UserGuid;
                response.Email = user.Email;
                response.Phone = user.Phone;
              
            }
            
          

            return response;
        }

        public bool UpdateUser(User updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserGuid == updatedUser.UserGuid);

            if (existingUser == null)
            {
                return false; // User not found
            }

            updatedUser.CreatedDate = existingUser.CreatedDate;
            updatedUser.Password= BC.HashPassword(updatedUser.Password);


            _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
            _context.SaveChanges();

            return true;
        }

        public AuthenticateResponse Sign(LoginRequest model)
        {
            AuthenticateResponse response = new AuthenticateResponse();

            var account = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            if (account == null || !BC.Verify(model.Password, account.Password))
            {
                response.IsResult = false;// throw new AppException("Email or password is incorrect");
                
            }else
            {
                // authentication successful so generate jwt and refresh tokens
                var jwtToken = generateJwtToken(account);

                response.FirstName = account.FirstName;
                response.LastName = account.Surname;
                response.UserGuid = account.UserGuid;
                response.Email = account.Email;
                response.userObject = account;
                response.JwtToken = jwtToken;
                response.IsResult = true;
            }
               
            return response;
        }

        private string generateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(account.UserGuid.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.UserGuid.ToString()), new Claim("email", account.Email.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}