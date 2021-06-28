using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class BaseRepository<Generic> : IBaseRepository<Generic> where Generic : BaseEntity
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName = string.Empty;
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(Generic).Name;
        }
        #endregion

        #region Method
        public IEnumerable<Generic> Get()
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var resEmployees = _dbConnection.Query<Generic>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu kết quả
            return resEmployees;
        }

        public IEnumerable<Generic> GetById(Guid id)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var parameterId = new DynamicParameters();
            parameterId.Add($"@{_tableName}Id", id);

            var data = _dbConnection.Query<Generic>($"Proc_Get{_tableName}ById", parameterId, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu
            return data;
        }

        public ServiceResult Insert(Generic data)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}", data, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Thêm mới thành công";
            //Trả về dữ liệu số bản ghi thêm mới
            return serviceResult;
        }

        public ServiceResult Update(Guid id, Generic data)
        {
            var serviceResult = new ServiceResult();

            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var parameter = MappingDbType(data);

            var rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameter, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Cập nhật thành công";

            return serviceResult;
        }

        public ServiceResult DeleteById(Guid id)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var parameterId = new DynamicParameters();
            parameterId.Add($"@{_tableName}Id", id);

            var rowAffects = _dbConnection.Execute($"Proc_Delete{_tableName}ById", parameterId, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi xóa
            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Cập nhật thành công";
            return serviceResult;
        }

        protected DynamicParameters MappingDbType<Generic>(Generic generic)
        {
            var properties = generic.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(generic);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            return parameters;
        }

        public IEnumerable<Generic> GetEntityByProperty(Generic generic, PropertyInfo property)
        {
             

            // connection database
            _dbConnection.Open();

            // query database
            var propertyName = property.Name;
            var propertyValue = property.GetValue(generic);
            var keyValue = generic.GetType().GetProperty($"{_tableName}Id").GetValue(generic);
            var query = "";
            

            // check state action 
            if (generic.EntityState == EntityState.AddNew)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            } else  if( generic.EntityState == EntityState.Update)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            } else
            {
                return null;
            }

            var entity = _dbConnection.Query<Generic>(query, commandType: CommandType.Text);

            return entity;
        }


        #endregion
    }
}
