using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    public class BaseService<Generic> : IBaseService<Generic>
    {
        #region DECLARE
        IBaseRepository<Generic> _baseRepository;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<Generic> baseRepository)
        {
            _baseRepository = baseRepository;
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
            var serviceResult = new ServiceResult();

            // validate require
            var isValid = Validate(data);

            if(isValid)
            {
                return _baseRepository.Insert(data);
            } else
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Validate error",
                        Code = "900"
                    },
                    userMsg = "Validate error",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Validate error";
                serviceResult.Data = msg;

                return serviceResult;
            }
        }

        public ServiceResult Update(Guid id, Generic data)
        {
            return _baseRepository.Update(id, data);
        }

        public ServiceResult DeleteById(Guid id)
        {
            return _baseRepository.DeleteById(id);
        }

        private bool Validate(Generic data)
        {
            var isValid = true; 
            // Get all property:
            var properties = data.GetType().GetProperties();

            foreach(var property in properties)
            {
                // check attribute need validate
                if(property.IsDefined(typeof(Required), false))
                {
                    // check required
                    var propertyValue = property.GetValue(data);
                    if(propertyValue == null)
                    {
                        isValid = false;
                    }
                } else if ( property.IsDefined(typeof(CheckDuplicate), false))
                {
                    // check duplicate data
                    var valueDuplicate = _baseRepository.GetEntityByProperty(property.Name, property.GetValue(data));
                    if(valueDuplicate != null)
                    {
                        isValid = false;
                    } 
                }
            }

            return isValid;

        }
        #endregion
    }
}
