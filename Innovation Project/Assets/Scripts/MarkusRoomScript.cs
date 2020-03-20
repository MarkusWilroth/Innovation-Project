using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusRoomScript : MonoBehaviour
{
    //ToDo: Spawn Truck, Spawn Players
    public GameObject truckObj, playerObj; //Truck och spelarna, laddas in från unity
    public Vector3 truckSpawnPos, playerSpawnPos; //deras spawns, laddas in från unity

    private GameObject holder; //Ett obj så att man kan ändra saker i obj script efter det har spawnat

    public void Start()
    {
        //Spawnar truck
        holder = Instantiate(truckObj, GameObject.FindGameObjectWithTag("GameObjects").transform);
        holder.transform.position = truckSpawnPos;

        //Spawnar player
        holder = Instantiate(playerObj, GameObject.FindGameObjectWithTag("GameObjects").transform);
        holder.transform.position = playerSpawnPos;
    }
}
