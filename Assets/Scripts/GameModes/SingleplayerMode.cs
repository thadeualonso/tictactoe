using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/Singleplayer", fileName = "NewGameMode")]
public class SingleplayerMode : GameMode
{
    protected override void CheckFirstPlayer()
    {
        if(drawLastRound)
        {
            AIMove();
            drawLastRound = false;
        }
    }

    public override bool RunTurn(Move move)
    {
        bool isGameFinished = false;

        isGameFinished = board.MakeMove(player1, move);

        if(!isGameFinished)
            isGameFinished = AIMove();

        return isGameFinished;
    }

    private bool AIMove()
    {
        Move aiMove = algorithm.FindBestMove(board.BoardMatrix, 1);

        if (aiMove.X == -1)
            return true;

        return board.MakeMove(player2, aiMove);
    }
}