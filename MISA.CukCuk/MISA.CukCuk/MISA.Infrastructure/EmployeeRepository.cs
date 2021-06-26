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
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;
        #endregion

        #region Contructor
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        #region Method
        public IEnumerable<Employee> GetEmployees()
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var resEmployees = _dbConnection.Query<Employee>("Proc_GetEmployees", commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu kết quả
            return resEmployees;
        }

        public IEnumerable<Employee> GetEmployeeById(Guid id)
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var resEmployee = _dbConnection.Query<Employee>("Proc_GetEmployeeById", new { EmployeeId = id} ,commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu kết quả
            return resEmployee;
        }

        public Employee GetEmployeeByCode(string code)
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var resEmployee = _dbConnection.QueryFirstOrDefault<Employee>("Proc_GetEmployeeByCode", new { EmployeeCode = code }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            //Trả về dữ liệu kết quả
            return resEmployee;
        }

        public ServiceResult InsertEmployee(Employee employee)
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var rowAffects = _dbConnection.Execute("Proc_InsertEmployee", employee, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Thêm mới thành công";

            //Trả về dữ liệu số bản ghi thêm mới
            return serviceResult;
        }

        public ServiceResult UpdateEmployee(Guid id, Employee employee)
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var rowAffects = _dbConnection.Execute("Proc_UpdateCustomer", new { EmployeeId = id, Employee = employee }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Cập nhật thành công";

            //Trả về dữ liệu số bản ghi thêm mới
            return serviceResult;
        }

        public ServiceResult DeleteEmployeeById(Guid id)
        {
            // kết nối database
            _dbConnection.Open();

            //khởi tạo và thực thi các commandText
            var rowAffects = _dbConnection.Execute("Proc_DeleteEmployeeById", new { EmployeeId = id}, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();

            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Xóa thành công";

            //Trả về dữ liệu số bản ghi thêm mới
            return serviceResult;
        }
        #endregion
    }
}
