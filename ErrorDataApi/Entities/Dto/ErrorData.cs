using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ErrorData : BaseErrorData
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? DeviceClassName { get; set; }
        public string? Tag { get; set; }
    }
}
