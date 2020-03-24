using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayer4Controller : MonoBehaviour
{
    // Start is called before the first frame update
    float leftAxis;
    float forwardAxis;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = Input.GetAxisRaw("JoyHorizontal4");
        forwardAxis = Input.GetAxisRaw("JoyVertical4");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardAxis);
        transform.Translate(Vector3.right * Time.deltaTime * leftAxis);
    }
}
