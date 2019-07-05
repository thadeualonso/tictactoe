using System;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField] private string playerName;
    [SerializeField] private char letter;

    public string PlayerName { get { return playerName; } private set { playerName = value; } }
    public char Letter { get { return letter; } private set { letter = value; } }
}