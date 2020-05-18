using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class Gameboard : MonoBehaviour
{
    public GameObject characterPrefab;
    public int testCharacters;

    private List<GameObject> characterList;
    private GameObject[] steps;
    private GameObject holder, startStep;

    void Start()
    {
        characterList = new List<GameObject>();

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
                characterList.Add(holder);
                startStep.GetComponent<StepScript>().AddCharacter(holder);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
