using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class AuthenticatedUser : BaseUser
    {
        public string Token { get; set; }
    }
}
