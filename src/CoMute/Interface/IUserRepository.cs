using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.Web.Interface
{
    public interface IUserRepository : IDisposable
    {
        RegistrationRequest Login(string username, string password);
        IEnumerable<RegistrationRequest> GetUsers();
        RegistrationRequest GetUserByID(int UserID);
        RegistrationRequest GetUserByEmail(string email);
        void InsertUser(RegistrationRequest registrationRequest);
        void DeleteUser(int UserID);
        void UpdateUser(RegistrationRequest registrationRequest);
        void Save();
        new void Dispose();
    }
}
