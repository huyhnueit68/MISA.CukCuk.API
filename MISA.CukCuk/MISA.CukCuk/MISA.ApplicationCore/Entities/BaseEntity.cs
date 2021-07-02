using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }


    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedDate { get; set; }

        public string? ModifedBy { get; set; }

        public List<string> Status { get; set; }

        public ServiceResult ImportResult { get; set; }

    }
}
