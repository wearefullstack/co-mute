using CoMute.API.Models;
using CoMute.API.Models.Authentication;
using CoMute.API.Models.Tokens;
using CoMute.API.Models.Users;
using CoMute.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.API.Services.Users
{
    public class UserService:IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings  _jwt;
        private readonly OpportunityDbContext opportunityDbContext;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwt, OpportunityDbContext opportunityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            this.opportunityDbContext = opportunityDbContext;
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"FAILED.No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            var passCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (passCheck)
            {
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Message = $"SUCCESS.Incorrect Credentials for user {user.Email}.";
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                authenticationModel.UserId = user.Id;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"FAILED.Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {              
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
                Email = model.Email,
                CustomPhone = model.Phone,
                CustomEmail = model.Email,
                Password = model.Password,
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.Roles.User.ToString());
                    return $"SUCCESS.User Registered with email {user.Email}";
                }
                return $"{result}";
            }
            else
            {
                return $"FAILED.Email {user.Email } is already registered.";
            }
        }

        /// <summary>
        /// Method is used to add or assign roles to users if needed
        /// Only an ADMIN Role will be able to access this function
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }            

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExists = Enum.GetNames(typeof(Authorization.Roles)).Any(x => x.ToLower() == model.Role.ToLower());
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                var existingRole = rolesList.Where(x => x.Contains(model.Role)).Count() ;

                if(roleExists && existingRole > 0)
                    return $"{model.Role} was already assigned to the user with email {model.Email}.";

                if (roleExists)
                {
                    var validRole = Enum.GetValues(typeof(Authorization.Roles)).Cast<Authorization.Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return $"Added {model.Role} to user {model.Email}.";
                }
                return $"Role {model.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";
        }

        public async Task<ProfileModel> GetUserProfileAsync(string userId)
        {
            var userDetails = await _userManager.FindByIdAsync(userId);

            if (userDetails == null)
                return null;

            var profile = new ProfileModel()
            {
                UserId  = userDetails.Id,
                Name  = userDetails.Name,
                Surname = userDetails.Surname, 
                UserName = userDetails.UserName, 
                CustomPhone = userDetails.CustomPhone, 
                CustomEmail  = userDetails.CustomEmail,
            };

            return profile;
        }

        public async Task<string> UpdateUserProfileAsync(ProfileModel profileModel)
        {
            var userDetails = await _userManager.FindByIdAsync(profileModel.UserId);
            if (userDetails == null) 
              return "FAILED.Could not find user on the system";            

            var users = userDetails;
            userDetails.Id = profileModel.UserId;
            userDetails.Name = profileModel.Name;
            userDetails.Surname = profileModel.Surname;
            userDetails.UserName = profileModel.UserName;
            userDetails.CustomPhone = profileModel.CustomPhone;
            userDetails.PhoneNumber = profileModel.CustomPhone;
            userDetails.CustomEmail = profileModel.CustomEmail;
            userDetails.Email = profileModel.CustomEmail;
            //userDetails.Password = profileModel.Password;



           
            // var result = await _userManager.UpdateAsync(users);
            //if(!result.Succeeded)
            //     return "FAILED.Could not update Profile";
            await _userManager.UpdateAsync(users);
            //await opportunityDbContext.SaveChangesAsync();

            return "SUCCESS.Profile Updated Successfully";
        }
    }
}
