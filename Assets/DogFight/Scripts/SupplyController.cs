using UnityEngine;

public class SupplyController : MonoBehaviour
{
    [SerializeField] GameObject ammoCratePrefab;
    [SerializeField] GameObject fuelCratePrefab;
    [SerializeField] float fuelCrateInterval;
    [SerializeField] float ammoCrateInterval;
    [SerializeField] float leftRoadEdgeX;
    [SerializeField] float rightRoadEdgeX;

    void Start()
    {
        InvokeRepeating("SpawnAmmoCrate", ammoCrateInterval, ammoCrateInterval);
        InvokeRepeating("SpawnFuelCrate", fuelCrateInterval, fuelCrateInterval);
    }

    void Update()
    {
        if (GameManager.Instance.gameEnded)
        {
            CancelInvoke("SpawnAmmoCrate");
            CancelInvoke("SpawnFuelCrate");
        }
    }

    void SpawnAmmoCrate()
    {
        float xPoint = Random.Range(leftRoadEdgeX, rightRoadEdgeX);
        Instantiate(ammoCratePrefab, new Vector3(xPoint, -295f, 2400f), Quaternion.identity);
    }

    void SpawnFuelCrate()
    {
        float xPoint = Random.Range(leftRoadEdgeX, rightRoadEdgeX);
        Instantiate(fuelCratePrefab, new Vector3(xPoint, -295f, 2400f), Quaternion.identity);
    }
}