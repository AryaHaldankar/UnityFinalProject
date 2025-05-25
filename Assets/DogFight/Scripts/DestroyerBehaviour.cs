using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour
{

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
            Destroy(transform.parent.gameObject);
    }
}
