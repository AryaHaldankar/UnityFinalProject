using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler Instance;
    [SerializeField] int size = 10;
    [SerializeField] GameObject bulletPrefab;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    public void SpawnBullet(Transform bulletTransform)
    {
        if (pool.Count == 0)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            pool.Enqueue(newBullet);
            size++;
        }
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = bulletTransform.position;
        bullet.transform.rotation = bulletTransform.rotation;
        StartCoroutine(DeactivateBullet(bullet));
    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}