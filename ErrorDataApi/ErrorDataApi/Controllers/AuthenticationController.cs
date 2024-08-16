using Azure;
using Entities.Dto;
using ErrorDataApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ErrorDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiControllerBase
    {
        static List<User> users = new List<User>();

        private readonly ApplicationDbContext _context;

        public AuthenticationController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<BaseResponse> LoginUserAsync([FromBody] LoginUserRequest req)
        {
            IQueryable<User> result = _context.Users;
            if (req == null || AreFieldsEmpty(req))
            {
                return ReturnResponseMessage("1", "İşlem başarısız. Alanlar boş olamaz.");
            }
            User userInDb = _context.Users.Where(x => x.UserName == req.UserName).FirstOrDefault();
            if (userInDb == null)
            {
                return ReturnResponseMessage("2", $"{userInDb?.UserName} kullanıcı adlı kullanıcı bulunamadı.");
            }
            if (req.Password != userInDb.Password)
            {
                return ReturnResponseMessage("3", $"{userInDb?.UserName} kullanıcısının şifresi bulunamadı.");
            }

            AuthenticatedUser authUser = new AuthenticatedUser();
            authUser.Id = userInDb.Id;
            authUser.Token = $"{userInDb.Id}{userInDb.UserName}{userInDb.UserType}{userInDb.CreatedBy}{userInDb.CreateDate}{userInDb.UpdatedBy}{userInDb.UpdateDate}";
            authUser.UserName = userInDb.UserName;
            authUser.UserType = userInDb.UserType;
            authUser.CreatedBy = userInDb.CreatedBy;
            authUser.CreateDate = userInDb.CreateDate;
            authUser.UpdatedBy = userInDb.UpdatedBy;
            authUser.UpdateDate = userInDb.UpdateDate;

            var ret = new LoginUserResult();
            ret.User = authUser;
            ret.ResponseCode = "0";
            ret.ResponseDescription = "İşlem başarılı.";

            return ret;
        }

        private bool AreFieldsEmpty(LoginUserRequest request)
        {
            var isUserNameEmpty = string.IsNullOrWhiteSpace(request.UserName);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(request.Password);
            return isUserNameEmpty && isPasswordEmpty;
        }
    }
}
