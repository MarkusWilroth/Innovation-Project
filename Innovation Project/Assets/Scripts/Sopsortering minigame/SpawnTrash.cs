using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{

    public GameObject[] trash;
    public GameObject holder;
    AudioSource aS;
    Sounds sound;
    int x;
    float randomXSpawn;
    float randomZSpawn;
    public float startDelay = 2.0f;
    public float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelay, spawnInterval);
        aS = GetComponent<AudioSource>();
        sound = GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        randomXSpawn = Random.Range(-4f, 4f);
        randomZSpawn = Random.Range(-4f, 4f);
        aS.PlayOneShot(sound.nyFlaska, 0.2f);
        Vector3 spawnPos = new Vector3(randomXSpawn, 5, randomZSpawn);

        x = Random.Range(0, trash.Length);

        holder = Instantiate(trash[x], spawnPos, trash[x].transform.rotation);
        switch (x)
        {
            case 0:
                holder.GetComponent<Trash>().trashType = Trash.TrashType.plastic;
                break;
            case 1:
                holder.GetComponent<Trash>().trashType = Trash.TrashType.paper;
                break;
            case 2:
                holder.GetComponent<Trash>().trashType = Trash.TrashType.glass;
                break;
            default:
                Debug.Log("Can't find object for X: " + x);
                break;
        }
    }
}
