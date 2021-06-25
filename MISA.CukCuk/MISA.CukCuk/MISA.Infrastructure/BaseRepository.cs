using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class BaseRepository<Generic> : IBaseRepository<Generic>
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;
        #endregion

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
        }

        public IEnumerable<Generic> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Generic> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(Generic data)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(Guid id, Generic data)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
