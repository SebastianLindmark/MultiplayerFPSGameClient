using System.Collections;
using System.Collections.Generic;
using dto;
using Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public NetworkPacketManager networkPacketManager;

    private PlayerIdentifier playerIdentifier = new PlayerIdentifier(1337);

    void Start()
    {
        Invoke(nameof(SendJoinEvent), 1f);
    }


    private void SendJoinEvent()
    {
        Debug.Log("Sending join event for player " + playerIdentifier.Id);
        var joinEvent = new JoinEvent(playerIdentifier);
        networkPacketManager.Send(joinEvent);
    }

    public PlayerIdentifier GetPlayerIdentifier()
    {
        return playerIdentifier;
    }

    // Update is called once per frame
    void Update()
    {
    }
}