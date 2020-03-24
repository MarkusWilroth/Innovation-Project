using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayer3Controller : MonoBehaviour
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
        leftAxis = Input.GetAxisRaw("JoyHorizontal3");
        forwardAxis = Input.GetAxisRaw("JoyVertical3");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardAxis);
        transform.Translate(Vector3.right * Time.deltaTime * leftAxis);
    }
}
