using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{

    private int playerCounter;

    void Start()
    {
        playerCounter = 0;
        
    }
    
    

    public void createPlayer(Player gameObject) {

        Player instantiated = Instantiate(gameObject);

        //make spawn at position
        
    
    }

    
    void Update()
    {
        
    }
}
