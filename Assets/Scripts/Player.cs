using System;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField] private string playerName;
    [SerializeField] private char letter;
    [SerializeField] private bool isAI;

    public string PlayerName { get { return playerName; } private set { playerName = value; } }
    public char Letter { get { return letter; } private set { letter = value; } }
    public bool IsAI { get { return isAI; } private set { isAI = value; } }

    public Player(string playerName, char letter, bool isAI)
    {
        PlayerName = playerName;
        Letter = letter;
        IsAI = isAI;
    }
}