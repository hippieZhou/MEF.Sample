using Prism.Events;

namespace BlankApp.Infrastructure.Event
{
    public class MessageSentEvent : PubSubEvent<string>
    {
    }
}
