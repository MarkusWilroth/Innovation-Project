using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class Gameboard : MonoBehaviour
{
    public GameObject characterPrefab;
    public int testCharacters;

    private List<GameObject> characterList;
    private GameObject[] steps, characterOrder;
    private GameObject holder, startStep;

    private int playerTurn;

    

    /*ToDo:
     * Fixa turns
     * Fixa movement
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
            for (int i = 0; i < testCharacters; i++)
            {
                holder = Instantiate(characterPrefab);
                holder.GetComponent<PlayerScript>().stepId = startStep.GetComponent<StepScript>().stepId;
                holder.GetComponent<PlayerScript>().playerNr = i;
                holder.GetComponent<BoardPlayerScript>().SpawnCharacter(this, steps);
                characterList.Add(holder);
                startStep.GetComponent<StepScript>().AddCharacter(holder);
            }
            GetPLayerOrder();
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
        Debug.Log("PlayerTurn["+playerTurn+"]: " + characterOrder[playerTurn].GetComponent<PlayerScript>().playerNr);
        characterOrder[playerTurn].GetComponent<BoardPlayerScript>().GetTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
