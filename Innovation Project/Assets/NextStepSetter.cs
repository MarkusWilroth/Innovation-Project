using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class NextStepSetter : MonoBehaviour
{
    private GameObject oldStep;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform step in transform)
        {
            if (oldStep != null)
            {
                oldStep.GetComponent<StepScript>().nextStep[0] = step.gameObject;
                
            }
            oldStep = step.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
