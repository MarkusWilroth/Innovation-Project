using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript
{
    public static ScoreScript scoreScript;

    public List<PlayerScript> playerScripts;
    public PlayerScript playerScript;

    public void StartWorld()
    {
        playerScripts = new List<PlayerScript>();
    }

    public void UpdatePlayers(int playerNr, int points, int gold)
    {
        playerScripts[playerNr].points = points;
        playerScripts[playerNr].gold = gold;
    }

    public void LoadPlayerScript(PlayerScript playerScript)
    {
        //playerScript.playerNr = playerNr;

        //playerScript.limb = limb;
        //playerScript.skin = skin;
        //playerScript.armor = armor;
        //playerScript.component = component;
        //playerScript.strName = name;

        if (playerScripts != null)
        {
            foreach (PlayerScript player in playerScripts)
            {
                if (player.playerNr == playerScript.playerNr)
                {
                    playerScripts.Remove(player);
                    break;
                }
            }
        }
        playerScripts.Add(playerScript);

        //playerScripts[playerNr - 1].playerNr = playerNr;

        //playerScripts[playerNr - 1].limb = limb;
        //playerScripts[playerNr - 1].skin = skin;
        //playerScripts[playerNr - 1].armor = armor;
        //playerScripts[playerNr - 1].component = component;
        //playerScripts[playerNr - 1].strName = name;

    }
}
