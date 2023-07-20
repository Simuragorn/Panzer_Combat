using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shell : MonoBehaviour, IDestroyable
{
    public int Speed;
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
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(Speed * Time.fixedDeltaTime * direction);
            yield return new WaitForFixedUpdate();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<ITarget>();
        if (target == null)
        {
            return;
        }
        contactPoint = collision.ClosestPoint(transform.position);

        var polygonCollider = collision.gameObject.GetComponent<PolygonCollider2D>();
        var polygonPoints = ColliderExtension.GetColliderPoints(polygonCollider);
        for (int i = 0; i < polygonPoints.Count; i++)
        {
            Vector2 point2 = polygonPoints[i];
            Vector2 point1 = i == 0 ? polygonPoints[polygonPoints.Count - 1] : polygonPoints[i - 1];

            bool isCrosses = ((Vector3)contactPoint).IsLaysBetweenPoints(point1, point2);
            if (isCrosses)
            {
                colliderPoint1 = point1;
                colliderPoint2 = point2;
                Vector2 obstacleSide = (point1 - point2).normalized;
                float angle = Vector2.Angle(obstacleSide, contactPoint);
                angle %= 90;
                Debug.Log(angle);
            }
        }
    }

    private void OnDrawGizmos()
    {
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
}
