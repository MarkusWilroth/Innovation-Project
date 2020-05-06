using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnSteps : MonoBehaviour
{
    // Start is called before the first frame update

    public string fileName;
    public GameObject GroundStep, ShopStep, StartStep;
    public float startX, startZ, stepSpace;
    private Vector3 spawnPos;
    private GameObject holder;

    private List<GameObject> stepList, toGetId;

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
                SpawnStep(StartStep);
            } else if (fileContents[i] == 's')
            {
                SpawnStep(ShopStep);
            } else if (fileContents[i] == 'x')
            {
                SpawnStep(GroundStep);
            }
            if (fileContents[i] == '|')
            {
                spawnPos.x = startX;
                spawnPos.z += stepSpace;

            } else
            {
                spawnPos.x += stepSpace;
            }
        }
    }

    private void SpawnStep(GameObject step)
    {
        holder = Instantiate(step);
        holder.transform.position = spawnPos;
        //holder.transform.SetParent(gameObject.transform, false);
        toGetId.Add(holder);
    }

    private void AssignId()
    {
        int idCounter = 0;
        Vector3 stepPos = new Vector3(0, 0, 0);

        while (toGetId != null)
        {
            foreach (GameObject step in toGetId)
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
