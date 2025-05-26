using UnityEngine;
using System.Collections.Generic;

public class TerrainBehaviour : MonoBehaviour
{
    private float terrainShiftSpeed;
    private bool isCrate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCrate = false;
        terrainShiftSpeed = GameManager.Instance.terrainMovementSpeed;
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.CompareTag("Fuel") || child.gameObject.CompareTag("Ammo"))
                isCrate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameEnded)
            return;
        transform.Translate(Vector3.back * terrainShiftSpeed * Time.deltaTime);

        if (isCrate && transform.position.z < 0)
            Destroy(gameObject);
    }
}
