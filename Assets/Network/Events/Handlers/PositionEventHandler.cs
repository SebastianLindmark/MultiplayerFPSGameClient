using dto;
using Game;
using UnityEditor;
using UnityEngine;

namespace Events.Handlers
{
    public class PositionEventHandler : EventHandler
    {

        private readonly PositionEvent positionEvent;
        private readonly PlayerIdentifier playerIdentifier;

        public PositionEventHandler(PositionEvent positionEvent)
        {
            this.positionEvent = positionEvent;
            this.playerIdentifier = positionEvent.PlayerIdentifier;
        }

        public PlayerIdentifier GetPlayerIdentifier()
        {
            return playerIdentifier;
        }


        public void Execute(Player player)
        {
            
            var pe = positionEvent.Position;
            Vector3 position = new Vector3(pe.X,pe.Y, pe.Z);
            player.transform.position = position;
        }
    }
}