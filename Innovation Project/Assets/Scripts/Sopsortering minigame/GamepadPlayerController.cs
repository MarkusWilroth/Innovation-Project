using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    bool test;
    float leftAxis;
    float forwardAxis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = Input.GetAxisRaw("JoyHorizontal");
        forwardAxis = Input.GetAxisRaw("JoyVertical");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardAxis);
        transform.Translate(Vector3.right * Time.deltaTime * leftAxis);
    }
}
