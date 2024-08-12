using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class UserDataRequest: BaseRequest
    {
        public User[] UserDataArray { get; set; }
    }
}
