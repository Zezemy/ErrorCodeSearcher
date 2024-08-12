using Azure;
using Entities.Dto;
using ErrorDataApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Entities.Dto.Enums;

namespace ErrorDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ApiControllerBase
    {
        static List<User> users = new List<User>();

        private readonly ApplicationDbContext _context;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<UserSearchResponse> GetAsyncById(long id)
        {
            try
            {
                if (id == 0)
                {
                    return new UserSearchResponse { Users = _context.Users.ToArray() };
                }
                else
                {
                    var result = _context.Users.Where(x => id == x.Id).ToArray();
                    var response = new UserSearchResponse { Users = result };
                    FillSuccessMessage(response, "0", "İşlem başarılı.");
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<BaseResponse> GetAsyncByUserName(string? userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return ReturnResponseMessage("1", "İşlem başarısız. UserName boş olamaz.");
                }
                else
                {
                    var result = _context.Users.Where(x => userName == x.UserName).ToArray();
                    var response = new UserSearchResponse { Users = result };
                    FillSuccessMessage(response, "0", "İşlem başarılı.");
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<BaseResponse> AddUserAsync([FromBody] UserDataRequest req)
        {
            if (req.UserDataArray.Any(x => AnyFieldsEmpty(x)))
            {
                return ReturnResponseMessage("1", "İşlem başarısız. Alanlar boş olamaz.");
            }
            _context.Users.AddRange(req.UserDataArray);
            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }
        private bool AnyFieldsEmpty(User data)
        {
            var isUserNameEmpty = string.IsNullOrWhiteSpace(data.UserName);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(data.Password);
            var isUserTypeValid = data.UserType > 0;
            return isUserNameEmpty || isPasswordEmpty || !isUserTypeValid;
        }


        [HttpPut]
        public async Task<BaseResponse> UpdateUserAsync([FromBody] UserDataRequest req)
        {
            foreach (var userDataFromRequest in req.UserDataArray)
            {
                var userData = _context.Users.Where(x => x.Id == userDataFromRequest.Id).FirstOrDefault();
                var response = new BaseResponse();
                var isValid = ValidateUpdateUserData(userData, response);
                if (!isValid)
                {
                    return response;
                }
                userData.UserName = userDataFromRequest.UserName;
                userData.Password = userDataFromRequest.Password;
                userData.UserType = userDataFromRequest.UserType;
            }
            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }

        private bool ValidateUpdateUserData(User? userData, BaseResponse response)
        {
            if (userData == null)
            {
                response = ReturnResponseMessage("2", $"{userData?.Id} id'li kullanıcı bulunamadı.");
                return false;
            }
            if (AnyFieldsEmpty(userData))
            {
                response = ReturnResponseMessage("1", "İşlem başarısız. Alanlar boş olamaz.");
                return false;
            }
            return true;
        }


        [HttpDelete]
        public async Task<BaseResponse> DeleteUserAsync(long id)
        {
            var userData = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (userData == null)
            {
                return new UserDataResponse()
                {
                    ResponseCode = "1",
                    ResponseDescription = $"{id} numaralı kullanıcı bulunamadı."
                };
            }
            _context.Users.Remove(userData);
            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }
    }
}
