using GlobalBlue.Client.Enums;
using System.Runtime.Serialization;

namespace GlobalBlue.Client
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public ResponseType ResponseType { get; set; }

        [DataMember]
        public ICollection<string> Messages { get; set; }
    }
}