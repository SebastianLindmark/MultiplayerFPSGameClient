using Events;

namespace NetworkPlayer
{
    public interface PlayerUpdateListener
    {
        GameEvent getPlayerUpdate();
    }
}