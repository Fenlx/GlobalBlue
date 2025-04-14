using GlobalBlue.Client;
using GlobalBlue.Services.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GlobalBlue.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ILogService Logger { get; }

        public BaseController(ILogService logService) 
        {
            Logger = logService;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                var response = codetoExecute.Invoke();
                Logger.Debug($"Response: {JsonConvert.SerializeObject(response)}");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        protected void LogRequest(BaseRequest request)
        {
            Logger.Debug($"Request: {JsonConvert.SerializeObject(request)}");
        }
    }
}