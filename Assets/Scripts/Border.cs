using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour
{
    public BoxCollider2D Collider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDestroyObject(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDestroyObject(collision.gameObject);
    }

    private void TryDestroyObject(GameObject gameObject)
    {
        var destroyableObject = gameObject.GetComponent<IDestroyable>();
        if (destroyableObject != null)
        {
            destroyableObject.Destroy();
        }
    }
}
