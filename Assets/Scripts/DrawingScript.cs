using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    public GameObject targetObject; 

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector3.right * 5f);
        DrawArrowHead(origin + Vector3.right * 5f, Vector3.right);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + Vector3.up * 5f);
        DrawArrowHead(origin + Vector3.up * 5f, Vector3.up);

        DrawUnitCircle(origin);

        if (targetObject != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(origin, targetObject.transform.position);
            DrawArrowHead(targetObject.transform.position, targetObject.transform.position - origin);
        }
    }

    private void DrawArrowHead(Vector3 position, Vector3 direction)
    {
        float arrowSize = 0.2f;

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 45, 0) * Vector3.forward;

        Gizmos.DrawLine(position, position + right * arrowSize);
        Gizmos.DrawLine(position, position - right * arrowSize);
    }

    private void DrawUnitCircle(Vector3 origin)
    {
        int segments = 64;
        float radius = 1f;

        Gizmos.color = Color.white;

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
}
