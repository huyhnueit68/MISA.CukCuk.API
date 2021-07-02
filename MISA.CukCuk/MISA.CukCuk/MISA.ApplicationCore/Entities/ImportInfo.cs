using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    class ImportInfo
    {
        public ImportInfo(string importKey, object data)
        {
            ImportKey = importKey;
            ImportData = data;
        }
        public string ImportKey { get; set; }
        public object ImportData { get; set; }
    }
}
