using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shell : MonoBehaviour, IDestroyable
{
    public int Speed;
    public Rigidbody2D Rigidbody;
    public GameObject VFX;

    private Vector2 direction;

    private void Start()
    {
        direction = transform.up;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Launch()
    {
        Rigidbody.AddForce(Vector2.up * Speed * 10, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = (transform.position - collision.transform.position).normalized;

        var target = collision.gameObject.GetComponent<ITarget>();
        if (target != null)
        {
            var contacts = new List<ContactPoint2D>();
            int numberOfContacts = collision.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;

            var polygonCollider = collision.gameObject.GetComponent<PolygonCollider2D>();
            var polygonPoints = GetPolygonColliderPoints(polygonCollider);
            for (int i = 1; i < polygonPoints.Count; i++)
            {
                bool isCrosses = IsVectorCrossesPoint(polygonPoints[i], polygonPoints[i - 1], contactPoint);
                if (isCrosses)
                {
                    normal = (polygonPoints[i] - polygonPoints[i - 1]).normalized;
                }
            }

            //direction = Vector2.Reflect(direction, normal);

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //VFX.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
    }

    private List<Vector2> GetPolygonColliderPoints(PolygonCollider2D polygonCollider)
    {
        var polygonPoints = new List<Vector2>();
        if (polygonCollider == null)
        {
            return polygonPoints;
        }
        int pathCount = polygonCollider.pathCount;

        for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
        {
            Vector2[] pathPoints = polygonCollider.GetPath(pathIndex);

            int pointCount = pathPoints.Length;

            for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
            {
                Vector2 point = polygonCollider.transform.TransformPoint(pathPoints[pointIndex]);
                polygonPoints.Add(point);
            }
        }
        return polygonPoints;
    }

    private bool IsVectorCrossesPoint(Vector2 point1, Vector2 point2, Vector2 crossPoint)
    {
        Vector2 vector1 = crossPoint - point1;
        Vector2 vector2 = crossPoint - point2;

        // Calculate the cross product
        float crossProduct = Vector3.Cross(vector1, vector2).z;

        // Check if the cross product is zero (point lies on the line) or has different signs
        return Mathf.Approximately(crossProduct, 0f) || crossProduct < 0f;
    }
}
