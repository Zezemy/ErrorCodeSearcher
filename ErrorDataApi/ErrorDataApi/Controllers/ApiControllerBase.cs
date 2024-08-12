using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorDataApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected static BaseResponse ReturnResponseMessage(string code, string description)
        {
            return new BaseResponse()
            {
                ResponseCode = code,
                ResponseDescription = description
            };
        }

        protected static void FillSuccessMessage(BaseResponse responseToFill, string code, string description)
        {
            responseToFill.ResponseCode = code;
            responseToFill.ResponseDescription = description;
        }
    }
}
