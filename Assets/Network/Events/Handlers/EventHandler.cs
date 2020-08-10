using dto;
using Game;
using Game.Entity;
using Game.GameEntity;
using UnityEngine;

namespace Events.Handlers
{
    public interface EventHandler
    {
        void Execute(Player player);

        PlayerIdentifier GetPlayerIdentifier();
    }
}