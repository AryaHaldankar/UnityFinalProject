using UnityEngine;

public class Vehicle : MonoBehaviour {
    protected float movementSpeed = 100f;
    public float movementSpeed //ENCAPSULATION
    {
        get { return movementSpeed; }
        set
        {
            if (value < 0)
                Debug.Log("Speed cannot be negative.");
            else
                movementSpeed = value;
        }
    }

    protected abstract void Move() { } // children have to implement move since all vehicles move
    
    protected virtual void shootBack(){} // not all children can shoot back, reserved for TANK
}