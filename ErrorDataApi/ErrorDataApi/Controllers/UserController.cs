using Azure;
using Entities.Dto;
using ErrorDataApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<UserSearchResponse> GetByIdAsync(long id)
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
        public async Task<BaseResponse> GetByUserNameAsync(string? userName)
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
        public async Task<SearchUserResult> SearchUserAsync([FromBody] SearchUserRequest req)
        {
            IQueryable<User> result = _context.Users;
            if (req.UserDataList == null || !req.UserDataList.Any() || (req.UserDataList.Count == 1 && AreFieldsEmpty(req.UserDataList[0])))
            {
                return new SearchUserResult()
                {
                    Data = result.ToList()
                };
            }
            else
            {
                if (req.UserDataList.Count == 1)
                {
                    result = FilterByUserData(req);
                    return ReturnSearchResult(result);
                }

                result = FilterByUserNameList(req, result);
                result = FilterByPasswordList(req, result);
                result = FilterByUserTypeList(req, result);

                return ReturnSearchResult(result);
            }
        }

        private IQueryable<User> FilterByUserNameList(SearchUserRequest req, IQueryable<User> users)
        {
            var list = req.UserDataList.Where(x => !string.IsNullOrWhiteSpace(x.UserName)).Select(x => x.UserName);
            return users.Where(x => list.Contains(x.UserName));

        }
        private IQueryable<User> FilterByPasswordList(SearchUserRequest req, IQueryable<User> users)
        {
            var list = req.UserDataList.Where(x => !string.IsNullOrWhiteSpace(x.Password)).Select(x => x.Password);
            return users.Where(x => list.Contains(x.Password));
        }

        private IQueryable<User> FilterByUserTypeList(SearchUserRequest req, IQueryable<User> users)
        {
            var list = req.UserDataList.Where(x => x.UserType > 0).Select(x => x.UserType);
            return users.Where(x => list.Contains(x.UserType));
        }
        private IQueryable<User> FilterByUserData(SearchUserRequest req)
        {
            IQueryable<User> result = _context.Users;
            var userName = req.UserDataList[0].UserName;
            var password = req.UserDataList[0].Password;
            var userType = req.UserDataList[0].UserType;

            if (!string.IsNullOrEmpty(userName))
            {
                return _context.Users.Where(x => x.UserName == userName);
            }

            if (!string.IsNullOrEmpty(password))
            {
                return _context.Users.Where(x => x.Password == password);
            }

            if (userType > 0)
            {
                return _context.Users.Where(x => x.UserType == userType);
            }
            return result;
        }

        private static SearchUserResult ReturnSearchResult(IQueryable<User> result)
        {
            return new SearchUserResult()
            {
                Data = result.ToList()
            };
        }
        private bool AreFieldsEmpty(User data)
        {
            var isUserNameEmpty = string.IsNullOrWhiteSpace(data.UserName);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(data.Password);
            var isUserTypeValid = data.UserType > 0;
            return isUserNameEmpty && isPasswordEmpty && !isUserTypeValid;
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
