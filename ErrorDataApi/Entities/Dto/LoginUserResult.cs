using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class LoginUserResult : BaseResponse
    {
        public AuthenticatedUser User { get; set; }
    }
}
