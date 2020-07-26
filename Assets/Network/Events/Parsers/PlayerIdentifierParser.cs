using System;
using dto;
using util;

namespace Events.Parsers
{
    public class PlayerIdentifierParser
    {

        public PlayerIdentifierParser()
        {

        }

        public PlayerIdentifier Parse(byte[] payload)
        {
            int playerId = BitConverter.ToInt32(payload, 0);
            return new PlayerIdentifier(playerId);
        }


    }
}