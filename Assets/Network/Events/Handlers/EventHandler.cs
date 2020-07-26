using dto;
using Game;
using UnityEngine;

namespace Events.Handlers
{
    public interface EventHandler
    {
        void Execute(Player player);

        PlayerIdentifier GetPlayerIdentifier();
    }
}