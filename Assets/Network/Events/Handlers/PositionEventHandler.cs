using dto;
using Events;
using Events.Handlers;
using Game.GameEntity;
using UnityEngine;

namespace Network.Events.Handlers
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
            Debug.Log("Received position update for entity id " + positionEvent.PlayerIdentifier.Id);
            Debug.Log("I am id " + player.playerIdentifier.Id);
            
            var pe = positionEvent.Position;
            Vector3 position = new Vector3(pe.X,pe.Y, pe.Z);
            player.transform.position = position;
        }
    }
}