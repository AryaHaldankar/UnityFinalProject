using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    public float movementSpeed;
    // private RayCastHit[] hits;
    // private Rigidbody rb;
    private int frames = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    void Update(){
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
        if(frames >= 90)
            Destroy(gameObject);
        frames += 1;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if(frames >= 90)
    //         Destroy(gameObject);
    //     frames += 1;
    //     rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.fixedDeltaTime);
    // }
}
