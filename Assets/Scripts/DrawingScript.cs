using UnityEditor;
using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject LookAtObject;

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        // Check if targetObject is inside the unit circle
        bool isTargetInsideCircle = IsPointInsideUnitCircle(targetObject.transform.position, origin);

        DrawUnitCircle(origin, isTargetInsideCircle);

        if (targetObject != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(origin, targetObject.transform.position);
            DrawArrowHead(targetObject.transform.position, targetObject.transform.position - origin);
        }

        if (LookAtObject != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(origin, LookAtObject.transform.position);
            DrawArrowHead(LookAtObject.transform.position, targetObject.transform.position - origin);
        }
    }

    private void DrawArrowHead(Vector3 position, Vector3 direction)
    {
        float arrowSize = 0.2f;

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 45, 0) * Vector3.forward;

        Gizmos.DrawLine(position, position + right * arrowSize);
        Gizmos.DrawLine(position, position - right * arrowSize);
    }

    private void DrawUnitCircle(Vector3 origin, bool isTargetInsideCircle)
    {
        int segments = 64;
        float radius = 1f;

        Gizmos.color = isTargetInsideCircle ? Color.red : Color.green;

        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 360f;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            Vector3 start = new Vector3(x, y, 0) + origin;
            angle = (i + 1) / (float)segments * 360f;
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            Vector3 end = new Vector3(x, y, 0) + origin;

            Gizmos.DrawLine(start, end);
        }
    }

    private bool IsPointInsideUnitCircle(Vector3 point, Vector3 origin)
    {
        float radius = 1f;
        float distance = Vector3.Distance(point, origin);
        return distance <= radius;
    }

    private bool IsLookingAtPlayer(Vector3 point, Vector3 origin)
    {
        float radius = 1f;
        float distance = Vector3.Distance(point, origin);
        return distance <= radius;
    }

    static public void DrawVector(Vector3 pos, Vector3 v, Color c, float thickness = 0.0f)
    {
        Handles.color = c;
        Handles.DrawLine(pos, pos + v, thickness);
        // Compute the "rough" endpoint for the cone
        // Normalize the vector (its magnitude becomes 1)
        Vector3 n = v.normalized;
        n = n * 0.35f; // Now the length is 35cm

        Handles.ConeHandleCap(0, pos + v - n, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);

    }
}
