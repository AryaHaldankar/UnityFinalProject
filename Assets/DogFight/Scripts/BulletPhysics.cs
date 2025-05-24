using UnityEngine;
using System.Collections;

public class BulletPhysics : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1000f;
    // private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
    }

    // IEnumerator DestroyBullet()
    // {
    //     yield return new WaitForSeconds(1f);
    //     Destroy(gameObject);
    // }
    // void FixedUpdate()
    // {
    //     rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.fixedDeltaTime);
    // }
}
