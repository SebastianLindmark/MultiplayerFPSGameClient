    using dto;

    namespace Network
    {
        public interface PacketListener
        {
            void onReceive(Packet packet,PlayerIdentifier playerIdentifier);
        }
    }