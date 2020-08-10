using System.Collections.Generic;
using dto;
using Events;
using Game.GameEntity;
using NetworkPlayer;
using UnityEngine;

namespace Network.NetworkPlayer
{
    public class PlayerNetworkController : MonoBehaviour
    {

        private PlayerAttribute playerAttribute = new PlayerAttribute();

        public PlayerAttribute PlayerAttribute => playerAttribute;

        private Player player;
        
        public NetworkPacketManager networkPacketManager;
        
        
        public void Start()
        {
            player = GetComponent<Player>();
            InvokeRepeating(nameof(SendPlayerUpdates), 2f,0.05f);
        }


        private void SendPlayerUpdates()
        {
            List<GameEvent> events = CollectAttributes(player.playerIdentifier);
            events.ForEach(e => networkPacketManager.Send(e));
            playerAttribute = new PlayerAttribute();
            
        }

        public void AddPosition(Position position)
        {
            playerAttribute.Position = position;
        }

        public void AddFiring()
        {
            playerAttribute.AddFiring();
        }

        private List<GameEvent> CollectAttributes(PlayerIdentifier playerIdentifier)
        {
            List<GameEvent> actions = new List<GameEvent>();

            if (playerAttribute.Position != null)
            {
                var ev = new PositionEvent(playerIdentifier, playerAttribute.Position);
                actions.Add(ev);
            }

            return actions;

        }

        public void NewFrame()
        {
            playerAttribute = new PlayerAttribute();
        }


    }
}