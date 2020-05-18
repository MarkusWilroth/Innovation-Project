﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class BoardPlayerScript : MonoBehaviour
{
    private enum PlayerPhase
    {
        itemPhase,
        rollPhase,
        eventPhase,
        endPhase
    }

    public float movementSpeed;

    private PlayerPhase playerPhase;
    private bool isPlayerTurn, hasTarget;
    private Gameboard gameboard;

    private List<GameObject> stepList; //De steg karaktären kommer gå till (För att den inte ska gena vid korsningar)
    private GameObject[] allSteps;
    private GameObject targetStep;

    private int moveLength; //How many steps to move

    // Start is called before the first frame update
    void Start()
    {
        stepList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {

            switch (playerPhase)
            {
                case PlayerPhase.itemPhase:
                    //Check if use item or roll (or both)
                    //Debug.Log("Didn't use Item");
                    playerPhase = PlayerPhase.rollPhase;
                    break;
                case PlayerPhase.rollPhase:
                    //Roll die and Move to next step
                    if (!hasTarget)
                    {
                        moveLength = RollDie();
                        if (moveLength <= 0 || moveLength >= 6)
                        {
                            Debug.Log("MoveLength: " + moveLength);
                        }
                        
                        for (int i = 1; i < moveLength; i++) //Tar fram alla steg karaktären kommer gå till för att nå målet
                        {
                            foreach (GameObject step in allSteps)
                            {
                                if (step.GetComponent<StepScript>().stepId == GetComponent<PlayerScript>().stepId + i)
                                {
                                    stepList.Add(step);
                                    targetStep = step;
                                    //Debug.Log("Added a step!");
                                    break;
                                }
                            }
                        }

                        hasTarget = true;
                    } else
                    {
                        if (stepList.Count != 0)
                        {
                            
                            Vector3 targetPos = stepList[0].transform.position; //går mot första målet i listan
                            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);

                            if (transform.position == targetPos) //Den har nått sin target!
                            {
                                stepList.Remove(stepList[0]); //Ser till att den går till nästa target
                                GetComponent<PlayerScript>().stepId = targetStep.GetComponent<StepScript>().stepId;
                            }
                        } else //Den har gått till alla sina target (Den är färdig
                        {
                            playerPhase = PlayerPhase.eventPhase;
                        }

                    }
                    break;
                case PlayerPhase.eventPhase:
                    //Check if event happens on step
                    //Debug.Log("Implement eventPhase");
                    playerPhase = PlayerPhase.endPhase;
                    break;
                case PlayerPhase.endPhase:
                    //Debug.Log("Implement endPhase");
                    EndTurn();
                    //Use items or end turn (or both)
                    break;
            }
        }

    }
    private void EndTurn()
    {
        isPlayerTurn = false;
        gameboard.NextTurn();
    }
    public void SpawnCharacter(Gameboard gameboard, GameObject[] allSteps)
    {
        this.gameboard = gameboard;
        this.allSteps = allSteps;
        hasTarget = false;
    }

    public void GetTurn()
    {
        isPlayerTurn = true;
        hasTarget = false;
        playerPhase = PlayerPhase.itemPhase;
    }

    public int RollDie()
    {
        int dieRoll = Random.Range(1, 6); //Tar positionen den står på och lägger till tärningsslaget för att få fram nästa steg
        return (dieRoll);
    }
}
