using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour, ITarget
{
    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        var target = targetCollider.gameObject.GetComponent<Shell>();
        if (target != null)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            targetCollider.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;
        }
    }
}
