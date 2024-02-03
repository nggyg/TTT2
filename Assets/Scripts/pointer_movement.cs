using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer_movement : MonoBehaviour
{
    public float speed;
    public Transform cameraTransform; // Reference to your camera's Transform

    // Update is called once per frame
    void Update()
    {
        // Ensure that you have assigned the camera's transform in the Inspector

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform not assigned in the inspector!");
            return;
        }

        // Get the camera's forward, right, and up vectors
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        Vector3 up = cameraTransform.up;

        // Ignore the y-component for movement on the XZ plane
        forward.y = 0f;
        right.y = 0f;
        up.y = 0f;

        // Normalize the vectors
        forward.Normalize();
        right.Normalize();
        up.Normalize();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovePointer(right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovePointer(-right);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovePointer(forward);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovePointer(-forward);
        }
    }

    void MovePointer(Vector3 movement)
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
