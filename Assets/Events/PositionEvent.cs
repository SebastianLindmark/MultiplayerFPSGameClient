﻿using System;
using dto;
using util;

namespace Events
{
    public class PositionEvent : GameEvent
    {
        private readonly PlayerIdentifier playerIdentifier;
        private readonly Position position;

        public PositionEvent(PlayerIdentifier playerIdentifier, Position position)
        {
            this.playerIdentifier = playerIdentifier;
            this.position = position;
        }

        public byte[] Serialize()
        {
            byte[][] arr =
            {
                new byte[]{10},
                BitConverter.GetBytes(playerIdentifier.Id),
                BitConverter.GetBytes(position.X),
                BitConverter.GetBytes(position.Y),
                BitConverter.GetBytes(position.Z),
                BitConverter.GetBytes(position.XRotation),
            };

            byte[] destination = new byte[21];
            Util.CopyBytes(destination, arr);
            return destination;
        }
    }
}