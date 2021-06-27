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

            // 1. validate field not null, trả về lỗi khi validate
            if (string.IsNullOrEmpty(customer.CustomerCode))
            {
                isValid = false;
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng không được phép để trống",
                        Code = "900"
                    },
                    userMsg = "Mã khách hàng không được phép để trống",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // 2. validate dupicate code
            var customerDuplicate = _customerRepository.GetCustomerByCode(customer.CustomerCode);
            if(customerDuplicate != null)
            {
                isValid = false;

                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng " + customer.CustomerCode + " đã tồn tại",
                        Code = "900"
                    },
                    userMsg = "Mã khách hàng " + customer.CustomerCode + " đã tồn tại",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng " + customer.CustomerCode + " đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }

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
        public IEnumerable<Customer> GetCustomerByCode(string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerPaging(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
