using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 maxSpeed = new Vector3(0.001f, 0.001f, 0);

    

    private Momentum momentum;

    void Start()
    {
        momentum = new Momentum(maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += getAcceleration();
    }


    private Vector3 getAcceleration() {

        float accelerationX = (-getAccelerationForKey(KeyCode.A)) + getAccelerationForKey(KeyCode.D);
        float accelerationZ = (-getAccelerationForKey(KeyCode.S)) + getAccelerationForKey(KeyCode.W);
        return new Vector3(accelerationX, 0, accelerationZ);

    }

    private float getAccelerationForKey(KeyCode key) {

        if (Input.GetKey(key))
        {
            return 0.10f;
        }
        else
        {
            return 0;
        }
    }


}
