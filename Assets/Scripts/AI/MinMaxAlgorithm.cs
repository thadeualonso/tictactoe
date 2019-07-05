using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/MinMax Algorithm", fileName = "MinMaxAlgorithm")]
public class MinMaxAlgorithm : ScriptableObject
{
    public const int MAX_SCORE  = 10;
    public const int MIN_SCORE  = -10;
    public const int TIE_SCORE  = 0;
    public const char PLAYER    = 'X';
    public const char AI        = 'O';
    public const char EMPTY     = '_';

    public char PlayerLetter { get { return PLAYER; } }
    public char AiLetter { get { return AI; } }

    public Move FindBestMove(char[,] board)
    {
        int bestScore = -1000;
        Move bestMove = new Move(-1, -1);

        for (int x = 0; x < board.Length / 3; x++)
        {
            for (int y = 0; y < board.Length / 3; y++)
            {
                if(board[x,y] == EMPTY)
                {
                    board[x, y] = PLAYER;
                    int score = MinMax(0, board, false);
                    board[x, y] = EMPTY;

                    if(score > bestScore)
                    {
                        bestMove = new Move(x, y);
                        bestScore = score;
                    }
                }
            }
        }

        return bestMove;
    }

    private int MinMax(int depth, char[,] board, bool isMax)
    {
        int score = Evaluate(board);

        if (score == MAX_SCORE)
            return score;

        if (score == MIN_SCORE)
            return score;

        if (!IsMoveLefts(board))
            return 0;

        int bestScore = 0;

        for (int x = 0; x < board.Length / 3; x++)
        {
            for (int y = 0; y < board.Length / 3; y++)
            {
                if(board[x,y] == EMPTY)
                {
                    board[x, y] = isMax ? PLAYER : AI;
                    int value = MinMax(depth + 1, board, !isMax);
                    bestScore = isMax ? Mathf.Max(bestScore, value) : Mathf.Min(bestScore, value);
                    board[x, y] = EMPTY;
                }
            }
        }

        return bestScore;
    }

    private bool IsMoveLefts(char[,] board)
    {
        for (int x = 0; x < board.Length / 3; x++)
        {
            for (int y = 0; y < board.Length / 3; y++)
            {
                if (board[x, y] == EMPTY)
                    return true;
            }
        }

        return false;
    }

    private int Evaluate(char[,] board)
    {
        for (int row = 0; row < board.Length / 3; row++)
        {
            if(board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
            {
                switch (board[row, 0])
                {
                    case PLAYER:
                        return MAX_SCORE;
                    case AI:
                        return MIN_SCORE;
                }
            }
        }

        for (int col = 0; col < board.Length / 3; col++)
        {
            if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
            {
                switch (board[0, col])
                {
                    case PLAYER:
                        return MAX_SCORE;
                    case AI:
                        return MIN_SCORE;
                }
            }
        }

        if(board[0,0] == board[1,1] && board[1,1] == board[2,2])
        {
            switch (board[1, 1])
            {
                case PLAYER:
                    return MAX_SCORE;
                case AI:
                    return MIN_SCORE;
            }
        }

        if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
        {
            switch (board[1, 1])
            {
                case PLAYER:
                    return MAX_SCORE;
                case AI:
                    return MIN_SCORE;
            }
        }

        return TIE_SCORE;
    }
}