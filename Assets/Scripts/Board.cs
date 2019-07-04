using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] List<Button> buttons;

    public char[,] BoardMatrix { get; set; }
    public char WinnerLetter { get; set; }

    private Button[,] buttonArray;

    public void Setup()
    {
        buttonArray = new Button[3, 3];

        buttonArray[0, 0] = buttons[0];
        buttonArray[0, 1] = buttons[1];
        buttonArray[0, 2] = buttons[2];
        buttonArray[1, 0] = buttons[3];
        buttonArray[1, 1] = buttons[4];
        buttonArray[1, 2] = buttons[5];
        buttonArray[2, 0] = buttons[6];
        buttonArray[2, 1] = buttons[7];
        buttonArray[2, 2] = buttons[8];

        SetupBoard();
        ClearBoard();
    }

    public void MakeMove(Player player, Move move)
    {
        BoardMatrix[move.X, move.Y] = player.Letter;
        buttonArray[move.X, move.Y].GetComponentInChildren<Text>().text = player.Letter.ToString();
        buttonArray[move.X, move.Y].interactable = false;
    }

    public bool HasLineCrossed()
    {
        if(IsRowCrossed())
        {
            return true;
        }
        else if(IsColumnCrossed())
        {
            return true;
        }
        else if(IsDiagonalCrossed())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFull()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (BoardMatrix[x, y] == '_')
                    return false;
            }
        }

        return true;
    }

    private void SetupBoard()
    {
        BoardMatrix = new char[3, 3];

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                BoardMatrix[row, col] = '_';
            }
        }
    }

    private void ClearBoard()
    {
        foreach (Button button in buttonArray)
        {
            button.interactable = true;
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = "";
        }
    }

    private bool IsRowCrossed()
    {
        for (int i = 0; i < 3; i++)
        {
            if (BoardMatrix[i, 0] == '_')
                continue;

            if (BoardMatrix[i, 0] == BoardMatrix[i, 1] && BoardMatrix[i, 1] == BoardMatrix[i, 2])
            {
                WinnerLetter = BoardMatrix[i, 0];
                return true;
            }
        }

        return false;
    }

    public void ClearWinner()
    {
        WinnerLetter = '\0';
    }

    private bool IsColumnCrossed()
    {
        for (int i = 0; i < 3; i++)
        {
            if (BoardMatrix[0, i] == '_')
                continue;

            if (BoardMatrix[0, i] == BoardMatrix[1, i] && BoardMatrix[1, i] == BoardMatrix[2, i])
            {
                WinnerLetter = BoardMatrix[0, i];
                return true;
            }
        }

        return false;
    }

    private bool IsDiagonalCrossed()
    {
        if (BoardMatrix[0, 0] == BoardMatrix[1, 1] && BoardMatrix[1, 1] == BoardMatrix[2, 2] && BoardMatrix[1, 1] != '_')
        {
            WinnerLetter = BoardMatrix[0, 0];
            return true;
        }

        if (BoardMatrix[2, 0] == BoardMatrix[1, 1] && BoardMatrix[1, 1] == BoardMatrix[0, 2] && BoardMatrix[1, 1] != '_')
        {
            WinnerLetter = BoardMatrix[2, 0];
            return true;
        }

        return false;
    }
}
