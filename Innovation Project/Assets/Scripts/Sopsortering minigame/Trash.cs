using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    int holder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name == "PlayerOne")
            {
                holder = 0;
            }
            if (collision.gameObject.name == "PlayerTwo")
            {

            }
            if (collision.gameObject.name == "PlayerThree")
            {

            }
            if (collision.gameObject.name == "PlayerFour")
            {

            }
        }
    }
}
