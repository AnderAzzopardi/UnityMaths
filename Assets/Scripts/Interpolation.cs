using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interpolation : MonoBehaviour
{

    public GameObject goA;
    public GameObject goB;
    public GameObject Player;
    public float Interp_time = 5.0f;

    public EasingFunction.Ease ease;
    private EasingFunction.Function easeFunction;


    [Range(0f, 10f)]
    public float elapsedTime = 0.0f;

    private void DrawVector(Vector3 pos, Vector3 v, Color c)
    {
        Gizmos.color = c;
        Gizmos.DrawLine(pos, pos + v);
        // Arrow head?
        Handles.color = c;
        // Compute the "rough" endpoint for the cone
        // Normalize the vector (its magnitude becomes 1)
        Vector3 n = v.normalized;
        n = n * 0.35f; // Now the length is 35cm

        Handles.ConeHandleCap(0, pos + v - n, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);

    }

    private void OnDrawGizmos()
    {
        DrawVector(Vector3.zero, goA.transform.position, Color.green);
        DrawVector(Vector3.zero, goB.transform.position, Color.red);

        // Interpolate until Interp_time
        float t = elapsedTime / Interp_time;

        // Clamp the t to 1 (remember: t has to be between 0 and 1)
        if (t > 1.0f)
            t = 1.0f;

        // Compute the interpolation: f(t) = A*(1-t) + B*t
        Vector3 pos = (1 - t) * goA.transform.position + t * goB.transform.position;

        // Set the player position
        Player.transform.position = pos;

        // Draw the parts
        DrawVectorParts(t);

    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;

        easeFunction = EasingFunction.GetEasingFunction(ease);

    }

    void DrawVectorParts(float t)
    {
        Vector3 partOfA = (1 - t) * goA.transform.position;
        Vector3 partOfB = t * goB.transform.position;

        // Draw the vector parts
        DrawVector(Vector3.zero, partOfA, Color.magenta); // Starts from origin
        DrawVector(partOfA, partOfB, Color.magenta); // Starts from the end of the previous one

    }

    // Update is called once per frame
    void Update()
    {
        // Let's get the elapsed time
        elapsedTime += Time.deltaTime;

        // Interpolate until Interp_time
        float t = elapsedTime / Interp_time;

        // Clamp the t to 1 (remember: t has to be between 0 and 1)
        if (t > 1.0f)
            t = 1.0f;

        // Easing???
        t = easeFunction(0f, 1f, t);
        /*
        if (t < 0.5f)
        {
            //t = 2 * t * t;  // y = 2*x^2
            t = 4 * t * t * t;  // y = 4*x^3
        }
        else
        {
            //t = 1 - 2 * (1 - t) * (1 - t);  // y = 1 - 2*(1-x)^2
            t = 1 - 4 * (1 - t) * (1 - t) * (1 - t);  // y = 1 - 4*(1-x)^3
        }
        */

        // Compute the interpolation: f(t) = A*(1-t) + B*t
        Vector3 pos = (1 - t) * goA.transform.position + t * goB.transform.position;

        // Set the player position
        Player.transform.position = pos;


    }
}
