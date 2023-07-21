using Assets.Scripts.Helpers;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shell : MonoBehaviour, IDestroyable
{
    public int Speed;
    public Collider2D Collider;
    public GameObject VFX;

    private Vector2 direction;
    private Vector2 contactPoint;
    private Vector2 positionPoint;
    private Vector2 obstacleVector;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        direction = transform.up;
    }

    public void Launch()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation);
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
        RaycastHit2D result = Physics2D.Raycast(transform.position, transform.up);
        if (result.collider == null ||
            result.collider.gameObject != collision.gameObject)
        {
            return;
        }
        Vector2 shellPosition = transform.position;
        contactPoint = result.point;
        var colliderPoints = target.GetColliderPoints();
        Vector2? crossingVector = VectorHelper.FindVectorThatCrossesTargetPoint(colliderPoints, contactPoint);
        if (!crossingVector.HasValue)
        {
            return;
        }
        obstacleVector = crossingVector.Value;
        Vector2 shootingVector = contactPoint - shellPosition;
        float angle = Vector2.Angle(obstacleVector, shootingVector);
        Debug.Log(angle);
        positionPoint = shellPosition;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z += angle;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(contactPoint, 0.7f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(positionPoint, 0.7f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(obstacleVector, obstacleVector * 0.5f);
    }
}
