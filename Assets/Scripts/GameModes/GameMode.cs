using UnityEngine;

public abstract class GameMode : ScriptableObject
{
    [SerializeField] protected Player player1;
    [SerializeField] protected Player player2;

    protected Board board;
    protected MinMaxAlgorithm algorithm;
    protected bool drawLastRound = false;

    public void Setup(Board board, MinMaxAlgorithm algorithm)
    {
        this.board = board;
        this.algorithm = algorithm;
        CheckFirstPlayer();
    }

    protected abstract void CheckFirstPlayer();

    public abstract bool RunTurn(Move move);

    public string GetGameResult(char winner)
    {
        if (winner == player1.Letter)
        {
            return player1.PlayerName + " wins!";
        }
        else if (winner == player2.Letter)
        {
            return player2.PlayerName + " wins!";
        }
        else
        {
            drawLastRound = true;
            return "Draw!";
        }
    }

    protected bool IsGameFinished()
    {
        return board.HasLineCrossed() || board.IsFull();
    }
}