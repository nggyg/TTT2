using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Behavior : MonoBehaviour
{
    public Transform pieceX;
    public Transform pieceO;
    public Light light;
    public Transform pointer;
    private float speed = 10f;

    private bool hasMovedX = false;
    private bool hasMovedO = false;

    void Start()
    {
        if (light == null)
        {
            light = GetComponent<Light>();
        }

        light.intensity = 0f;
    }

    void Update()
    {
        if (InPosition(pointer.position))
        {
            light.intensity = 10f;

            // Move pieceX if X is pressed and it hasn't moved yet
            if (Input.GetKeyDown(KeyCode.X) && !hasMovedO && !hasMovedX && pieceX.position.y < 1.7f)
            {
                StartCoroutine(MovePiece(pieceX, new Vector3(0f, 150f, 0f), 1.7f));
                hasMovedX = true;
                updateBoard();
            }

            // Move pieceO if O is pressed and it hasn't moved yet
            if (Input.GetKeyDown(KeyCode.O) && !hasMovedO && !hasMovedX && pieceO.position.y < 11.8f)
            {
                StartCoroutine(MovePiece(pieceO, new Vector3(0f, 0f, -150f), 11.8f));
                hasMovedO = true;
                updateBoard();
            }
        }
        else
        {
            light.intensity = 0f;
            // Reset the hasMoved flags when not in position
            hasMovedX = false;
            hasMovedO = false;
        }
    }

    private void updateBoard()
    {
        Game_Tracker parentScript = GetComponentInParent<Game_Tracker>();
        if (parentScript != null)
        {
            parentScript.checkStatus();
        }
    }

    IEnumerator MovePiece(Transform piece, Vector3 movement, float limit)
    {
        movement.Normalize();
        while (Mathf.Abs(piece.position.y - limit) > 0.1f)
        {
            piece.Translate(movement * speed * Time.deltaTime);
            yield return null; // wait for the next frame
        }
    }

    private bool InPosition(Vector3 position)
    {
        Vector3 origin = transform.position;

        return (
            position.x < origin.x + 10 &&
            position.x > origin.x - 10 &&
            position.z < origin.z + 10 &&
            position.z > origin.z - 10
        );
    }
    
}
