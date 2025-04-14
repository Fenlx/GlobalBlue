using GlobalBlue.Services.Contracts.Common;

namespace GlobalBlue.Services
{
    public class BaseService
    {
        protected ILogService Logger { get; }

        public BaseService(ILogService logger)
        {
            Logger = logger;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }
    }
}