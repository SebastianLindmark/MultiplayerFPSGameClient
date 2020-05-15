using System;
using dto;
using util;

namespace Events.Parsers
{
    public class PlayerIdentifierParser
    {
        private readonly PlayerIdentifier playerIdentifier;

        public PlayerIdentifierParser(PlayerIdentifier playerIdentifier)
        {
            this.playerIdentifier = playerIdentifier;
        }

        public byte[] Serialize()
        {
            var destination = new byte[4];
            Util.CopyBytes(destination, BitConverter.GetBytes(playerIdentifier.Id));
            return destination;
        }

    }
}