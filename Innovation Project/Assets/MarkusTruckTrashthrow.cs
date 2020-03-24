using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusTruckTrashthrow : MonoBehaviour
{
    //ToDo:
    //Få bilen att slänga sopor

    public GameObject trash, debris;
    public float dropMin, dropMax, bumpMin, bumpMax; //tid mellan drops och bumps (När ett bump händer släpps massor med trash på en o samma gång)
    public int goodDropRatio; //kommer den droppa rätt? 0-100
    public int roadMin, roadMax; //Vart kan de landa

    private Vector3 debrisTargetPos;
    private GameObject holder;
    private Vector3 trashDir;
    private MarkusTruckMovement movementScript;
    private float dropTimer, bumpTimer;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<MarkusTruckMovement>();
        dropTimer = Random.Range(dropMin, dropMax);
        bumpTimer = Random.Range(bumpMin, bumpMax);

        trashDir = transform.position;
        trashDir.z = -60;
        
    }

    // Update is called once per frame
    void Update()
    {
        dropTimer -= Time.deltaTime;
        bumpTimer -= Time.deltaTime;

        if (dropTimer <= 0)
        {
            MakeDrop();
            dropTimer = Random.Range(dropMin, dropMax);
        }

        if (bumpTimer <= 0)
        {
            //Släppa massor med trash
            for (int i = 0; i <= 10; i++)
            {
                MakeDrop();
            }
            bumpTimer = Random.Range(bumpMin, bumpMax);
        }
    }

    private void MakeDrop()
    {
        //Göra ett drop
        if (Random.Range(0, 100) <= goodDropRatio)
        {
            holder = Instantiate(trash);
        }
        else
        {
            holder = Instantiate(debris);
        }

        holder.transform.SetParent(GameObject.FindGameObjectWithTag("GameObjects").transform, false);
        Vector3 spawnPos = transform.position;
        spawnPos.z -= 5;
        holder.transform.position = spawnPos;
        trashDir.x = Random.Range(roadMin, roadMax);

        holder.GetComponent<MarkusDumpScript>().GetDir(trashDir);
    }
}
