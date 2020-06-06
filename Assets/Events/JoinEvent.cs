using System;
using dto;
using util;

namespace Events
{
    public class JoinEvent : GameEvent
    {
        private PlayerIdentifier playerIdentifier;
        private readonly byte modeByte = 0;
        public JoinEvent(PlayerIdentifier playerIdentifier)
        {
            this.playerIdentifier = playerIdentifier;
        }

        public byte[] Serialize()
        {
            byte[][] arr =
            {
                BitConverter.GetBytes(playerIdentifier.Id),
                new[]{modeByte},
            };

            var destination = new byte[1 + 1*4];
            Util.CopyBytes(destination, arr);
            return destination;
        }
    }
}