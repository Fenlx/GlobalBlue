using GlobalBlue.Client.Business;
using GlobalBlue.Client.Common;
using GlobalBlue.Services.Contracts.Business;
using GlobalBlue.Services.Contracts.Common;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GlobalBlue.Services.Business
{
    public class BusinessService : BaseService, IBusinessService
    {
        private readonly string _patternValidCharacters = @"[0-9]{1,}((\.|,)[0-9]){0,}";

        public BusinessService(ILogService logger) : base(logger)
        {
        }

        public VATResponse CalculateVat(VATRequest vatRequest)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var response = new VATResponse()
                {
                    Messages = new List<string>()
                };
                if (MoreThanOneInput(vatRequest, response) || MissingOrInvalidAmountInput(vatRequest, response) || MissingOrInvalidVatRate(vatRequest, response))
                {
                    return response;
                }
                CalculateMissingValues(vatRequest, response);
                return response;
            });
        }

        private bool MissingOrInvalidAmountInput(VATRequest vatRequest, VATResponse vatResponse)
        {
            var result = false;
            var regex = new Regex(_patternValidCharacters);
            if ((!string.IsNullOrEmpty(vatRequest?.Vat) && !regex.IsMatch(vatRequest.Vat)) ||
                (!string.IsNullOrEmpty(vatRequest?.Gross) && !regex.IsMatch(vatRequest.Gross)) ||
                (!string.IsNullOrEmpty(vatRequest?.Net) && !regex.IsMatch(vatRequest.Net)))
            {
                result = true;
                vatResponse.ResponseType = Client.Enums.ResponseType.Error;
                vatResponse.Messages.Add(VatCalculationErrorMessages.MissingOrInvalidInput);
            }
            return result;
        }

        private bool MoreThanOneInput(VATRequest vatRequest, VATResponse vatResponse)
        {
            var result = false;
            if ((!string.IsNullOrEmpty(vatRequest?.Vat) && !string.IsNullOrEmpty(vatRequest?.Gross)) ||
                (!string.IsNullOrEmpty(vatRequest?.Vat) && !string.IsNullOrEmpty(vatRequest?.Net)) ||
                (!string.IsNullOrEmpty(vatRequest?.Gross) && !string.IsNullOrEmpty(vatRequest?.Net)))
            {
                result = true;
                vatResponse.ResponseType = Client.Enums.ResponseType.Error;
                vatResponse.Messages.Add(VatCalculationErrorMessages.MoreThanOneInput);
            }
            return result;
        }

        private bool MissingOrInvalidVatRate(VATRequest vatRequest, VATResponse vatResponse)
        {
            var result = false;
            if (string.IsNullOrEmpty(vatRequest?.VatRate))
            {
                result = true;
                vatResponse.ResponseType = Client.Enums.ResponseType.Error;
                vatResponse.Messages.Add(VatCalculationErrorMessages.MissingOrInvalidRateInput);
            } 
            else
            {
                var vatRate = vatRequest.VatRate.Replace("%", string.Empty);
                var regex = new Regex(_patternValidCharacters);
                if (!regex.IsMatch(vatRate))
                {
                    result = true;
                    vatResponse.ResponseType = Client.Enums.ResponseType.Error;
                    vatResponse.Messages.Add(VatCalculationErrorMessages.MissingOrInvalidRateInput);
                }
                var ptCulture = new CultureInfo("pt-PT");
                var usCulture = new CultureInfo("en-US");
                result = !decimal.TryParse(vatRate, usCulture, out decimal usRateValue) && !decimal.TryParse(vatRate, ptCulture, out decimal ptRateValue);
                if (result)
                {
                    result = true;
                    vatResponse.ResponseType = Client.Enums.ResponseType.Error;
                    vatResponse.Messages.Add(VatCalculationErrorMessages.MissingOrInvalidRateInput);
                }
            }
            return result;
        }

        private decimal GetVatRate(VATRequest vatRequest)
        {
            var vatRate = vatRequest.VatRate.Replace("%", string.Empty);
            var usRateValue = ParseStringToValue(vatRate);
            var ptRateValue = ParseStringToValue(vatRate);
            if (usRateValue > 0)
            {
                return usRateValue;
            } 
            else
            {
                return ptRateValue;
            }
        }

        private decimal ParseStringToValue(string initialValue)
        {
            var ptCulture = new CultureInfo("pt-PT");
            var usCulture = new CultureInfo("en-US");
            decimal.TryParse(initialValue, usCulture, out decimal usRateValue);
            decimal.TryParse(initialValue, ptCulture, out decimal ptRateValue);
            if (usRateValue > 0)
            {
                return usRateValue;
            }
            else
            {
                return ptRateValue;
            }
        }

        private string DecimalToString(decimal value)
        {
            return value.ToString("0.00");
        }

        private void CalculateMissingValues(VATRequest vatRequest, VATResponse vatResponse)
        {
            var rateValue = GetVatRate(vatRequest);
            var gross = 0.0M;
            var net = 0.0M;
            var vat = 0.0M;
            if (!string.IsNullOrEmpty(vatRequest?.Vat))
            {
                vat = ParseStringToValue(vatRequest.Vat);
                gross = 100 * vat / rateValue;
                net = gross + vat;
            } 
            else if (!string.IsNullOrEmpty(vatRequest?.Gross))
            {
                gross = ParseStringToValue(vatRequest.Gross);
                vat = gross * (rateValue / 100);
                net = gross + vat;
            }
            else
            {
                net = ParseStringToValue(vatRequest.Net);
                vat = rateValue * net / (100 + rateValue);
                gross = 100 * vat / rateValue;
            }
            vatResponse.Net = DecimalToString(net);
            vatResponse.Gross = DecimalToString(gross);
            vatResponse.Vat = DecimalToString(vat);
            vatResponse.VatRate = DecimalToString(rateValue);
            vatResponse.ResponseType = Client.Enums.ResponseType.Ok;
        }
    }
}