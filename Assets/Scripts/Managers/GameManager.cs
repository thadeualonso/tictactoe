using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] MinMaxAlgorithm algorithm;
    [SerializeField] BoardManager board;

    [Header("Settings")]
    [SerializeField] List<GameMode> gameModes;
    [SerializeField] float gameFinishDelay = 2f;

    [Header("Events")]
    [SerializeField] UnityEventString onGameFinished;
    [SerializeField] UnityEvent onScore;

    private GameMode currentGameMode;
    private bool isGameFinished;

    private void Awake()
    {
        if (board == null)
            board = FindObjectOfType<BoardManager>();
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
    }

    private void CheckGameResult()
    {
        string gameResult = currentGameMode.GetGameResult(board.WinnerLetter);

        if (gameResult != "Draw!")
            onScore.Invoke();

        StartCoroutine(RunAfterTime(() => GameFinished(gameResult), gameFinishDelay));
    }

    private IEnumerator RunAfterTime(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method.Invoke();
    }

    private void GameFinished(string result)
    {     
        onGameFinished?.Invoke(result);
        isGameFinished = true;
    }
}