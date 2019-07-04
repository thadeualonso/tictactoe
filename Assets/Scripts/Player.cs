using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public char Letter { get; private set; }
    public bool IsAI { get; private set; }

    public Player(char letter, bool isAI)
    {
        Letter = letter;
        IsAI = isAI;
    }
}