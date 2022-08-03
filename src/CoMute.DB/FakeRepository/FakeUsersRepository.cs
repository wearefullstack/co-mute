using CoMute.Core.Domain;
using System.Collections.Generic;

namespace CoMute.DB.FakeRepository
{
    public interface IFakeUsersRepository
    {
        void Save(User user);
        List<User> GetAllUsers();
    }
    public class FakeUsersRepository : IFakeUsersRepository
    {
        private List<User> users = new List<User>();
        public List<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public void Save(User user)
        {
            users.Add(user);
        }
    }
}
