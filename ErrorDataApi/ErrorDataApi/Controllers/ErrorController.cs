using ErrorDataApi.Context;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static Entities.Dto.Enums;

namespace ErrorDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ErrorController : ControllerBase
    {
        static List<ErrorData> errorDatas = new List<ErrorData>();

        private readonly ApplicationDbContext _context;

        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetErrors")]
        public async Task<SearchResult> GetAsync(string? errorCode, string? errorDescription)
        {
            try
            {
                if (errorCode == null && errorDescription == null)
                {
                    return new SearchResult()
                    {
                        Data = _context.ErrorDatas.ToList()
                    };
                }
                else
                {
                    var resultData1 = _context.ErrorDatas.Where(x => errorCode == x.Code).ToList();
                    var resultData2 = _context.ErrorDatas.Where(x => errorDescription == x.Description).ToList();
                    var result = resultData1?.Union(resultData2 ?? new List<ErrorData>());
                    return new SearchResult()
                    {
                        Data = result.ToList()
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [HttpPost(Name = "SearchErrors")]
        public async Task<SearchResult> SearchAsync([FromBody] SearchErrorRequest req)
        {
            IQueryable<ErrorData> result = _context.ErrorDatas;
            if (req.ErrorDataList == null || !req.ErrorDataList.Any() || (req.ErrorDataList.Count == 1 && AreFieldsEmpty(req.ErrorDataList[0])))
            {
                return new SearchResult()
                {
                    Data = result.ToList()
                };
            }
            else
            {

                if (req.ErrorDataList.Count == 1)
                {
                    result = FilterByErrorData(req);
                    return ReturnSearhResult(result);
                }

                result = FilterByCodeList(req, result);
                result = FilterByDescriptionList(req, result);
                result = FilterByCategoryList(req, result);
                result = FilterByDeviceClassNameList(req, result);
                result = FilterByTagList(req, result);

                return ReturnSearhResult(result);

            }

        }

        private IQueryable<ErrorData> FilterByCodeList(SearchErrorRequest req, IQueryable<ErrorData> errorDatas)
        {
            var list = req.ErrorDataList.Where(x => !string.IsNullOrWhiteSpace(x.Code)).Select(x => x.Code);
            return errorDatas.Where(x => list.Contains(x.Code));

        }
        private IQueryable<ErrorData> FilterByDescriptionList(SearchErrorRequest req, IQueryable<ErrorData> errorDatas)
        {
            var list = req.ErrorDataList.Where(x => !string.IsNullOrWhiteSpace(x.Description)).Select(x => x.Description);
            return errorDatas.Where(x => list.Contains(x.Description));

        }

        private IQueryable<ErrorData> FilterByCategoryList(SearchErrorRequest req, IQueryable<ErrorData> errorDatas)
        {
            var list = req.ErrorDataList.Where(x => !string.IsNullOrWhiteSpace(x.Category)).Select(x => x.Category);
            return errorDatas.Where(x => list.Contains(x.Category));
        }
        private IQueryable<ErrorData> FilterByDeviceClassNameList(SearchErrorRequest req, IQueryable<ErrorData> errorDatas)
        {
            var list = req.ErrorDataList.Where(x => !string.IsNullOrWhiteSpace(x.DeviceClassName)).Select(x => x.DeviceClassName);
            return errorDatas.Where(x => list.Contains(x.DeviceClassName));

        }

        private IQueryable<ErrorData> FilterByTagList(SearchErrorRequest req, IQueryable<ErrorData> errorDatas)
        {
            var list = req.ErrorDataList.Where(x => !string.IsNullOrWhiteSpace(x.Tag)).Select(x => x.Tag);
            return errorDatas.Where(x => list.Contains(x.Tag));

        }

        private IQueryable<ErrorData> FilterByErrorData(SearchErrorRequest req)
        {
            IQueryable<ErrorData> result = _context.ErrorDatas;
            var code = req.ErrorDataList[0].Code;
            var description = req.ErrorDataList[0].Description;
            var category = req.ErrorDataList[0].Category;
            var deviceClassName = req.ErrorDataList[0].DeviceClassName;
            var tag = req.ErrorDataList[0].Tag;
            if (!string.IsNullOrEmpty(code))
            {
                return _context.ErrorDatas.Where(x => x.Code == code);
            }

            if (!string.IsNullOrEmpty(description))
            {
                return _context.ErrorDatas.Where(x => x.Description == description);
            }
            if (!string.IsNullOrEmpty(category))
            {
                return _context.ErrorDatas.Where(x => x.Category == category);
            }

            if (!string.IsNullOrEmpty(deviceClassName))
            {
                return _context.ErrorDatas.Where(x => x.DeviceClassName == deviceClassName);
            }

            if (!string.IsNullOrEmpty(tag))
            {
                return _context.ErrorDatas.Where(x => x.Tag == tag);
            }

            return result;
        }

        private static SearchResult ReturnSearhResult(IQueryable<ErrorData> result)
        {
            return new SearchResult()
            {
                Data = result.ToList()
            };
        }

        [HttpPost(Name = "AddAsync")]
        public async Task<ErrorDataResponse> AddAsync([FromBody] ErrorDataRequest req)
        {
            if (req.ErrorDataArray.Any(x => AnyFieldsEmpty(x)))
            {
                return ReturnResponseMessage("1", "İşlem başarısız. Alanlar boş olamaz.");
            }
            _context.ErrorDatas.AddRange(req.ErrorDataArray);
            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }

        private static ErrorDataResponse ReturnResponseMessage(string code, string description)
        {
            return new ErrorDataResponse()
            {
                ResponseCode = code,
                ResponseDescription = description
            };
        }


        [HttpPut(Name = "UpdateAsync")]
        public async Task<ErrorDataResponse> UpdateAsync([FromBody] ErrorDataRequest req)
        {
            foreach (var errorDataFromRequest in req.ErrorDataArray)
            {
                var errorData = _context.ErrorDatas.Where(x => x.Id == errorDataFromRequest.Id).FirstOrDefault();
                if (errorData == null)
                {
                    return ReturnResponseMessage("2", $"{errorData.Code} kodlu hata tanımı bulunamadı.");
                }
                if (AnyFieldsEmpty(errorData))
                {
                    return ReturnResponseMessage("1", "İşlem başarısız. Alanlar boş olamaz.");
                }
                var supportedDevices = Enum.GetNames<DeviceClasses>().ToList();
                supportedDevices.Add("XFSGeneral");
                if (supportedDevices.Contains(errorDataFromRequest.DeviceClassName))
                {
                    errorData.Description = errorDataFromRequest.Description;
                    errorData.Code = errorDataFromRequest.Code;
                    errorData.Category = errorDataFromRequest.Category;
                    errorData.DeviceClassName = errorDataFromRequest.DeviceClassName;
                    errorData.Tag = errorDataFromRequest.Tag;
                }
                else
                    return ReturnResponseMessage("3", "İşlem başarısız. Desteklenmeyen cihaz tipi.");
            }

            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }

        [HttpDelete(Name = "DeleteAsync")]
        public async Task<ErrorDataResponse> DeleteAsync(long id)
        {
            var errorData = _context.ErrorDatas.Where(x => x.Id == id).FirstOrDefault();
            if (errorData == null)
            {
                return new ErrorDataResponse()
                {
                    ResponseCode = "1",
                    ResponseDescription = $"{id} numaralı tanım bulunamadı."
                };
            }
            _context.ErrorDatas.Remove(errorData);
            _context.SaveChanges();
            return ReturnResponseMessage("0", "İşlem başarılı.");
        }
        private bool AreFieldsEmpty(ErrorData data)
        {
            var isDeviceClassNameEmpty = string.IsNullOrWhiteSpace(data.DeviceClassName);
            var isErrorCodeEmpty = string.IsNullOrWhiteSpace(data.Code);
            var isDescriptionEmpty = string.IsNullOrWhiteSpace(data.Description);
            var isCategoryEmpty = string.IsNullOrWhiteSpace(data.Category);
            var isTagEmpty = string.IsNullOrWhiteSpace(data.Tag);
            return isDeviceClassNameEmpty && isErrorCodeEmpty && isDescriptionEmpty && isCategoryEmpty && isTagEmpty;
        }

        private bool AnyFieldsEmpty(ErrorData data)
        {
            var isDeviceClassNameEmpty = string.IsNullOrWhiteSpace(data.DeviceClassName);
            var isErrorCodeEmpty = string.IsNullOrWhiteSpace(data.Code);
            var isDescriptionEmpty = string.IsNullOrWhiteSpace(data.Description);
            var isCategoryEmpty = string.IsNullOrWhiteSpace(data.Category);
            return isDeviceClassNameEmpty || isErrorCodeEmpty || isDescriptionEmpty || isCategoryEmpty;
        }
    }
}
