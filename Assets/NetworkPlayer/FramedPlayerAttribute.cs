using System;
using System.Collections.Generic;
using System.Linq;
using dto;
using Events;
using Network;
using UnityEngine;

namespace NetworkPlayer
{
    public class FramedPlayerAttribute : MonoBehaviour
    {

        private PlayerAttribute playerAttribute = new PlayerAttribute();

        public PlayerAttribute PlayerAttribute => playerAttribute;

        private PlayerIdentifier playerIdentifier;
        
        public NetworkPacketManager networkPacketManager;
        
        
        public void Start()
        {
            playerIdentifier = GetComponent<Player>().GetPlayerIdentifier();
            InvokeRepeating(nameof(SendPlayerUpdates), 2f,0.5f);
        }


        private void SendPlayerUpdates()
        {
            List<GameEvent> events = CollectAttributes(playerIdentifier);
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