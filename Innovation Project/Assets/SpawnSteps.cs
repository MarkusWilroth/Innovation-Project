using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Step;

public class SpawnSteps : MonoBehaviour
{
    public string fileName;
    public GameObject GroundStep, ShopStep, StartStep;
    public float startX, startZ, stepSpace;
    private Vector3 spawnPos;
    private GameObject holder;



    private List<GameObject> stepList, toGetId, oldStepList;

    void Start()
    {
        stepList = new List<GameObject>();
        toGetId = new List<GameObject>();
        spawnPos = new Vector3(startX, 0, startZ);
        StreamReader sr = new StreamReader(Application.dataPath + "/ConseptArt/" + fileName + ".txt");
        string fileContents = sr.ReadToEnd();
        sr.Close();

        Debug.Log("Hmm: " + fileContents);

        for (int i = 0; i < fileContents.Length; i++)
        {
            if (fileContents[i] == 'y')
            {
                SpawnStep(StartStep, StepType.startStep);
            }
            else if (fileContents[i] == 's')
            {
                SpawnStep(ShopStep, StepType.shopStep);
            }
            else if (fileContents[i] == 'x')
            {
                SpawnStep(GroundStep, StepType.step);
            }
            if (fileContents[i] == '|')
            {
                spawnPos.x = startX;
                spawnPos.z += stepSpace;

            }
            else
            {
                spawnPos.x += stepSpace;
            }
        }
    }

    private void SpawnStep(GameObject step, StepType stepType)
    {
        holder = Instantiate(step);
        holder.transform.position = spawnPos;
        holder.transform.SetParent(gameObject.transform, false);
        holder.name = step.name;
        holder.GetComponent<StepScript>().stepType = stepType;
        toGetId.Add(holder);
    }

    private void AssignId()
    {
        int idCounter = 0;
        int checkOrder = 0;
        bool hasStart = false;
        Vector3 stepPos = new Vector3(0, 0, 0);
        Vector3 testPos = new Vector3(0, 0, 0);

        while (toGetId != null)
        {
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
                        break;
                    }
                }
            } else
            {
                foreach (GameObject step in toGetId)
                {
                    switch(checkOrder)
                    {
                        case 0:
                            testPos = new Vector3(holder.transform.position.x + stepSpace, holder.transform.position.y, holder.transform.position.z);
                            break;
                        case 1:
                            testPos = new Vector3(holder.transform.position.x, holder.transform.position.y, holder.transform.position.z - stepSpace);
                            break;
                        case 2:
                            testPos = new Vector3(holder.transform.position.x, holder.transform.position.y, holder.transform.position.z + stepSpace);
                            break;
                        case 3:
                            testPos = new Vector3(holder.transform.position.x - stepSpace, holder.transform.position.y, holder.transform.position.z);
                            break;
                        default:
                            Debug.Log("Error: next step not found! step: " + idCounter);
                            break;
                    }
                    if (step.transform.position == testPos)
                    {
                        step.GetComponent<StepScript>().stepId = idCounter;
                        idCounter++;

                        holder = step; //Det aktiva steget
                        stepList.Add(step); //Lägger till första steget
                        toGetId.Remove(step); //Tar bort steget så att den inte kollas igen
                        checkOrder = 0;
                        break;
                    }
                }
                checkOrder++;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
