using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using dto;
using Events;
using Events.Handlers;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public NetworkPacketManager networkPacketManager;

    public int playerId;

    private PlayerIdentifier playerIdentifier;

    private ConcurrentQueue<EventHandler> eventHandlerQueue = new ConcurrentQueue<EventHandler>();

    void Start()
    {
        playerIdentifier = new PlayerIdentifier(playerId);

        GameObject gameObj = GameObject.FindWithTag("EntityManager");

        if (gameObj != null)
        {
            gameObj.GetComponent<PlayerManager>().Add(playerIdentifier, this);
        }

        Invoke(nameof(SendJoinEvent), 1f);
    }


    private void SendJoinEvent()
    {
        Debug.Log("Sending join event for player " + playerIdentifier.Id);
        var joinEvent = new JoinEvent(playerIdentifier);
        networkPacketManager.Send(joinEvent);
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
            eventHandler.Execute(this);
        }
    }
}