using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectScritp : MonoBehaviour
{
    private int[] connectedControllers = new int[] { 0,0,0,0}; //Håller koll på vilka kontroller som är spawnade
    private bool[] occupiedSpot = new bool[] { false, false, false, false }; //Håller koll på vilka positioner som är tagna
    public GameObject SetupCharacter;
    public GameObject LblPressToJoin;
    public GameObject LblReady;
    public GameObject MasterMenu;
    public GameObject CharacterBox;

    private Vector3[] characterSpawnPos = new Vector3[4];
    private GameObject[] pressHolder;
    private GameObject holderCharacter, holderBox;
    private List<GameObject> playerList;

    private Vector2 lblPos;
    private bool isAllReady;

    private void Start()
    {
        playerList = new List<GameObject>();
        pressHolder = new GameObject[characterSpawnPos.Length];
        for (int i = 0; i < characterSpawnPos.Length; i++)
        {
            characterSpawnPos[i] = new Vector3(-615 + (372 * i), -35, 0);
            pressHolder[i] = Instantiate(LblPressToJoin);
            
            lblPos = new Vector2(characterSpawnPos[i].x + 60, characterSpawnPos[i].y + 30);
            pressHolder[i].transform.position = lblPos;
            pressHolder[i].transform.SetParent(gameObject.transform, false);
        }
    }

    private void Update()
    {
        for (int i = 1; i <= 4; i++)
        {

            if (Input.GetButtonDown(i +"A"))
            {
                if (connectedControllers[i-1] == 0) //Controllern används inte
                {
                    connectedControllers[i - 1] = i;
                    SpawnCharacter(i);
                }
            }
        }
    }

    private void UpdateToJoin() //Ändrar så att rätt press A to join visas;
    {
        for (int i = 0; i < occupiedSpot.Length; i++)
        {
            if (occupiedSpot[i])
            {
                pressHolder[i].SetActive(false);
            } else
            {
                pressHolder[i].SetActive(true);
            }
        }
    }

    private void SpawnCharacter(int player) //Kommer behöva föra om denna ifall vi ska kunna disconnecta kontroller
    {
        
        for (int i = 0; i < occupiedSpot.Length; i++)
        {
            if (!occupiedSpot[i]) //Platsen används inte
            {
                holderBox = Instantiate(CharacterBox);
                holderBox.transform.position = characterSpawnPos[i];
                holderBox.transform.SetParent(transform, false);

                holderCharacter = Instantiate(SetupCharacter);
                holderCharacter.GetComponent<SetUpCharacter>().occupiedSlot = i;
                holderCharacter.GetComponent<SetUpCharacter>().setUpBoxScript = holderBox.GetComponent<SetupBox>();
                holderCharacter.transform.position = new Vector3(0, -170, 0);
                holderCharacter.transform.SetParent(holderBox.transform, false);
                
                occupiedSpot[i] = true;
                break;
            }
        }
        
        //holderCharacter.transform.SetParent(gameObject.transform, false);
        holderCharacter.GetComponent<SetUpCharacter>().playerNr = player;
        holderCharacter.GetComponent<SetUpCharacter>().connectScript = this;
        
        playerList.Add(holderCharacter);
        MasterMenu.GetComponent<MenuControllScript>().playerList = playerList;
        UpdateToJoin();
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

    public void DisconnectPlayer(int player, int slot, GameObject playerObj)
    {
        connectedControllers[player - 1] = 0;
        occupiedSpot[slot] = false;
        playerList.Remove(playerObj);
        MasterMenu.GetComponent<MenuControllScript>().playerList = playerList;
        UpdateToJoin();
    }
}
