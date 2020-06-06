using dto;
using UnityEngine;

namespace Events.Handlers
{
    public interface EventHandler
    {
        void Execute(MonoBehaviour monoBehaviour);

        PlayerIdentifier GetPlayerIdentifier();
    }
}