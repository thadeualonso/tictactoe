using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/Singleplayer", fileName = "NewGameMode")]
public class SingleplayerMode : GameMode
{
    protected override void CheckFirstPlayer()
    {
        if(drawLastRound)
        {
            Move randomMove = new Move(Random.Range(0, 3), Random.Range(0, 3));
            AIMove(randomMove);
            drawLastRound = false;
        }
    }

    public override bool RunTurn(Move move)
    {
        bool isGameFinished = false;

        isGameFinished = board.MakeMove(player1, move);

        Move aiMove = algorithm.FindBestMove(board.BoardMatrix);

        if (!isGameFinished)
            isGameFinished = AIMove(aiMove);

        return isGameFinished;
    }

    private bool AIMove(Move move)
    {
        if (move.X == -1)
            return true;

        return board.MakeMove(player2, move);
    }
}