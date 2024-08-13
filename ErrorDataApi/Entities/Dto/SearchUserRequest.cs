using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class SearchUserRequest : BaseRequest
    {
        public List<User> UserDataList { get; set; }
    }
}

