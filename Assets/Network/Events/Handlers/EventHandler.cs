using dto;
using Game.GameEntity;

namespace Network.Events.Handlers
{
    public interface EventHandler
    {
        void Execute(Player player);

        PlayerIdentifier GetPlayerIdentifier();
    }
}