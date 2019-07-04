using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMode", fileName = "NewGameMode")]
public class GameMode : ScriptableObject
{
    [Serializable]
    public class Player
    {
        public bool isAI;
        public char letter;
    }

    [SerializeField] List<Player> players;
}