namespace BlankApp.Doamin.Bus
{
    public interface IEventBus
    {
        void Sned<PubSubEvent>(object payload);
    }
}
