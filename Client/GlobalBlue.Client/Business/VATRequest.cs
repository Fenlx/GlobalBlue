using System.Runtime.Serialization;

namespace GlobalBlue.Client.Business
{
    public class VATRequest : BaseRequest
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