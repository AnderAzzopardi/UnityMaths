using UnityEngine;

public class BulletScript: MonoBehaviour
{
    public float speed = 10f;  // Adjust the speed as needed

    void Start()
    {
        // Set the bullet to destroy itself after 3 seconds
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
