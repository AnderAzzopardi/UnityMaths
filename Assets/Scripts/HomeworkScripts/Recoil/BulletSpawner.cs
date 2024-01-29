using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Assuming left mouse button for shooting
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        if (bulletPrefab != null)
        {
            // Instantiate a bullet prefab at the current position and rotation of the spawner
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned to the BulletSpawner!");
        }
    }
}
