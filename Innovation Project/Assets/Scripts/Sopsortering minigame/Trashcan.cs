using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer test;
    MeshRenderer testTwo;

    void Start()
    {
        test = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            testTwo = collision.gameObject.GetComponent<MeshRenderer>();
            if (test.material.color == testTwo.material.color)
            {
                Debug.Log("Right");
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Wrong");
            }
        }
    }
}
