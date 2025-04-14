using System.Runtime.Serialization;

namespace GlobalBlue.Client.Business
{
    public class VATResponse : BaseResponse
    {
        [DataMember]
        public string? VatRate { get; set; }

        [DataMember]
        public string? Net { get; set; }

        [DataMember]
        public string? Gross { get; set; }

        [DataMember]
        public string? Vat { get; set; }
    }
}