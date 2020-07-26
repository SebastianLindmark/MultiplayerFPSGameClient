using System.Collections.Concurrent;
using dto;
using Events.Handlers;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour, NetworkEntity
    {
    
        public int playerId;
        public string username;

        private PlayerIdentifier playerIdentifier;
        
        private readonly ConcurrentQueue<EventHandler> eventHandlerQueue = new ConcurrentQueue<EventHandler>();

        void Start()
        {
            playerIdentifier = new PlayerIdentifier(playerId);

            GameObject gameObj = GameObject.FindWithTag("EntityManager");

            if (gameObj != null)
            {
                EntityManager playerManager = gameObj.GetComponent<EntityManager>();
                playerManager.Add(playerIdentifier, this);
            }
        
        }


        public void OnAction(EventHandler eventHandler)
        {
            eventHandlerQueue.Enqueue(eventHandler);
        }

        public PlayerIdentifier GetPlayerIdentifier()
        {
            return playerIdentifier;
        }
    
        void Update()
        {
            if (eventHandlerQueue.TryDequeue(out var eventHandler))
            {
                Debug.Log("Parsing message");
                eventHandler.Execute(this);
            }
        }

        public PlayerIdentifier getId()
        {
            return playerIdentifier;
        }

        public void onEventReceive(EventHandler eventHandler)
        {
            throw new System.NotImplementedException();
        }
    }
}