using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayer2Controller : MonoBehaviour
{
    bool test;
    float leftAxis;
    float forwardAxis;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = Input.GetAxisRaw("JoyHorizontal2");
        forwardAxis = Input.GetAxisRaw("JoyVertical2");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardAxis);
        transform.Translate(Vector3.right * Time.deltaTime * leftAxis);
    }
}

