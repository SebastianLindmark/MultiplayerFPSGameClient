using System;
using System.Collections.Generic;
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

        public PlayerManager playerManager;
        
        public void Start()
        {
            
        }

        public void AddPosition(Position position)
        {
            playerAttribute.Position = position;
        }

        public void AddFiring()
        {
            playerAttribute.AddFiring();
        }
        
        public List<GameEvent> GetAttributeFrame()
        {
            PlayerIdentifier playerIdentifier = playerManager.GetLocalPlayerIdentifier();
            var gameEvents = AddAttributes(playerIdentifier);
            playerAttribute = new PlayerAttribute();
            return gameEvents;
        }

        public PlayerIdentifier GetPlayerIdentifier()
        {
            return playerManager.GetLocalPlayerIdentifier();
        }

        private List<GameEvent> AddAttributes(PlayerIdentifier playerIdentifier)
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