using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/Multiplayer", fileName = "NewGameMode")]
public class MultiplayerMode : GameMode
{
    private bool isP1turn = true;

    protected override void CheckFirstPlayer()
    {
        if(drawLastRound)
        {
            isP1turn = false;
            drawLastRound = false;
        }
    }

    public override bool RunTurn(Move move)
    {
        bool isGameFinished = false;

        if (isP1turn)
            isGameFinished = board.MakeMove(player1, move);
        else
            isGameFinished = board.MakeMove(player2, move);

        isP1turn = !isP1turn;

        return isGameFinished;
    }
}