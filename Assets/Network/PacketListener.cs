    namespace Network
    {
        public interface PacketListener
        {
            void onReceive(GamePacket packet);
        }
    }