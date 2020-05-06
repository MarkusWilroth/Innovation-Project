using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectScritp : MonoBehaviour
{
    private int[] connectedControllers = new int[] { 0,0,0,0};
    public GameObject SetupCharacter;
    public GameObject LblPressToJoin;
    public GameObject LblReady;

    private Vector3[] characterSpawnPos = new Vector3[4];
    private GameObject[] pressHolder;
    private GameObject holder;

    private Vector2 lblPos;

    private void Start()
    {
        pressHolder = new GameObject[characterSpawnPos.Length];
        for (int i = 0; i < characterSpawnPos.Length; i++)
        {
            characterSpawnPos[i] = new Vector3(-520 + (372 * i), -200, -70);
            pressHolder[i] = Instantiate(LblPressToJoin);
            
            lblPos = new Vector2(characterSpawnPos[i].x - 40, characterSpawnPos[i].y + 220);
            pressHolder[i].transform.position = lblPos;
            pressHolder[i].transform.SetParent(gameObject.transform, false);
        }
    }

    private void Update()
    {
        for (int i = 1; i <= 4; i++)
        {

            if (Input.GetButtonDown(i + "A"))
            {
                if (connectedControllers[i-1] == 0) //Controllern används inte
                {
                    connectedControllers[i - 1] = i;
                    SpawnCharacter(i);
                }
            }
        }
    }

    private void SpawnCharacter(int player)
    {
        holder = Instantiate(SetupCharacter);
        holder.transform.position = characterSpawnPos[player - 1];
        holder.transform.SetParent(gameObject.transform, false);
        holder.GetComponent<SetUpCharacter>().playerNr = player;

        pressHolder[player - 1].SetActive(false);
    }
}
