using MISA.Infrastructure.Model;
using MISA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;
using MISA.Entity;

namespace MISA.ApplicationCore
{
    public class CustomerService
    {
        #region Method
        /// <summary>
        /// Lấy danh sách khàng
        /// </summary>
        /// <returns>Trả về danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public IEnumerable<Customer> GetCustomers()
        {
            var customerContext = new CustomerContext();
            var customers = customerContext.GetCustomers();

            return customers;
        }

        /// <summary>
        /// Lấy mã khách hàng theo id
        /// </summary>
        ///  <param name="id">Mã khách hàng</param>
        /// <returns> Trả về khách hàng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public IEnumerable<Customer> GetCustomerById(Guid id)
        {
            var customerContext = new CustomerContext();
            var serviceResult = new ServiceResult();
            var customers = customerContext.GetCustomersById(id);

            return customers;
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Dữ liệu khách hàng</param>
        /// <returns>Khách hàng vừa thêm thành cồng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public ServiceResult InsertCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            var customerContext = new CustomerContext();

            // validate data
            // validate field not null, trả về lỗi khi validate
            if (string.IsNullOrEmpty(customer.CustomerCode))
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "",
                        Code = "900"
                    },
                    userMsg = "Mã khách hàng không được phép để trống",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;

                return serviceResult;
            } 

            // validate duplicate code
            var res = customerContext.GetCustomerByCode(customer.CustomerCode);
            if (res != null)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng đã tồn tại",
                        Code = "900"
                    },
                    userMsg = "Mã khách hàng đã tồn tại",
                };
                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // validate customer group id
            var resCustomerGroup = customerContext.GetCustomerGroupById((Guid)customer.CustomerGroupId);
            if (resCustomerGroup == null)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerGroupId",
                        msg = "Không tồn tại mã nhóm",
                        Code = "401"
                    },
                    userMsg = "Không tồn tại mã nhóm",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // validate phone number
            var resPhoneNumber = customerContext.GetCustomerGroupByPhone(customer.PhoneNumber);
            if (resPhoneNumber != null)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "PhoneNumber",
                        msg = "Số điện thoại đã tồn tại",
                        Code = "401"
                    },
                    userMsg = "Số điện thoại đã tồn tại",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Số điện thoại đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // validate email
            var resEmail = customerContext.GetCustomerByEmail(customer.Email);
            if (resEmail != null)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "Email",
                        msg = "Email đã tồn tại",
                        Code = "401"
                    },
                    userMsg = "Email đã tồn tại",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Email đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }


            // thêm mới khi validate thành công
            var rowAffects = customerContext.InsertCustomer(customer);

            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Thêm dữ liệu thành công";
            serviceResult.Data = rowAffects;

            return serviceResult;
        }

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="id">Mã khách hàng cần sửa</param>
        /// <param name="customer">Thông tin sửa khách hàng</param>
        /// <returns>Trả về trạng thái bản ghi cập nhật</returns>
        public ServiceResult UpdateCustomer(Guid id, Customer customer)
        {
            var serviceResult = new ServiceResult();
            var customerContext = new CustomerContext();

            // validate dữ liệu
            var oldCustomerCode = customerContext.GetCustomersById(id);

            //validate code
            if (oldCustomerCode.ToArray()[0].CustomerCode != customer.CustomerCode)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Vui lòng không thay đổi mã khách hàng",
                        Code = "401"
                    },
                    userMsg = "Vui lòng không thay đổi mã khách hàng",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Vui lòng không thay đổi mã khách hàng";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // validate customer group id
            var resCustomerGroup = customerContext.GetCustomerGroupById((Guid)customer.CustomerGroupId);
            if (resCustomerGroup == null)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerGroupId",
                        msg = "Không tồn tại mã nhóm",
                        Code = "401"
                    },
                    userMsg = "Không tồn tại mã nhóm",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Mã khách hàng đã tồn tại";
                serviceResult.Data = msg;

                return serviceResult;
            }

            // validate phone number
            if(oldCustomerCode.ToArray()[0].PhoneNumber != customer.PhoneNumber)
            {
                var resPhoneNumber = customerContext.GetCustomerGroupByPhone(customer.PhoneNumber);
                if (resPhoneNumber != null)
                {
                    var msg = new
                    {
                        devMsg = new
                        {
                            fieldName = "PhoneNumber",
                            msg = "Số điện thoại đã tồn tại",
                            Code = "401"
                        },
                        userMsg = "Số điện thoại đã tồn tại",
                    };

                    serviceResult.MISACode = MISAEnum.NotValid;
                    serviceResult.Messenger = "Số điện thoại đã tồn tại";
                    serviceResult.Data = msg;

                    return serviceResult;
                }
            }
            

            // validate email
            if(oldCustomerCode.ToArray()[0].Email != customer.Email) {
                var resEmail = customerContext.GetCustomerByEmail(customer.Email);
                if (resEmail != null)
                {
                    var msg = new
                    {
                        devMsg = new
                        {
                            fieldName = "Email",
                            msg = "Email đã tồn tại",
                            Code = "401"
                        },
                        userMsg = "Email đã tồn tại",
                    };

                    serviceResult.MISACode = MISAEnum.NotValid;
                    serviceResult.Messenger = "Email đã tồn tại";
                    serviceResult.Data = msg;

                    return serviceResult;
                }
            }
            

            //cập nhật dữ liệu khi validate thành công
            var rowAffects = customerContext.UpdateCustomer(id, customer);

            if (rowAffects > 0)
            {
                serviceResult.MISACode = MISAEnum.IsValid;
                serviceResult.Messenger = "Cập nhật dữ liệu thành công";
                serviceResult.Data = rowAffects;
            }
            else
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = " ",
                        msg = "Đã có lỗi xảy ra, vui lòng thử lại sau",
                        Code = "501"
                    },
                    userMsg = "VĐã có lỗi xảy ra, vui lòng thử lại sau",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Đã có lỗi xảy ra, vui lòng thử lại sau";
                serviceResult.Data = msg;
            }

            return serviceResult;
        }

        /// <summary>
        /// Xóa thông tin khách hàng
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <returns>Trả về trạng thái</returns>
        public ServiceResult DeleteCustomerById(Guid id)
        {
            var customerContext = new CustomerContext();
            var serviceResult = new ServiceResult();

            var rowAffects = customerContext.DeleteCustomerById(id);

            if(rowAffects > 0)
            {
                serviceResult.MISACode = MISAEnum.IsValid;
                serviceResult.Messenger = "Xóa bản ghi thành công!";
                serviceResult.Data = rowAffects;

                return serviceResult;
            } else
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "",
                        msg = "Xóa dữ liệu thất bại, vui lòng thử lại sau",
                        Code = "500"
                    },
                    userMsg = "Xóa dữ liệu thất bại, vui lòng thử lại sau",
                };

                serviceResult.MISACode = MISAEnum.NotValid;
                serviceResult.Messenger = "Xóa dữ liệu thất bại, vui lòng thử lại sau";
                serviceResult.Data = msg;

                return serviceResult;
            }
        }
        #endregion
    }
}
