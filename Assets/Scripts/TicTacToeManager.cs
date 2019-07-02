using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TicTacAI;

public class TicTacToeManager : MonoBehaviour
{
    public const string RESULT_PLAYER  = "Player wins!";
    public const string RESULT_AI      = "AI wins!";
    public const string RESULT_DRAW    = "Draw!";

    [SerializeField] TicTacAI ai;
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject pnlFinish;

    private char[,] board;
    private Button[,] buttonArray;
    private bool isGameFinished;
    private bool drawLastRound;

    public void StartGame()
    {
        isGameFinished = false;
        SetupBoard();
        SetupButtonArray();

        if(drawLastRound)
        {
            AIMove();
            drawLastRound = false;
        }
    }

    private void SetupBoard()
    {
        board = new char[3, 3];

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                board[row, col] = '_';
            }
        }
    }

    private void SetupButtonArray()
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

        ClearBoard();
    }

    private void ClearBoard()
    {
        foreach (Button button in buttonArray)
        {
            button.interactable = true;
            Text buttonText     = button.GetComponentInChildren<Text>();
            buttonText.text     = "";
        }
    }

    public void MakeMove(string boardIndex)
    {
        if (isGameFinished)
            return;

        int x = int.Parse(boardIndex.ToCharArray()[0].ToString());
        int y = int.Parse(boardIndex.ToCharArray()[1].ToString());

        board[x, y] = ai.PlayerLetter;
        buttonArray[x, y].GetComponentInChildren<Text>().text = ai.PlayerLetter.ToString();

        AIMove();

        if (RowCrossed() == ai.PlayerLetter)
            GameFinished(RESULT_PLAYER);
        else if (RowCrossed() == ai.AiLetter)
            GameFinished(RESULT_AI);

        if (ColumnCrossed() == ai.PlayerLetter)
            GameFinished(RESULT_PLAYER);
        else if (ColumnCrossed() == ai.AiLetter)
            GameFinished(RESULT_AI);

        if (DiagonalCrossed() == ai.PlayerLetter)
            GameFinished(RESULT_PLAYER);
        else if (DiagonalCrossed() == ai.AiLetter)
            GameFinished(RESULT_AI);
    }

    private void AIMove()
    {
        Move bestMove = ai.FindBestMove(board, 1);

        if (bestMove.x == -1)
        {
            GameFinished(RESULT_DRAW);
            return;
        }

        board[bestMove.x, bestMove.y] = ai.AiLetter;
        Text buttonText = buttonArray[bestMove.x, bestMove.y].GetComponentInChildren<Text>();
        buttonText.text = ai.AiLetter.ToString();
        buttonArray[bestMove.x, bestMove.y].interactable = false;
    }

    private void GameFinished(string result)
    {
        if (result == RESULT_DRAW)
            drawLastRound = true;

        pnlFinish.GetComponent<CanvasGroup>().alpha = 1f;
        pnlFinish.GetComponent<CanvasGroup>().blocksRaycasts = true;
        pnlFinish.GetComponentInChildren<TextMeshProUGUI>().text = result;
        isGameFinished = true;
    }

    private char RowCrossed()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == '_')
                continue;

            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return board[i,0];
        }

        return '_';
    }

    private char ColumnCrossed()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] == '_')
                continue;

            if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return board[0, i];
        }

        return '_';
    }

    private char DiagonalCrossed()
    {
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[1,1] != '_')
            return board[0,0];

        if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2] && board[1, 1] != '_')
            return board[2,0];

        return '_';
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
