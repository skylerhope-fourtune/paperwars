using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 cameraPosition;

    [Header("Camera Settings")]
    public float cameraSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = this.transform.position;
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

        this.transform.position = cameraPosition;
    }
}
