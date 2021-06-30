using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MISA.ApplicationCore.Resource;

namespace MISA.ApplicationCore.Service
{
    public class BaseService<Generic> : IBaseService<Generic> where Generic:BaseEntity
    {
        #region DECLARE
        IBaseRepository<Generic> _baseRepository;
        ServiceResult _serviceResult;
        public string _tableName = string.Empty;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<Generic> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = MISAEnum.Success };
            _tableName = typeof(Generic).Name;
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
            // import resource file
            // Create a resource manager to retrieve resources.
            ResourceManager resourceManager = new ResourceManager($"MISA.ApplicationCore.Resource.{_tableName}", Assembly.GetExecutingAssembly());

            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            // path to your excel file, get file import
            FileInfo fileInfo = new FileInfo(path);

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows;
            int columns = worksheet.Dimension.Columns;

            /*
                Convert data table to object with key and value
             */

            List<string> listKey = new List<string>();
            List<SortedList> objDataTable = new List<SortedList>();

            // get title table convert to resource
            for (int i = 1; i <= columns; i++)
            {
                string title = worksheet.Cells[2, i].Value.ToString();

                // format title
                string formatTitle = title.Trim(new Char[] { ' ', '(', '*',')' , '.' }).Trim();

                // covert title to lowercase string
                string titleLowerCase = formatTitle.ToLower();

                // compare with resouce and save to list key
                foreach (DictionaryEntry entry in resourceSet)
                {
                    string resourceKey = entry.Key.ToString();
                    string resourceValue = entry.Value.ToString();
                    
                    if(titleLowerCase == resourceValue.ToLower())
                    {
                        listKey.Add(resourceKey);
                        break;
                    }
                }
            }

            // match value with resource


            for (int i = 3; i <= rows; i++)
            {
                /*List<object> temp = new List<object>();*/
                SortedList temp = new SortedList();

                var temp1 = new{};
                for (int j = 0; j < listKey.Count(); j++)
                {
                    string key = listKey[j];
                    string value = "";
                    if(worksheet.Cells[i, j + 1].Value != null)
                    {
                        value = worksheet.Cells[i, j + 1].Value.ToString();
                    }
                    temp.Add(key, value);
                }

                /*var json = JsonSerializer.Serialize(temp);*/

                objDataTable.Add(temp);
            }

            /* validate object and set status for data */

            // validate in file
            for(int i = 0; i < objDataTable.Count(); i++)
            {
                ServiceResult serviceResult = new ServiceResult();
                serviceResult.MISACode = MISAEnum.IsValid;
                SortedList items = objDataTable[i]; // get items need compare

                for (int j = 0; i < objDataTable.Count(); i++)
                {
                    if(i!=j)
                    {
                        SortedList temp = objDataTable[j]; // get items need compare
                        
                        foreach(var item in items)
                        {
                            string validate = "ValidateResult";
                            if (item.GetType().GetProperty("Key").GetValue(item).ToString() == $"{_tableName}Code")
                            {
                                // validate not null
                                if(item.GetType().GetProperty("Value").GetValue(item) == null || item.GetType().GetProperty("Value").GetValue(item) == "")
                                {
                                    string valueResource = GetValueResource($"{_tableName}Code");
                                    serviceResult.MISACode = MISAEnum.NotValid;
                                    serviceResult.Messenger = $"{valueResource} không được để trống";

                                    // add resource to object
                                    objDataTable[i].Add(validate, serviceResult);
                                } else
                                {
                                    // validate duplicate
                                    string itemCode = item.GetType().GetProperty("Value").GetValue(item).ToString();

                                    // add resource to object
                                    objDataTable[i].Add(validate, serviceResult);
                                }
                            }
                        }
                    }
                }
            }

            foreach(SortedList property in objDataTable)
            {
                foreach(var prop in property)
                {
                    string key = prop.GetType().GetProperty("Key").GetValue(prop).ToString();
                }
            }

            // validate in database


            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy value từ resource
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetValueResource(string key)
        {
            string value = "";
            // import resource file
            // Create a resource manager to retrieve resources.
            ResourceManager resourceManager = new ResourceManager($"MISA.ApplicationCore.Resource.{_tableName}", Assembly.GetExecutingAssembly());

            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if(key == entry.Key.ToString())
                {
                    value = entry.Value.ToString();
                }
            }

            return value;
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
