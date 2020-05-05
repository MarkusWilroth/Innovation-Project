using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectScritp : MonoBehaviour
{
    private int[] connectedControllers = new int[] { 0,0,0,0};
    public GameObject SetupCharacter;
    public GameObject PressToJoin;
    private Vector3[] characterSpawnPos = new Vector3[4];

    private GameObject holder;

    private void Start()
    {
        for (int i = 0; i < characterSpawnPos.Length; i++)
        {
            characterSpawnPos[i] = new Vector3(-550 + (350 * i), -150, -70);
        }
    }

    private void Update()
    {
        for (int i = 1; i <= 4; i++)
        {

            if (Input.GetButtonDown(i + "A"))
            {
                if (connectedControllers[i-1] == 0) //Controllern används inte
                {
                    connectedControllers[i - 1] = i;
                    SpawnCharacter(i);
                }
            }
        }
    }

    private void SpawnCharacter(int player)
    {
        holder = Instantiate(SetupCharacter);
        holder.transform.position = characterSpawnPos[player - 1];
        holder.transform.SetParent(gameObject.transform, false);
        holder.GetComponent<SetUpCharacter>().playerNr = player;
    }
}
