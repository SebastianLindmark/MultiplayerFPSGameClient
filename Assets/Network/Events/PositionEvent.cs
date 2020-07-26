using System;
using dto;
using UnityEngine;
using util;

namespace Events
{
    public class PositionEvent : GameEvent
    {
        private readonly PlayerIdentifier playerIdentifier;
        private readonly Position position;

        public PlayerIdentifier PlayerIdentifier => playerIdentifier;

        public Position Position => position;
        
        private readonly byte modeByte = 10; 

        public PositionEvent(PlayerIdentifier playerIdentifier, Position position)
        {
            this.playerIdentifier = playerIdentifier;
            this.position = position;
        }
        
        public byte[] Serialize()
        {
            byte[][] arr =
            {
                BitConverter.GetBytes(playerIdentifier.Id),
                new[]{modeByte},
                BitConverter.GetBytes(position.X),
                BitConverter.GetBytes(position.Y),
                BitConverter.GetBytes(position.Z),
                BitConverter.GetBytes(position.XRotation),
            };
            byte[] destination = new byte[1 + 5*4];
            Util.CopyBytes(destination, arr);
            return destination;
        }
    }
}