namespace Presentations.Api.Application.ViewModel
{
    public class MessageValidationResponse
    {
        public string PropertyName { get; private set; }
        public string ErrorMessage { get; private set; }

        public MessageValidationResponse(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}