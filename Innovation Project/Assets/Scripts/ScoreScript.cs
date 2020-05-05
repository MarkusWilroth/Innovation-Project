using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript
{
    public static ScoreScript scoreScript;

    public PlayerScript[] playerScripts;

    public void StartWorld()
    {
        playerScripts = new PlayerScript[4];
    }

    public void UpdatePlayers(int playerNr, int points, int gold)
    {
        playerScripts[playerNr].points = points;
        playerScripts[playerNr].gold = gold;
    }
}
