using UnityEngine;

[RequireComponent(typeof(Hull))]
[RequireComponent(typeof(Engine))]
[RequireComponent(typeof(Track))]
[RequireComponent(typeof(Gun))]
public class Tank : MonoBehaviour
{
    protected Hull hull;
    protected Engine engine;
    protected Track track;
    protected Gun gun;

    protected void Awake()
    {
        hull = GetComponent<Hull>();
        engine = GetComponent<Engine>();
        track = GetComponent<Track>();
        gun = GetComponent<Gun>();
    }

    public void Shoot()
    {
        gun.TryShoot();
    }

    public void Move(float verticalAxisInput)
    {
        engine.Move(verticalAxisInput);
    }

    public void Rotate(float horizontalAxisInput)
    {
        track.Rotate(horizontalAxisInput);
    }
}
