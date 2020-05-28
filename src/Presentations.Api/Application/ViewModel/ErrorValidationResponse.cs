using System.Collections.Generic;

namespace Presentations.Api.Application.ViewModel
{
    public class ErrorValidationResponse
    {
        public IEnumerable<MessageValidationResponse> Erros { get; set; }
    }
}