﻿using Dapper;
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
using System.Text.Json;

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

        public string ProcessDataImport(string path)
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
                    string formatTitle = title.Trim(new Char[] { ' ', '(', '*', ')', '.' }).Trim();

                    // covert title to lowercase string
                    string titleLowerCase = formatTitle.ToLower();

                    // compare with resouce and save to list key
                    foreach (DictionaryEntry entry in resourceSet)
                    {
                        string resourceKey = entry.Key.ToString();
                        string resourceValue = entry.Value.ToString();

                        if (titleLowerCase == resourceValue.ToLower())
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

                    var temp1 = new { };
                    for (int j = 0; j < listKey.Count(); j++)
                    {
                        string key = listKey[j];
                        string value = "";
                        if (worksheet.Cells[i, j + 1].Value != null)
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
                for (int i = 0; i < objDataTable.Count(); i++)
                {
                    // get items need compare
                    SortedList items = objDataTable[i];
                    List<object> consoleMess = new List<object>();
                    bool isNull = true, isDuplicateFile = true, isDuplicateDb = true;

                    for (int j = 0; j < objDataTable.Count(); j++)
                    {
                        if (i != j)
                        {
                            ServiceResult serviceResult = new ServiceResult();
                            serviceResult.MISACode = MISAEnum.IsValid;
                            SortedList temp = objDataTable[j]; // get items need compare
                        
                            foreach (var item in items)
                            {
                                string validate = "ValidateResult";

                                // validate code
                                if (item.GetType().GetProperty("Key").GetValue(item).ToString() == $"{_tableName}Code")
                                {
                                    string valueResource = GetValueResource($"{_tableName}Code");
                                    var value = item.GetType().GetProperty("Value").GetValue(item);
                                    // validate not null

                                    if (value is null or "")
                                    {
                                        if(isNull)
                                        {
                                            serviceResult.MISACode = MISAEnum.NotValid;
                                            serviceResult.Messenger = $"{valueResource} không được để trống";
                                            isNull = false;
                                            consoleMess.Add(serviceResult);
                                            // add resource to object
                                        }
                                    }
                                    else
                                    {
                                        // validate duplicate in file
                                        string itemCode = item.GetType().GetProperty("Value").GetValue(item).ToString();
                                        string valueValidate = getValueSortedList(temp, $"{_tableName}Code");

                                        if (itemCode == valueValidate)
                                        {
                                            if(isDuplicateFile)
                                            {
                                                // set message service result
                                                serviceResult.MISACode = MISAEnum.NotValid;
                                                serviceResult.Messenger = $"{valueResource} {itemCode} đã trùng lặp trong tệp của bạn";
                                                isDuplicateFile = false;
                                                consoleMess.Add(serviceResult);
                                            }
                                        }

                                        // validate in database
                                        var resFilter = _baseRepository.GetByCode(itemCode);
                                        if (resFilter.Count() > 0)
                                        {
                                            if(isDuplicateDb)
                                            {
                                                // set message service result
                                                serviceResult.MISACode = MISAEnum.NotValid;
                                                serviceResult.Messenger = $"{valueResource} {itemCode} đã trùng lặp trong hệ thống";
                                                consoleMess.Add(serviceResult);
                                                isDuplicateDb = false;
                                            }
                                        }

                                        // add resource to object


                                        TestResult(objDataTable[i]);
                                    }
                                }

                                // validate datetime

                            }
                        }

                    }
                }

                var json = JsonSerializer.Serialize(objDataTable);

                return json;

            /*try
            {
               
            }
            catch (Exception ce)
            {
                return ce.ToString();
            }*/
        }

        private string TestResult(SortedList items)
        {
            foreach(var item in items)
            {
                if(item.GetType().GetProperty("Key").GetValue(item).ToString() == "ValidateResult")
                {
                    return "";
                }
            }

            return "";
        }

        /// <summary>
        ///  Get value in item sorted list
        /// </summary>
        /// <param name="item"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        /// PQ Huy (01.07.2021)
        private string getValueSortedList(SortedList item, string prop)
        {
            string value = "";

            foreach(var obj in item)
            {
                if (obj.GetType().GetProperty("Key").GetValue(obj).ToString() == $"{_tableName}Code")
                {
                    if (obj.GetType().GetProperty("Value").GetValue(obj) is not null or not "")
                    {
                        value = obj.GetType().GetProperty("Value").GetValue(obj).ToString();
                    }
                    break;
                }
            }

            return value;
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
                    break;
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
