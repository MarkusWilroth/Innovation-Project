using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int holder;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.useGravity = false;
            if (collision.gameObject.name == "Player1")
            {
                holder = 1;
            }
            if (collision.gameObject.name == "Player2")
            {
                holder = 2;
            }
            if (collision.gameObject.name == "Player3")
            {
                holder = 3;
            }
            if (collision.gameObject.name == "Player4")
            {
                holder = 4;
            }
        }
      
    }
}
