namespace Helpers.Events
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T value);
    }
}