using UnityEngine;

namespace Game
{
    public class AIRandomMovement : MonoBehaviour
    {
        // Start is called before the first frame update

        public CharacterController controller;

        public float speed = 12f;

        private Vector3 velocity;

        private float movementTimer;

        void Update()
        {
            
            float x = 0;
            float z = 0;

            Debug.Log(Time.time);
            if (Time.time - movementTimer > 10)
            {
                movementTimer = Time.time;
                
            }
            else if (Time.time - movementTimer > 7.5)
            {
                z = 1;
            }
            else if (Time.time - movementTimer > 5.0)
            {
                x = 1;
            }
            else if (Time.time - movementTimer > 2.5)
            {
                z = -1;
            }
            else
            {
                x = -1;
            }


            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (speed * Time.deltaTime));
        }
    }
}