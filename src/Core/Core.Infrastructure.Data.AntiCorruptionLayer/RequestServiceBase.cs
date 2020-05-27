using Core.Infrastructure.Data.AntiCorruptionLayer.Interfaces;
using TCE.RestClient;

namespace Core.Infrastructure.Data.AntiCorruptionLayer
{
    public class RequestServiceBase : RestClient, IRequestServiceBase
    {
        public RequestServiceBase(string urlBase) : base(urlBase)
        {
        }
    }
}