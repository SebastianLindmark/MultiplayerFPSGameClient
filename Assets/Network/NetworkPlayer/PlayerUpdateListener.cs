using Events;
using Network.Events;

namespace NetworkPlayer
{
    public interface PlayerUpdateListener
    {
        GameEvent getPlayerUpdate();
    }
}