using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class AuthLoginUserResult : BaseResponse
    {
        public AuthenticatedUser User { get; set; }
    }
}
