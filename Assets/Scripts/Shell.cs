using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

[RequireComponent(typeof(Rigidbody2D))]
public class Shell : MonoBehaviour, IDestroyable
{
    public int Speed;
    public Rigidbody2D Rigidbody;
    public Collider2D Collider;
    public GameObject VFX;

    private Vector2 direction;
    public Vector2 colliderPoint1;
    public Vector2 colliderPoint2;
    public Vector2 contactPoint;

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
        Vector2 positionContact = (transform.position - collision.transform.position).normalized;

        var target = collision.gameObject.GetComponent<ITarget>();
        if (target != null)
        {
            var contacts = new List<ContactPoint2D>();
            int numberOfContacts = collision.GetContacts(contacts);
            contactPoint = contacts[0].point;

            var polygonCollider = collision.gameObject.GetComponent<PolygonCollider2D>();
            var polygonPoints = GetPolygonColliderPoints(polygonCollider);
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                Vector2 point2 = polygonPoints[i];
                Vector2 point1 = i == 0 ? polygonPoints[polygonPoints.Count - 1] : polygonPoints[i - 1];
                bool isCrosses = IsPointBetweenPoints(point1, point2, contactPoint);
                if (isCrosses)
                {
                    colliderPoint1 = point1;
                    colliderPoint2 = point2;
                    Vector2 obstacleSide = (point1 - point2).normalized;
                    float angle = Vector2.Angle(obstacleSide, positionContact);
                    angle %= 90;
                    Debug.Log(angle);
                    //Collider.enabled = false;
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(colliderPoint1, colliderPoint2);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(contactPoint, 0.7f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(colliderPoint1, 0.7f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(colliderPoint2, 0.7f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(colliderPoint1, contactPoint);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(colliderPoint2, contactPoint);
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

    private bool IsPointBetweenPoints(Vector3 startPoint, Vector3 endPoint, Vector3 targetPoint)
    {
        Vector3 endToStart = endPoint - startPoint;
        Vector3 targetToStart = targetPoint - startPoint;

        var crossVector = Vector3.Cross(targetToStart.normalized, endToStart.normalized);
        bool colinear = Mathf.Abs(crossVector.x) < 0.1 && Mathf.Abs(crossVector.y) < 0.1 && Mathf.Abs(crossVector.z) < 0.1;
        if (!colinear)
            return false;

        float epsilon = 0.0001f;
        bool suitableMagnitude = targetToStart.magnitude <= endToStart.magnitude + epsilon;

        if (!suitableMagnitude)
            return false;

        return true;
    }
}
