using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKey(KeyCode.LeftControl) && transform.position.y>0f)
        {
            MovePointer(new Vector3(0f, -30f, 0f));
        }

    }

    void MovePointer(Vector3 movement)
    {
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
