using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPingText : MonoBehaviour
{

    public GameObject playerOne;
    public GameObject playerTwo;
    
    private CharacterController _characterController;
    private Text text;
    void Start()
    {
        _characterController = playerOne.GetComponent<CharacterController>();
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerOne.gameObject.transform.position, playerTwo.gameObject.transform.position);
        float velocity = 12;
        Debug.Log("Distance " + distance);
        Debug.Log("Velocity " + velocity);
        float rtt = distance / velocity;
        text.text = "RTT is " + rtt * 1000;


    }
}
