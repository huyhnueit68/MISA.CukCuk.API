using MISA.ApplicationCore.Entities;
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

        public ServiceResult Insert(Generic data)
        {
            return _baseRepository.Insert(data);
        }

        public ServiceResult Update(Guid id, Generic data)
        {
            return _baseRepository.Update(id, data);
        }

        public ServiceResult DeleteById(Guid id)
        {
            return _baseRepository.DeleteById(id);
        }
        #endregion
    }
}
