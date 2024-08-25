using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 cameraPosition;
    private Camera cam;

    [Header("Camera Settings")]
    public float cameraSpeed = 1f;
    public float zoomSpeed = 2f;  // Speed of zooming
    public float minZoom = 5f;  // Minimum zoom (closest to the player)
    public float maxZoom = 13f;   // Maximum zoom (farthest from the player)

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = this.transform.position;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraPosition.y += cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraPosition.y -= cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraPosition.x -= cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraPosition.x += cameraSpeed / 10;
        }

        // Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scroll * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

        this.transform.position = cameraPosition;
    }
}
