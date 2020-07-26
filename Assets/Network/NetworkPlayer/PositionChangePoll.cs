using System;
using System.Threading;
using dto;
using UnityEngine;

namespace NetworkPlayer
{
    public class PositionChangePoll : MonoBehaviour
    {
        public GameObject player;

        private Vector3 storedPosition;
        private Quaternion storedRotation;

        private PlayerNetworkController playerNetworkController;
        
        public void Start()
        {
            playerNetworkController = player.GetComponent<PlayerNetworkController>();
            InvokeRepeating(nameof(CheckPosition), 1f, 0.05f);
            
        }

        void CheckPosition()
        {
            
            Vector3 currentPosition = player.transform.position;
            Quaternion currentRotation = player.transform.rotation;
            if (!PositionChanged(currentPosition, currentRotation)) return;

            storedPosition = currentPosition;
            storedRotation = currentRotation;
            
            Position position = new Position(currentPosition, currentRotation);
            playerNetworkController.AddPosition(position);
        }

        private bool PositionChanged(Vector3 currentPosition, Quaternion currentRotation)
        {
            
            float angle = Quaternion.Angle(storedRotation, currentRotation);

            bool rotationChange = Mathf.Abs(angle) > Quaternion.kEpsilon;
            bool positionChange = Vector3.Distance(storedPosition, currentPosition) > Vector3.kEpsilon;
            
            return rotationChange || positionChange;
            
        }
    }
}