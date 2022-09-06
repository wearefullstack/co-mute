using System.Data.SqlClient;
using System.Data;

namespace Co_Mute.Api.Contexts
{
    public class SqlConnectionContext
    {
        private readonly IConfiguration _iConfiguration;
        private readonly string _sConnectionString;

        public SqlConnectionContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _sConnectionString = _iConfiguration.GetConnectionString("Constr");
        }

   
        public string GetSecret()
        {
            return _iConfiguration.GetSection("AppSettings:Secret").Value;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_sConnectionString);
        }
    }
}
