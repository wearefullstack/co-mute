using CoMute.Core.Interfaces.Repositories;
using CoMute.DB;
using CoMute.DB.Repository;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoMute.Web.Models.Providers
{
    //public class AuthProvider : OAuthAuthorizationServerProvider
    //{
    //    private IUserRepository _userRepository;
    //    public IUserRepository UserRepository
    //    {
    //        get { return _userRepository ?? (_userRepository = new UserRepository(new CoMuteDbContext())); }
    //        set
    //        {
    //            if (_userRepository != null) throw new InvalidOperationException("UserRepository is already set");
    //            _userRepository = value;
    //        }
    //    }

    //    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    //    {
    //        var user = UserRepository.ValidateUser(context.UserName, context.Password);
    //        if (user == null)
    //        {
    //            context.SetError("invalid_grant", "Provided username and password is incorrect");
    //            return;
    //        }
    //        var identity = new ClaimsIdentity(context.Options.AuthenticationType);            
    //        identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
    //        identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));                        
    //        context.Validated(identity);
    //    }
    //}
}