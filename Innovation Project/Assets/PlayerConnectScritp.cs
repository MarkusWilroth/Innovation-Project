using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectScritp : MonoBehaviour
{
    private int[] connectedControllers = new int[] { 0,0,0,0};
    public GameObject SetupCharacter;
    public GameObject LblPressToJoin;
    public GameObject LblReady;
    public GameObject MasterMenu;

    private Vector3[] characterSpawnPos = new Vector3[4];
    private GameObject[] pressHolder;
    private GameObject holder;
    private List<GameObject> playerList;

    private Vector2 lblPos;
    private bool isAllReady;

    private void Start()
    {
        playerList = new List<GameObject>();
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

    private void SpawnCharacter(int player) //Kommer behöva föra om denna ifall vi ska kunna disconnecta kontroller
    {
        holder = Instantiate(SetupCharacter);
        holder.transform.position = characterSpawnPos[playerList.Count];
        holder.transform.SetParent(gameObject.transform, false);
        holder.GetComponent<SetUpCharacter>().playerNr = player;

        pressHolder[playerList.Count].SetActive(false);
        playerList.Add(holder);
        
    }

    public void BtnCont()
    {
        isAllReady = true;
        if (playerList.Count <= 0)
        {
            isAllReady = false;
            //No player connected
        } else
        {
            foreach (GameObject player in playerList)
            {
                if (!player.GetComponent<SetUpCharacter>().isReady)
                {
                    //all players are not ready
                    isAllReady = false;
                }
            }
        }
         if (isAllReady)
        {
            MasterMenu.GetComponent<MainMenuScript>().GetToLevelSelect();
        }

    }
}
