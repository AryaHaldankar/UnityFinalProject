using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    public float terrainShiftSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * terrainShiftSpeed * Time.deltaTime);
    }
}
