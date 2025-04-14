using GlobalBlue.Client.Business;

namespace GlobalBlue.Services.Contracts.Business
{
    public interface IBusinessService
    {
        VATResponse CalculateVat(VATRequest vatRequest);
    }
}