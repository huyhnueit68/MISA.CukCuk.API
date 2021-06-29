using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    public class BaseService<Generic> : IBaseService<Generic> where Generic:BaseEntity
    {
        #region DECLARE
        IBaseRepository<Generic> _baseRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<Generic> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = MISAEnum.Success };
        }
        #endregion

        #region Method
        public IEnumerable<Generic> Get()
        {
            return _baseRepository.Get();
        }

        public IEnumerable<Generic> GetById(Guid id)
        {
            return _baseRepository.GetById(id);
        }

        public virtual ServiceResult Insert(Generic data)
        {
            // set state action
            data.EntityState = EntityState.AddNew;

            // validate require
            var isValid = Validate(data);

            if(isValid)
            {
                _serviceResult.Data = _baseRepository.Insert(data);
            }


            return _serviceResult;
        }

        public ServiceResult Update(Guid id, Generic data)
        {
            // set state action
            data.EntityState = EntityState.Update;

            // validate require
            var isValid = Validate(data);

            if (isValid)
            {
                _serviceResult.Data = _baseRepository.Update(id, data);
            }

            return _serviceResult;
        }

        public ServiceResult DeleteById(Guid id)
        {
            _serviceResult = _baseRepository.DeleteById(id);
            if(Convert.ToInt32(_serviceResult.Data) > 0)
            {
                _serviceResult.MISACode = MISAEnum.Success;
                _serviceResult.Messenger = "Xóa dữ liệu thành công";
            } else
            {
                _serviceResult.MISACode = MISAEnum.NotValid;
                _serviceResult.Messenger = "Không tồn tại id: " + id;
            }
            return _serviceResult;
        }

        /// <summary>
        /// Validate data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// PQ Huy (28.06.2021)
        private bool Validate(Generic data)
        {
            var messArr = new List<string>();
            bool isValid = true;

            // Get all property:
            var properties = data.GetType().GetProperties();

            foreach(var property in properties)
            {
                // get property name
                var propertyName = "";
                if(property.GetCustomAttributesData().Count() != 0)
                {
                    try
                    {
                        propertyName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                    } catch(Exception ce)
                    {
                        propertyName = "";
                        Console.Write(ce);
                    }
                }

                // check attribute need validate
                if (property.IsDefined(typeof(Required), false))
                {
                    // check required
                    var propertyValue = property.GetValue(data);
                    if(propertyValue == null)
                    {
                        isValid = false;
                        messArr.Add($"Vui lòng không để trống {propertyName}");
                        _serviceResult.MISACode = MISAEnum.NotValid;
                        _serviceResult.Data = messArr;
                        _serviceResult.Messenger = "Dữ liệu không hợp lệ";

                        return isValid;
                    }
                }
                
                if (property.IsDefined(typeof(CheckDuplicate), false))
                {
                    // check duplicate data
                    var valueDuplicate = _baseRepository.GetEntityByProperty(data, property);
                    if(valueDuplicate.Count() != 0)
                    {
                        isValid = false;

                        messArr.Add($"{propertyName} {property.GetValue(data)} đã tồn tại");
                        _serviceResult.MISACode = MISAEnum.NotValid;
                        _serviceResult.Data = messArr;
                        _serviceResult.Messenger = "Dữ liệu không hợp lệ";

                        return isValid;
                    }
                }
            }

            _serviceResult.MISACode = MISAEnum.IsValid;
            _serviceResult.Data = messArr;
            _serviceResult.Messenger = "Validate dữ liệu hợp lệ";

            return isValid;
        }

        public IEnumerable<Generic> ProcessDataImport(string path)
        {
            // get file import
            // path to your excel file
            FileInfo fileInfo = new FileInfo(path);

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7

            // convert data in file to object
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= columns; j++)
                {

                    string content = worksheet.Cells[i, j].Value.ToString();
                    
                }
            }


            // validate object and set status for data

            throw new NotImplementedException();
        }

        public ServiceResult ImportData(Generic[] data)
        {
            // count import data success

            // load array data and insert by api insert

            //return result
            throw new NotImplementedException();
        }

        #endregion
    }
}
