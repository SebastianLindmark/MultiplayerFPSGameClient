using dto;

namespace Events
{
    public class JoinEvent : GameEvent
    {
        private PlayerIdentifier playerIdentifier;

        public JoinEvent(PlayerIdentifier playerIdentifier)
        {
            this.playerIdentifier = playerIdentifier;
        }

        public byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}