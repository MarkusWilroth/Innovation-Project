using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hold;
    public bool holding = false;
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
        if (holding)
        {
            hold.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Trash") && !holding)
        {
            holding = true;
            hold = collision.gameObject;
            collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            collision.transform.parent = transform;
        }
    }
}
