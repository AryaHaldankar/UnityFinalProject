using UnityEngine;

public class CreatorBehaviour : MonoBehaviour
{
    public GameObject terrainPrefab;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            Debug.Log("Creating terrain");
            float terrainPos = transform.parent.transform.position.z;
            GameObject newTerrain = Instantiate(terrainPrefab, new Vector3(-1000, -300, 2500 + terrainPos), Quaternion.identity);
            newTerrain.name = "terrain";
            transform.parent.gameObject.tag = "Terrain";
        }
    }
}
