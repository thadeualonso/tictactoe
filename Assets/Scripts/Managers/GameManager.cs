using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string RESULT_PLAYER = "Player wins!";
    public const string RESULT_AI = "AI wins!";
    public const string RESULT_DRAW = "Draw!";

    [Header("Components")]
    [SerializeField] MinMaxAlgorithm algorithm;
    [SerializeField] Board board;

    [Header("Settings")]
    [SerializeField] List<GameMode> gameModes;

    [Header("Events")]
    [SerializeField] UnityEventString onGameFinished;

    private bool isGameFinished;
    private bool drawLastRound;
    private bool isSingleplayer;
    private Player human;
    private Player ai;

    public void StartSingleplayer()
    {
        isSingleplayer = true;
        StartGame();
    }

    public void StartMultiplayer()
    {
        isSingleplayer = false;
        StartGame();
    }

    public void StartGame()
    {
        isGameFinished = false;
        board.Setup();

        human = new Player(algorithm.PlayerLetter, false);
        ai = new Player(algorithm.AiLetter, true);

        if (drawLastRound)
        {
            Move aiMove = new Move(Random.Range(0, 3), Random.Range(0, 3));
            board.MakeMove(ai, aiMove);
            drawLastRound = false;
        }
    }

    public void MakeMove(string boardIndex)
    {
        if (isGameFinished)
            return;

        int x = int.Parse(boardIndex.ToCharArray()[0].ToString());
        int y = int.Parse(boardIndex.ToCharArray()[1].ToString());

        Move move = new Move(x, y);

        bool gameFinished = board.MakeMove(human, move);

        if(gameFinished)
        {
            CheckGameResult(board.WinnerLetter);
            return;
        }
        else
        {
            if (isSingleplayer)
            {
                Move aiMove = algorithm.FindBestMove(board.BoardMatrix, 1);

                if (aiMove.X == -1)
                {
                    GameFinished(RESULT_DRAW);
                    return;
                }

                bool gameFinish = board.MakeMove(ai, aiMove);

                if (gameFinish)
                {
                    CheckGameResult(board.WinnerLetter);
                    return;
                }
            }
        }
    }

    private void CheckGameResult(char winner)
    {
        if (winner == algorithm.PlayerLetter)
        {
            GameFinished(RESULT_PLAYER);
            return;
        }
        else if (winner == algorithm.AiLetter)
        {
            GameFinished(RESULT_AI);
            return;
        }
        else
        {
            GameFinished(RESULT_DRAW);
            return;
        }
    }

    private void GameFinished(string result)
    {
        if (result == RESULT_DRAW)
            drawLastRound = true;

        onGameFinished?.Invoke(result);
        isGameFinished = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}