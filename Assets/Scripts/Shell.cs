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
        StartCoroutine(MoveBullet());
    }

    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        var target = targetCollider.gameObject.GetComponent<ITarget>();
        if (target != null)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            targetCollider.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;

            Vector2 closestPointA = targetCollider.ClosestPoint(contactPoint);
            Vector2 closestPointB = targetCollider.ClosestPoint(contactPoint + (contactPoint - closestPointA));

            Vector2 hullSideVector = closestPointA - closestPointB;

            float contactAngle = Vector2.Angle(hullSideVector, contactPoint);
            Debug.Log(contactAngle);

            if (contacts.Length > 0)
            {
                ;
            }


            Vector2 normal = (transform.position - targetCollider.transform.position).normalized;
            direction = Vector2.Reflect(direction, normal);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            VFX.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
    }

    private IEnumerator MoveBullet()
    {
        while (true)
        {
            transform.Translate(direction * Speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
