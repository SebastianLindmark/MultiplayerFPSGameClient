using dto;

namespace NetworkPlayer
{
    public class PlayerAttribute
    {
        private Position position;
        
        public Position Position
        {
            get => position;
            set => position = value;
        }

        
        
        public PlayerAttribute()
        {
        }

        public void AddFiring()
        {
            //Add to list of firings
        }

    }
}