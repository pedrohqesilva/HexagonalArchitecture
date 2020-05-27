namespace Core.Domain.Events
{
    public class DomainNotification : Event
    {
        public string PropertyName { get; private set; }
        public string ErrorMessage { get; private set; }

        public DomainNotification(string key, string value)
        {
            PropertyName = key;
            ErrorMessage = value;
        }
    }
}