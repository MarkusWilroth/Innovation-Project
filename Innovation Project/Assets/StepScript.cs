using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step
{
    public enum StepType
    {
        startStep,
        shopStep,
        step
    }
}

public class StepScript : MonoBehaviour
{
    public int stepId; //Används för att se till att spelaren går frammåt
    public List<GameObject> CharacterOnStepList;
    public Step.StepType stepType;

    

    private void Start()
    {
        CharacterOnStepList = new List<GameObject>();
    }
}

