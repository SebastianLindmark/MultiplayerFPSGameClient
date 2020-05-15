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

        private FramedPlayerAttribute playerAttribute;
        
        public void Start()
        {
            playerAttribute = player.GetComponent<FramedPlayerAttribute>();
            InvokeRepeating("CheckPosition", 1f, 0.5f);
            
        }

        void CheckPosition()
        {
            
            Vector3 currentPosition = player.transform.position;
            Quaternion currentRotation = player.transform.rotation;

            if (!PositionChanged(currentPosition, currentRotation)) return;

            storedPosition = currentPosition;
            storedRotation = currentRotation;
            
            Position position = new Position(currentPosition, currentRotation);
            playerAttribute.AddPosition(position);
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