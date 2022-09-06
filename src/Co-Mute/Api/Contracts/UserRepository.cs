using Co_Mute.Api.Contexts;
using Co_Mute.Api.Models;
using Co_Mute.Api.Models.Dto;
using Co_Mute.Api.Repository;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Co_Mute.Api.Contracts
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnectionContext _oSqlConnectionContext;



        public UserRepository(SqlConnectionContext oSqlConnectionContext)
        {
            _oSqlConnectionContext = oSqlConnectionContext;
        }

        async Task<FunctionCommandUser> IUserRepository.LoginUser(UserLogin userLogin)
        {
            string procedureName = "GetUserLoginDetail";
            var oParameters = new DynamicParameters();

            oParameters.Add("Email", userLogin.Email, DbType.String);

            using (var connection = _oSqlConnectionContext.CreateConnection())
            {
                var exists = await connection.QueryAsync<int>("SELECT 1 FROM Users WHERE Email = @email", new { email = userLogin.Email});

                if (!exists.Any())
                {
                    return null;
                }

                var user = await connection.QueryFirstAsync<GetUser>(procedureName, oParameters, commandType: CommandType.StoredProcedure);

                
                if (VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                {
                    var userId = new FunctionCommandUser();

                    userId.UserId = user.UserId;

                    return userId;
                }
                return null;
            }
        }



        async Task<int> IUserRepository.UpdateUserDetails(Co_Mute.Api.Models.Dto.UpdateUser updateUser, int id)
        {
            CreatePasswordHash(updateUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            string procedureName = "UpdateUserDetails";
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", id, DbType.Int32);
            parameters.Add("@Name", updateUser.Name, DbType.String);
            parameters.Add("@Surname", updateUser.Surname, DbType.String);
            parameters.Add("@Email", updateUser.Email, DbType.String);
            parameters.Add("@Phone", updateUser.Phone, DbType.String);
            parameters.Add("@Password", passwordHash, DbType.Binary);
            parameters.Add("@PasswordSalt", passwordSalt, DbType.Binary);

            using (var connection = _oSqlConnectionContext.CreateConnection())
            {
                var userDetails = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return userDetails;
            }
        }

        async Task IUserRepository.RegisterNewUser(UserRegisterDto oCreateUser)
        {
            CreatePasswordHash(oCreateUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            string sProcedureName = "RegisterNewUser";
            var oParameters = new DynamicParameters();
            oParameters.Add("@Name", oCreateUser.Name, DbType.String);
            oParameters.Add("@Surname", oCreateUser.Surname, DbType.String);
            oParameters.Add("@Email", oCreateUser.Email, DbType.String);
            oParameters.Add("@Phone", oCreateUser.Phone, DbType.String);
            oParameters.Add("@Password", passwordHash, DbType.Binary);
            oParameters.Add("@Salt", passwordSalt, DbType.Binary);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                 await sConnection.QueryAsync(sProcedureName, oParameters, commandType: CommandType.StoredProcedure);
                
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte []passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash); 
            }
        }

        public string CreateToken(UserLogin oUserLogin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, oUserLogin.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_oSqlConnectionContext.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        
    }
}
