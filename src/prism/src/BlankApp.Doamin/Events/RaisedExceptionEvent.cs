using Prism.Events;
using System;

namespace BlankApp.Doamin.Events
{
    public class RaisedExceptionEvent : PubSubEvent<Exception>
    {
    }
}
