using Microsoft.Extensions.FileSystemGlobbing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class Game_Tracker : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI textMesh;
    public Transform pointer;
    private string[][] board;
    private bool gameOver;
    public string pieceToMove = "X";
    void Start()
    {
        board = new string[][] { new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" } };
        //textMesh.gameObject.SetActive(false);
        textMesh.text = "Welcome!\nMove around with ASDW, look around with the mouse," +
            "\nascend with SHIFT, descend with Ctrl," +
            "\nmove the pointe with the arrow keys and press SPACE to mark." +
            "\n Enjoy!" +
            "\nPress Enter to begin.";
        gameOver = false;
        pointer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textMesh.IsActive())
        {
            if (Input.GetKey(KeyCode.KeypadEnter) && gameOver == false)
            {
                textMesh.gameObject.SetActive(false);
                pointer.gameObject.SetActive(true);
            }
            if (Input.GetKey(KeyCode.KeypadEnter) && gameOver == true)
            {
                ResetGame();
            }
            if (Input.GetKey(KeyCode.Escape) && gameOver == true)
            {
                return;
            }
        }

    }

    public void checkStatus(string sign, string childName)
    {
        Regex regex = new Regex("Box(\\d+)(\\d+)");
        Match match = regex.Match(childName);
        int firstNumber = int.Parse(match.Groups[1].Value);
        int secondNumber = int.Parse(match.Groups[2].Value);
        board[firstNumber][secondNumber] = sign;

        if (sign == "X")
            pieceToMove = "O";
        else
            pieceToMove = "X";
        logBoard();
        if (VictoryFor("X")){
            gameOver = true;
            pointer.gameObject.SetActive(false);
            textMesh.gameObject.SetActive(true);
            textMesh.text = "Victory for X!\n" +
                "Go again?";
        }
        if(VictoryFor("O"))
        {
            gameOver = true;
            pointer.gameObject.SetActive(false);
            textMesh.gameObject.SetActive(true);
            textMesh.text = "Victory for O!\n" +
                "Go again?";
        }
        if (BoardFull())
        {
            gameOver = true;
            pointer.gameObject.SetActive(false);
            textMesh.gameObject.SetActive(true);
            textMesh.text = "TIE!\n" +
                "Go again?";
        }
    }
    private void logBoard()
    {
        string text = "";
        foreach(int i in new[] { 0, 1, 2 })
        {
            foreach (int j in new[] { 0, 1, 2 })
            {
                text += board[i][j] + " ";
            }
            text += '\n';
        }
        text += "Last piece moved: " + pieceToMove;
        Debug.Log(text);
    }
    private bool BoardFull()
    {
        if (VictoryFor("X"))
        {
            gameOver = true;
            pointer.gameObject.SetActive(false);
            textMesh.gameObject.SetActive(true);
            textMesh.text = "Victory for X!\n" +
                "Go again?";
            return false;
        }
        if (VictoryFor("O"))
        {
            gameOver = true;
            pointer.gameObject.SetActive(false);
            textMesh.gameObject.SetActive(true);
            textMesh.text = "Victory for O!\n" +
                "Go again?";
            return false;
        }
        foreach (int i in new[] { 0, 1, 2 })
        {
            foreach (int j in new[] { 0, 1, 2 })
            {
                if (board[i][j] == "-")
                {
                    return false;
                }
            }
        }
        return true;
    }
    private bool VictoryFor(string sign)
    {
        foreach (int i in new[] { 0, 1, 2 })
        {
            if ((board[i][0] == sign && board[i][1] == sign && board[i][2] == sign) ||
                (board[0][i] == sign && board[1][i] == sign && board[2][i] == sign))
            {
                return true;
            }
            if ((board[0][0] == sign && board[1][1] == sign && board[2][2] == sign) ||
            (board[0][2] == sign && board[1][1] == sign && board[2][0] == sign))
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGame()
    {
        //lower pieces
        foreach (Transform child in transform)
        {
            Debug.Log(child.name + "\n");
            Box_Behavior boxScript = child.GetComponent<Box_Behavior>();
            boxScript.lowerPieces();
            /*foreach (string sign in new[] { "X", "O" })
            {
                Transform piece = child.Find(sign);
                Debug.Log(piece.name + "\n");
                if (piece.position.y>4)
                {
                    piece.position = new Vector3(child.position.x, -5.5f, child.position.z);
                }
            }*/
        }
        board = new string[][] { new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" } };
        //textMesh.gameObject.SetActive(false);
        textMesh.text = "Welcome!\nMove around with ASDW, look around with the mouse," +
            "\nascend with SHIFT, descend with Ctrl," +
            "\nmove the poieces with the arrow keys. Enjoy!" +
            "\nPress Enter to begin.";
        gameOver = false;
        pieceToMove = "X";
    }
}
