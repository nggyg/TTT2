using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public float verticalRotationLimit;
    public Transform pointer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointer.gameObject.activeSelf)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            RotateCamera(new Vector3(-mouseY, mouseX, 0f) * rotationSpeed);

            if (Input.GetKey(KeyCode.D))
            {
                MovePointer(new Vector3(30f, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.A))
            {
                MovePointer(new Vector3(-30f, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.W))
            {
                MovePointer(new Vector3(0f, 0f, 30f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                MovePointer(new Vector3(0f, 0f, -30f));
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MovePointer(new Vector3(0f, 30f, 0f));
            }
            if (Input.GetKey(KeyCode.LeftControl) && transform.position.y > 0f)
            {
                MovePointer(new Vector3(0f, -30f, 0f));
            }
        }
    }

    void RotateCamera(Vector3 rotation)
    {
        // Clamp vertical rotation
        float currentRotationX = transform.eulerAngles.x;
        currentRotationX = (currentRotationX > 180f) ? currentRotationX - 360f : currentRotationX;
        float newRotationX = Mathf.Clamp(currentRotationX + rotation.x, -verticalRotationLimit, verticalRotationLimit);

        // Apply rotation
        transform.rotation = Quaternion.Euler(newRotationX, transform.eulerAngles.y + rotation.y, 0f);
    }
    void MovePointer(Vector3 movement)
    {
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
