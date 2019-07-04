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

    private GameMode currentGameMode;
    private bool isGameFinished;

    private void Awake()
    {
        if (board == null)
            board = FindObjectOfType<Board>();
    }

    public void StartSingleplayer()
    {
        currentGameMode = gameModes.Find(gm => gm.GetType() == typeof(SingleplayerMode));
        StartGame();
    }

    public void StartMultiplayer()
    {
        currentGameMode = gameModes.Find(gm => gm.GetType() == typeof(MultiplayerMode));
        StartGame();
    }

    public void StartGame()
    {
        isGameFinished = false;
        board.Setup();
        currentGameMode.Setup(board, algorithm);
    }

    public void MakeMove(string boardIndex)
    {
        if (isGameFinished)
            return;

        int x = int.Parse(boardIndex.ToCharArray()[0].ToString());
        int y = int.Parse(boardIndex.ToCharArray()[1].ToString());

        Move move = new Move(x, y);
        bool turnFinishesGame = currentGameMode.RunTurn(move);

        if (turnFinishesGame)
            CheckGameResult();

        //if (board.HasLineCrossed() || board.IsFull())
        //    CheckGameResult();
    }

    private void CheckGameResult()
    {
        string gameResult = currentGameMode.GetGameResult(board.WinnerLetter);
        GameFinished(gameResult);
    }

    private void GameFinished(string result)
    {     
        onGameFinished?.Invoke(result);
        isGameFinished = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}