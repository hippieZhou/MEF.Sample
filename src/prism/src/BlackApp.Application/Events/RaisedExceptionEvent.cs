using Prism.Events;
using System;

namespace BlackApp.Application.Events
{
    public class RaisedExceptionEvent : PubSubEvent<Exception>
    {
    }
}
