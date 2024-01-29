using System.Collections;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public float recoilDistance = 1f;
    public float recoilAngle = 30f;
    public float recoilSpeed = 0.5f;
    public float returnSpeed = 0.3f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Assuming left mouse button for shooting
        {
            StartCoroutine(Recoil());
        }
    }

    IEnumerator Recoil()
    {
        // Recoil animation
        float elapsed = 0f;

        Vector3 recoilPosition = originalPosition - new Vector3(0, 0, recoilDistance);
        Vector3 recoilRotation = new Vector3(recoilAngle, 0f, 0f);

        while (elapsed < recoilSpeed)
        {
            transform.position = Vector3.Lerp(originalPosition, recoilPosition, elapsed / recoilSpeed);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, recoilRotation, elapsed / recoilSpeed));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return to original position
        elapsed = 0f;

        while (elapsed < returnSpeed)
        {
            transform.position = Vector3.Lerp(recoilPosition, originalPosition, elapsed / returnSpeed);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(recoilRotation, Vector3.zero, elapsed / returnSpeed));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
