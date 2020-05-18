using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class CreateStepOrder : MonoBehaviour
{
    public float stepSpace;

    private GameObject holder;
    private List<GameObject> stepList, toGetId;
    private float timer;
    
    void Start()
    {
        stepList = new List<GameObject>();
        toGetId = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
        {
            toGetId.Add(child.gameObject);
        }

        AssignID();

        SpawnInOrder();
    }
    
    private void AssignID()
    {
        int idCounter = 0;
        int checkOrder = 0;
        bool hasStart = false;
        bool didChange = false;
        Vector3 stepPos = new Vector3(0, 0, 0);
        Vector3 testPos = new Vector3(0, 0, 0);
        timer = 2000f;

        while (toGetId != null)
        {
            didChange = false;
            timer--;
            if (timer <= 0)
            {
                Debug.Log("step halted at: step " + idCounter);
                break;
            }
            if (!hasStart)
            {
                foreach (GameObject step in toGetId)
                {
                    if (step.name == "StartStep") //Det steg den startar ifrån
                    {
                        
                        step.GetComponent<StepScript>().stepId = idCounter;
                        idCounter++;

                        holder = step; //Det aktiva steget
                        stepList.Add(step); //Lägger till första steget
                        hasStart = true; //Kollar inte längre efter start steget
                        toGetId.Remove(step); //Tar bort steget så att den inte kollas igen
                        didChange = true;
                        Destroy(step);
                        break;
                    }
                }
            }
            else
            {
                //Debug.Log("Holder pos: " + holder.transform.position);
                foreach (GameObject step in toGetId)
                {
                    switch (checkOrder)
                    {
                        case 0:
                            testPos = new Vector3((holder.transform.position.x + stepSpace), holder.transform.position.y, holder.transform.position.z);
                            //Debug.Log("testPos: " + testPos);
                            break;
                        case 1:
                            testPos = new Vector3(holder.transform.position.x, holder.transform.position.y, (holder.transform.position.z - stepSpace));
                            break;
                        case 2:
                            testPos = new Vector3(holder.transform.position.x, holder.transform.position.y, (holder.transform.position.z + stepSpace));
                            break;
                        case 3:
                            testPos = new Vector3((holder.transform.position.x - stepSpace), holder.transform.position.y, holder.transform.position.z);
                            break;
                        default:
                            //Debug.Log("Error: next step not found! step: " + idCounter + "Checkorder: " + checkOrder);
                            break;
                    }
                    //Debug.Log("-- testPos: " + testPos + "\n-- stepPos: " + step.transform.position);
                    if (step.transform.position == testPos)
                    {
                        step.GetComponent<StepScript>().stepId = idCounter;
                        idCounter++;

                        holder = step; //Det aktiva steget
                        stepList.Add(step); //Lägger till första steget
                        toGetId.Remove(step); //Tar bort steget så att den inte kollas igen
                        checkOrder = 0;
                        didChange = true;
                        Destroy(step);
                        break;
                    }
                }
                //Debug.Log("Hmmmmmm");
                if(!didChange)
                {
                    checkOrder++;
                    if (checkOrder >= 4)
                    {
                        toGetId.Clear();
                        break;
                    }
                }
            }
        }
    }

    private void SpawnInOrder()
    {
        foreach (GameObject step in stepList)
        {
            holder = Instantiate(step);
            holder.name = "Step: " + step.GetComponent<StepScript>().stepId;
            holder.transform.SetParent(gameObject.transform, false);
        }
        stepList.Clear();
    }
}
