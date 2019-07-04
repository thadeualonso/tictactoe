using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMode", fileName = "NewGameMode")]
public class GameMode : ScriptableObject
{
    [SerializeField] List<Player> players;

    public void Round()
    {

    }
}