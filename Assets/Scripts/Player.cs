using System;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField] private char letter;
    [SerializeField] private bool isAI;

    public char Letter { get { return letter; } private set { letter = value; } }
    public bool IsAI { get { return isAI; } private set { isAI = value; } }

    public Player(char letter, bool isAI)
    {
        Letter = letter;
        IsAI = isAI;
    }
}