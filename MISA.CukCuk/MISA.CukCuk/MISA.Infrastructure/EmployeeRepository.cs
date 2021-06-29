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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region DECLARE

        #endregion

        #region Contructor
        public EmployeeRepository(IConfiguration configuration) : base (configuration)
        {

        }
        #endregion

        #region Method


        #endregion
    }
}
