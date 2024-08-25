using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 cameraPosition;
    private Camera cam;

    [Header("Camera Settings")]
    public float cameraSpeed = 20f;
    public float zoomSpeed = 2f;  // Speed of zooming
    

    [Header("Camera Boundaries")]
    public float minZoom = 5f;  // Minimum zoom (closest to the player)
    public float maxZoom = 13f;   // Maximum zoom (farthest from the player)
    public float minX = -10f;     // Minimum x position
    public float maxX = 10f;      // Maximum x position
    public float minY = -5f;     // Minimum y position
    public float maxY = 16f;      // Maximum y position


    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = this.transform.position;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float adjustedCameraSpeed = cameraSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            cameraPosition.y += adjustedCameraSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraPosition.y -= adjustedCameraSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraPosition.x -= adjustedCameraSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraPosition.x += adjustedCameraSpeed;
        }

        // Clamp the camera position to the boundaries
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);

        // Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scroll * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

        this.transform.position = cameraPosition;
    }
}
