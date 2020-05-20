    namespace Network
    {
        public interface PacketListener
        {
            void onReceive(Packet packet);
        }
    }