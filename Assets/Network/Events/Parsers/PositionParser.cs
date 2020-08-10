using System;
using dto;
using Events;

namespace Network.Events.Parsers
{
    public static class PositionParser
    {
        public static PositionEvent Parse(byte[] packetData, int startIndex)
        {
            int playerId = BitConverter.ToInt32(packetData, startIndex);
            float positionX = BitConverter.ToSingle(packetData, startIndex + 4);
            float positionY = BitConverter.ToSingle(packetData, startIndex + 8);
            float positionZ = BitConverter.ToSingle(packetData, startIndex + 12);
            float rotation = BitConverter.ToSingle(packetData, startIndex + 16);
            
            var position = new Position(positionX, positionY, positionZ, rotation);
            return new PositionEvent(new PlayerIdentifier(playerId), position);
        }
    }
}