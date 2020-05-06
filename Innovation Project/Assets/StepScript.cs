using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScript : MonoBehaviour
{
    public int stepId; //Används för att se till att spelaren går frammåt
    public List<GameObject> CharacterOnStepList;

    private void Start()
    {
        CharacterOnStepList = new List<GameObject>();
    }
}
