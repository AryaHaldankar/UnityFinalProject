using UnityEngine;

public class CreatorBehaviour : MonoBehaviour
{
    public GameObject terrainPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            float terrainPos = transform.parent.transform.position.z;
            GameObject newTerrain = Instantiate(terrainPrefab, new Vector3(-1000, -300, 2500 + terrainPos), Quaternion.identity);
            newTerrain.name = "terrain";
            transform.parent.gameObject.tag = "Terrain";
        }
    }
}
