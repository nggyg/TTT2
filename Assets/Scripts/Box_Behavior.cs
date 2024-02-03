using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Behavior : MonoBehaviour
{
    public Transform pieceX;
    public Transform pieceO;
    public Light light;
    public Transform pointer;
    private float speed = 30f;

    private bool hasMovedX = false;
    private bool hasMovedO = false;
    private Game_Tracker parentScript;
    private Transform pieceToMove;

    void Start()
    {
        if (light == null)
        {
            light = GetComponent<Light>();
        }
        if (GetComponentInParent<Game_Tracker>() != null)
        {
            parentScript = GetComponentInParent<Game_Tracker>();
        }
        light.intensity = 0f;
    }

    void Update()
    {
        pieceToMove= transform.Find(parentScript.pieceToMove);
        if (InPosition(pointer.position) && !hasMovedO 
            && !hasMovedX && pointer.gameObject.activeSelf)
        {
            light.intensity = 10f;
            
            // Move pieceX if X is pressed and it hasn't moved yet
            if (Input.GetKeyDown(KeyCode.Space) && pieceX.position.y < 1.7f && pieceToMove==pieceX)
            {
                hasMovedX = true;
                parentScript.checkStatus("X",transform.name);
                StartCoroutine(MovePiece(pieceX, new Vector3(0f, 150f, 0f), 5.5f));
                            }
            // Move pieceO if O is pressed and it hasn't moved yet
            else if (Input.GetKeyDown(KeyCode.Space)  && pieceO.position.y < 1.7f && pieceToMove == pieceO)
            {
                hasMovedO = true;
                parentScript.checkStatus("O", transform.name);
                StartCoroutine(MovePiece(pieceO, new Vector3(0f, 0f, -150f), 5.5f));
            }
        }
        else
        {
            light.intensity = 0f;
            // Reset the hasMoved flags when not in position
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
        //parentScript.checkStatus(piece.name);
    }

    public void lowerPieces()
    {
        if (hasMovedO)
            pieceO.position = new Vector3(pieceO.position.x, -5.5f, pieceO.position.z);

        if (hasMovedX)
            pieceX.position = new Vector3(pieceX.position.x, -5.5f, pieceX.position.z);

        hasMovedX = false;
        hasMovedO = false;
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
