using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer_movement : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovePointer(new Vector3(30f, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovePointer(new Vector3(-30f, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovePointer(new Vector3(0f, 0f, 30f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovePointer(new Vector3(0f, 0f, -30f));
        }

    }

    void MovePointer(Vector3 movement)
    {
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
