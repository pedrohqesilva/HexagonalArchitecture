namespace Presentations.Api.Application.ViewModel
{
    public class ExeceptionResponse
    {
        public ExeceptionResponse()
        {
        }

        public ExeceptionResponse(string message)
        {
            Message = message;
        }

        public ExeceptionResponse(string message, string stackTrace) : this(message)
        {
            StackTrace = stackTrace;
        }

        public ExeceptionResponse(string message, string stackTrace, string innerMessage) : this(message, stackTrace)
        {
            InnerMessage = innerMessage;
        }

        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return $"Message: {Message} - InnerMessage: {InnerMessage} - StackTrace: {StackTrace}";
        }
    }
}