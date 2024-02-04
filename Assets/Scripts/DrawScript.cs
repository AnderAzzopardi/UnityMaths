using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    [SerializeField] private float circRadius;

    [SerializeField] GameObject _vectorPos, _lookAtVector;
    [Range(-180f, 180f)]
    [SerializeField] private float _angleThreshold;

    public float circHeight;

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Vector3 vectorPos = _vectorPos.transform.position;
        Vector3 lookAtPos = _lookAtVector.transform.position - position;

        Vector3 l = lookAtPos.normalized;
        Vector3 n = (vectorPos - position).normalized;

        // NPC_Vector
        DrawVector(Vector3.zero, position, Color.white, 1.5f);

        // LOOK-AT_Vector
        DrawVector(position, lookAtPos, Color.magenta, 1.5f);

        // PLAYER_Vector
        DrawVector(Vector3.zero, vectorPos, Color.white, 1.5f);

        float dot = Vector3.Dot(l, n);
        float dotThreshold = Mathf.Cos(Mathf.Deg2Rad * _angleThreshold);
        Color dotColor = dot > dotThreshold ? Color.red : Color.blue;
        DrawVector(position, vectorPos - position, dotColor, 1.5f);

        // Circle
        Color circleColor = Vector3.Distance(position, vectorPos) >= circRadius ? Color.green : Color.red;
        Handles.color = circleColor;
        Handles.DrawWireDisc(position, Vector3.forward, circRadius, 1.5f);

        // Wedge
        Handles.color = Color.white;
        DrawWedgeLines(position, l, circRadius, circHeight);

        // Quaternion for rotation
        DrawRotation(position, l);
    }

    private void DrawWedgeLines(Vector3 position, Vector3 direction, float radius, float height)
    {
        Vector3 rotated = RotateVectorByAngle(direction, _angleThreshold);
        Vector3 rotated2 = RotateVectorByAngle(direction, -_angleThreshold);

        DrawDiskLines(position, rotated, radius, height);
        Gizmos.DrawLine(position - Vector3.forward * height / 2f, position + Vector3.forward * height / 2f);
        DrawDiskLines(position, rotated2, radius, height);
    }

    private void DrawDiskLines(Vector3 position, Vector3 direction, float radius, float height)
    {
        Vector3 upperPoint = position + Vector3.forward * height / 2f + direction * radius;
        Vector3 lowerPoint = position - Vector3.forward * height / 2f + direction * radius;

        Gizmos.DrawLine(position + Vector3.forward * height / 2f, upperPoint);
        Gizmos.DrawLine(position - Vector3.forward * height / 2f, lowerPoint);
        Gizmos.DrawLine(upperPoint, lowerPoint);
    }

    private void DrawRotation(Vector3 position, Vector3 direction)
    {
        // Upper
        Vector3 rotated = RotateVectorByAngle(direction, _angleThreshold);
        DrawVector(position, rotated * circRadius, Color.yellow, 2f);

        // Lower
        Vector3 rotated2 = RotateVectorByAngle(direction, -_angleThreshold);
        DrawVector(position, rotated2 * circRadius, Color.yellow, 2f);
    }

    private Vector3 RotateVectorByAngle(Vector3 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        return rotation * vector;
    }

    private void DrawVector(Vector3 pos, Vector3 v, Color c, float thickness = 0.0f)
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
