using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Character;
    public Vector3[] spawnpos;
    private bool isTesting;
    private GameObject holder;

    private void Start()
    {
        if (ScoreScript.scoreScript == null)
        {
            isTesting = true;
        }

        if (!isTesting)
        {
            int i = 0;
            foreach (PlayerScript player in ScoreScript.scoreScript.playerScripts)
            {
                if (player.playerNr != 0)
                {
                    
                    holder = Instantiate(Character);
                    holder.transform.position = spawnpos[i];

                    holder.GetComponent<PlayerScript>().LoadCharacter(player.playerNr, player.limb, player.skin, player.armor, player.component, player.strName);
                    
                    holder.transform.SetParent(gameObject.transform, false);
                    holder.GetComponent<GamepadPlayerController>().player = player.playerNr;
                }
            }
        } else
        {
            for (int i = 0; i < 4; i++)
            {
                holder = Instantiate(Character);
                holder.transform.position = spawnpos[i];

                holder.transform.SetParent(gameObject.transform, false);
                holder.GetComponent<GamepadPlayerController>().player = i + 1;
            }
        }
    }

}
