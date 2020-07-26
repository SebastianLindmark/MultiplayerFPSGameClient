using System;
using System.Text;
using dto;
using util;

namespace Events
{
    public class JoinEvent : GameEvent
    {
        private readonly PlayerIdentifier playerIdentifier;
        private readonly string username;
        private readonly byte modeByte = 0;

        public JoinEvent(PlayerIdentifier playerIdentifier, string username)
        {
            this.playerIdentifier = playerIdentifier;
            this.username = username;
        }

        public byte[] Serialize()
        {
            byte[][] arr =
            {
                BitConverter.GetBytes(playerIdentifier.Id),
                new[] {modeByte},
                Encoding.UTF8.GetBytes(username)
            };

            var destination = new byte[(1 * 4) + 1 + 16];
            Util.CopyBytes(destination, arr);
            return destination;
        }
    }
}