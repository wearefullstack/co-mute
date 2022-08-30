using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace CoMute.Lib
{
    interface IDataConnector
    {
        IDbConnection GetConnection();
        IList<T> GetList<T>(string sql, object data = null);
        int Execute(string sql, object data = null);
    }

    class DataConnector : IDataConnector
    {
        const string connString = "Data Source=localhost;User Id=root;Password=;Port=40044;Database=comute; Convert Zero Datetime=true; Use Compression=true; Allow User Variables=True;";

        public IDbConnection GetConnection()
        {
            var connection = new MySqlConnection(connString);
            connection.Open();
            return connection;
        }

        public IList<T> GetList<T>(string sql, object data = null)
        {
            using var connection = GetConnection();
            return connection.Query<T>(sql, data ?? new { }).ToList();
        }

        public int Execute(string sql, object data = null)
        {
            using var connection = GetConnection();
            return connection.Execute(sql, data ?? new { });
        }
    }
}