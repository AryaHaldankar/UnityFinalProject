using UnityEngine;
using System.Collections;

public class BulletPhysics : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 500f;

    void Update()
    {
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
    }
}
