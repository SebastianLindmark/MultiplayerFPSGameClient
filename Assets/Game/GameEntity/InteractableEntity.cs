using System;
using UnityEngine;
using EventHandler = Events.Handlers.EventHandler;

namespace Game.Entity
{
    public class InteractableEntity : MonoBehaviour
    {

        private GameEntity.Entity entity;
        private void Start()
        {
            entity = gameObject.GetComponent<GameEntity.Entity>();

            if (!entity)
            {
                throw new Exception("Can only be attached to entity");
            }
        }

        public void OnAction(EventHandler eventHandler)
        {
            entity.OnAction(eventHandler);
        }
    
    }
}