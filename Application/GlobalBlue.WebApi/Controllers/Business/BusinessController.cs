using GlobalBlue.Client.Business;
using GlobalBlue.Services.Contracts.Business;
using GlobalBlue.Services.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue.WebApi.Controllers.Business
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService, ILogService logService) : base(logService)
        {
            _businessService = businessService;
        }

        [HttpPost]
        public VATResponse CalculateVat([FromBody] VATRequest vatRequest)
        {
            LogRequest(vatRequest);
            return ExecuteFaultHandledOperation(() => _businessService.CalculateVat(vatRequest));
        }
    }
}