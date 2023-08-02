using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float offsetSmoothing;
    [SerializeField] private float zoomSensitivity;
    private HumanController playerController;
    private Camera camera;
    void Start()
    {
        playerController = FindObjectOfType<HumanController>();
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        PlayerFollowing();
        Zoom();
    }

    private void PlayerFollowing()
    {
        var playerPosition = new Vector3(playerController.TankPosition.x, playerController.TankPosition.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }

    private void Zoom()
    {
        float cameraSizeChanges = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        if (camera.orthographic)
        {
            camera.orthographicSize -= cameraSizeChanges;
        }
        else
        {
            camera.fieldOfView -= cameraSizeChanges;
        }
    }
}
