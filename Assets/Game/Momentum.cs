using UnityEngine;

public class Momentum
{


    public Vector3 initialMomentum = new Vector3(0, 0, 0);

    private Vector3 currentMomentum;

    private Vector3 maxMomentum;

    public Momentum(Vector3 maxMomentum)
    {
        currentMomentum = initialMomentum;
        this.maxMomentum = maxMomentum;
    }


    public void addMomentum(Vector3 addMomentum)
    {
        Vector3 resulting = currentMomentum += addMomentum;
        if (resulting.x <= maxMomentum.x && resulting.y <= maxMomentum.y)
        {
            currentMomentum += addMomentum;
        }

    }

    public void resetX()
    {
        currentMomentum.x = 0;
    }

    public void resetY()
    {
        currentMomentum.y = 0;
    }

    public Vector3 getMomentum()
    {
        return this.currentMomentum;
    }



}