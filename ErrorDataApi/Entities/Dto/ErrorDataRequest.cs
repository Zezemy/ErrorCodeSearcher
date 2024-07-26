using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class ErrorDataRequest: BaseRequest
    {
        public ErrorData[] ErrorDataArray { get; set; }
    }
}
