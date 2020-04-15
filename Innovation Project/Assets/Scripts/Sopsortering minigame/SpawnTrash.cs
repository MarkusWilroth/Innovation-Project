﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{

    public GameObject[] trash;
    int x;
    float randomXSpawn;
    float randomZSpawn;
    public float startDelay = 2.0f;
    public float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        randomXSpawn = Random.Range(-4f, 4f);
        randomZSpawn = Random.Range(-4f, 4f);

        Vector3 spawnPos = new Vector3(randomXSpawn, 5, randomZSpawn);

        x = Random.Range(0, trash.Length);

        Instantiate(trash[x], spawnPos, trash[x].transform.rotation);
    }
}
