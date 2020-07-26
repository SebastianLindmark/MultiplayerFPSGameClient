namespace dto
{
    public class PlayerIdentifier
    {
        private int _id;

        public PlayerIdentifier(int id)
        {
            this._id = id;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}