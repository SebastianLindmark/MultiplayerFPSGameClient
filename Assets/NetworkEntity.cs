using dto;
using Events.Handlers;

public interface NetworkEntity
{
    PlayerIdentifier getId();
    void onEventReceive(EventHandler eventHandler);
}