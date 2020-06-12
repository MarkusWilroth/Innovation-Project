using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;
using BoardCamera;

public class Gameboard : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject MainCamera;
    public Material testMat;
    public int testCharacters;

    private List<GameObject> characterList;
    private GameObject[] steps, characterOrder;
    private GameObject holder, startStep;

    private int playerTurn;

    

    /*ToDo:
     * Fixa turns
     * Fixa movement
     * Fixa rätt animationer
     * Fixa roation
     */

    void Start()
    {
        characterList = new List<GameObject>();
        playerTurn = 0;

        steps = GameObject.FindGameObjectsWithTag("Step");
        foreach (GameObject step in steps)
        {
            if (step.GetComponent<StepScript>().stepType == StepType.startStep)
            {
                startStep = step;
                break;
            }
        }

        if (ScoreScript.scoreScript == null) //Kommer inte från start menyn (testing)
        {
            ScoreScript.scoreScript = new ScoreScript();
            ScoreScript.scoreScript.StartWorld();

            MainCamera.GetComponent<BoardCameraMovement>().cameraState = BoardCameraMovement.CameraState.introState;

            for (int i = 1; i <= testCharacters; i++)
            {
                holder = Instantiate(characterPrefab);
                holder.GetComponent<PlayerScript>().stepId = startStep.GetComponent<StepScript>().stepId;
                holder.GetComponent<PlayerScript>().playerNr = i;
                holder.GetComponent<BoardPlayerScript>().SpawnCharacter(this, steps);
                characterList.Add(holder);
                startStep.GetComponent<StepScript>().AddCharacter(holder);
                holder.GetComponent<PlayerScript>().CreateCharacter(i, testMat, testMat, testMat, testMat, "Test" + i);
            }
            GetPLayerOrder();
        } else
        {
            MainCamera.GetComponent<BoardCameraMovement>().cameraState = BoardCameraMovement.CameraState.loadState; //Kommer behövas en bool i Scorescript
            foreach (PlayerScript playerScript in ScoreScript.scoreScript.playerScripts)
            {
                holder = Instantiate(characterPrefab);
                holder.GetComponent<PlayerScript>().LoadCharacter(playerScript.playerNr, playerScript.limb, playerScript.skin, playerScript.armor, playerScript.component, playerScript.strName);
                holder.GetComponent<PlayerScript>().stepId = startStep.GetComponent<StepScript>().stepId;
                holder.GetComponent<BoardPlayerScript>().SpawnCharacter(this, steps);
                characterList.Add(holder);
                startStep.GetComponent<StepScript>().AddCharacter(holder);
            }
        }

        characterOrder[playerTurn].GetComponent<BoardPlayerScript>().GetTurn();
    }
    private void GetPLayerOrder()
    {
        characterOrder = new GameObject[characterList.Count];

        for (int i = 0; i < characterOrder.Length; i++)
        {
            int rand = Random.Range(0, characterList.Count);
            characterOrder[i] = characterList[rand];
            characterList.Remove(characterList[rand]);
        }
        
    }

    public void NextTurn()
    {
        playerTurn++;
        if (playerTurn >= characterOrder.Length)
        {
            //Start Minigame
            playerTurn = 0;
        }
        //Debug.Log(" - CharacterOrder: " + characterOrder.ToString());
        //Debug.Log("PlayerTurn["+playerTurn+"]: " + characterOrder[playerTurn].GetComponent<PlayerScript>().playerNr);
        characterOrder[playerTurn].GetComponent<BoardPlayerScript>().GetTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
