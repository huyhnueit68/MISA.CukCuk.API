using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Service;

namespace MISA.ApplicationCore
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region DECLARE
        ICustomerRepository _customerRepository;
        #endregion

        #region Construct
        /// <summary>
        /// hàm khỏi tạo cho customer service
        /// </summary>
        /// <param name="customerRepository"></param>
        /// CreatedBy: PQ Huy (24.06.2021)
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #endregion 

        #region Method

        public override ServiceResult Insert(Customer customer)
        {
            var serviceResult = new ServiceResult();

            // validate data
            var isValid = true;

            // logic validate:
            if (isValid)
            {
                return base.Insert(customer);
            }
            else
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "",
                        msg = "Validate Error",
                        Code = "900"
                    },
                    userMsg = "Validate Error",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Validate Error";
                serviceResult.Data = msg;

                return serviceResult;
            }
        }

        /// <summary>
        /// Lấy ra khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="code">Mã khách hàng</param>
        /// <returns>Trả về khách hàng</returns>
        /// CreatedBy: PQ Huy (26.06.2021)
        public Customer GetCustomerByCode(string code)
        {
            var customer = _customerRepository.GetCustomerByCode(code);

            return customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerPaging(int pageNumber, int pageSize)
        {
            return _customerRepository.GetCustomerPaging(pageNumber, pageSize);
        }

        #endregion
    }
}
