using Microsoft.Extensions.FileSystemGlobbing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Game_Tracker : MonoBehaviour
{
    // Start is called before the first frame update

    private string[][] board;
    void Start()
    {
        board = new string[][] { new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" }, new string[] { "-", "-", "-" } };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkStatus()
    {
        if(Victory()){

        }
        if (BoardFull())
        {

        }
        foreach (Transform child in transform)
        {
            Regex regex = new Regex("Box(\\d+)(\\d+)");
            Match match = regex.Match(child.name);
            int firstNumber= int.Parse(match.Groups[1].Value);
            int secondNumber = int.Parse(match.Groups[2].Value);
            
            if (Raised(child.Find("X")))
            {
                board[firstNumber][secondNumber] = "X";
            }
            if (Raised(child.Find("O")))
            {
                board[firstNumber][secondNumber] = "O";
            }
        }
        logBoard();
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
        Debug.Log(text);
    }
    private bool BoardFull()
    {
        return false;
    }
    private bool Victory()
    {
        return false; 
    }

    private bool Raised(Transform piece)
    {
        if (piece.name == "X" && piece.position.y > 0) { return true; }
        if (piece.name == "O" && piece.position.y > 10) { return true; }
        return false;
    }
}
