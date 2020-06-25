using dto;
using UnityEngine;

namespace Events.Handlers
{
    public interface EventHandler
    {
        void Execute(Player player);

        PlayerIdentifier GetPlayerIdentifier();
    }
}