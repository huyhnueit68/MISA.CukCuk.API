using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Object chứa dữ liệu data
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Message thông báo trạng thái
        /// </summary>
        public string Messenger { get; set; }

        /// <summary>
        /// Define MISA code
        /// </summary>
        public MISAEnum MISACode { get; set; }
    }
}
