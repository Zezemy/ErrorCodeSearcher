using ErrorDataApi.Context;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            if (req.ErrorDataList == null || !req.ErrorDataList.Any() || (req.ErrorDataList.Count == 1 && AreFieldsEmpty(req.ErrorDataList[0])) )
            {
                return new SearchResult()
                {
                    Data = _context.ErrorDatas.ToList()
                };
            }
            else
            {
                var errorCodeList = req.ErrorDataList.Select(x => x.Code);
                var errorDescriptionList = req.ErrorDataList.Select(x => x.Description);
                var errorDeviceClassNameList = req.ErrorDataList.Select(x => x.DeviceClassName);
                var errorTagList = req.ErrorDataList.Select(x => x.Tag);
                var resultData1 = _context.ErrorDatas.Where(x => errorCodeList.Contains(x.Code));
                var resultData2 = _context.ErrorDatas.Where(x => errorDescriptionList.Contains(x.Description));
                var resultData3 = _context.ErrorDatas.Where(x => errorDeviceClassNameList.Contains(x.DeviceClassName));
                var resultData4 = _context.ErrorDatas.Where(x => errorTagList.Contains(x.Tag));
                var result = resultData1.Union(resultData2).Union(resultData3).Union(resultData4);
                return new SearchResult()
                {
                    Data = result.ToList()
                };
            }

        }

        [HttpPost(Name = "AddAsync")]
        public async Task<ErrorDataResponse> AddAsync([FromBody] ErrorDataRequest req)
        {
            //errorDatas.Add(req.ErrorData);
            _context.ErrorDatas.Add(req.ErrorData);
            _context.SaveChanges();
            return new ErrorDataResponse()
            {
                ResponseCode = "0",
                ResponseDescription = "İşlem başarılı."
            };
        }

        [HttpPut(Name = "UpdateAsync")]
        public async Task<ErrorDataResponse> UpdateAsync([FromBody] ErrorDataRequest req)
        {
            var errorData = _context.ErrorDatas.Where(x => x.Id == req.ErrorData.Id).FirstOrDefault();
            if (errorData == null)
            {
                return new ErrorDataResponse()
                {
                    ResponseCode = "1",
                    ResponseDescription = $"{req.ErrorData.Code} kodlu hata tanımı bulunamadı."
                };
            }
            errorData.Description = req.ErrorData.Description;
            _context.SaveChanges();
            return new ErrorDataResponse()
            {
                ResponseCode = "0",
                ResponseDescription = "İşlem başarılı."
            };
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
            return new ErrorDataResponse()
            {
                ResponseCode = "0",
                ResponseDescription = "İşlem başarılı."
            };
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
    }
}
