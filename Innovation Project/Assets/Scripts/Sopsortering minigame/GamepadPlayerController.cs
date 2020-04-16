using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hold;
    Rigidbody trashBody;
    Rigidbody rigidbody;
    bool activeCooldown = false;
    public bool holding = false;
    public float speed;
    float dashSpeed = 5f;
    float leftAxis;
    float forwardAxis;
    public float throwForce;
    Vector3 VelocityX;
    Vector3 VelocityZ;
    public Vector3 direction;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = Input.GetAxisRaw("JoyHorizontal");
        forwardAxis = Input.GetAxisRaw("JoyVertical");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardAxis * speed);
        transform.Translate(Vector3.right * Time.deltaTime * leftAxis * speed);

        VelocityZ.z =  forwardAxis * speed;
        VelocityX.x =  leftAxis * speed;
        direction = new Vector3(VelocityX.x, 0.5f, VelocityZ.z);

        if (holding)
        {
            hold.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (holding && Input.GetButtonDown("X"))
        {
            holding = false;
            
            Throw();
        }
        if (!activeCooldown  && Input.GetButtonDown("A"))
        {
            activeCooldown = true;
            Invoke("Cooldown", 1f);
            Dash();
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

    private void Cooldown()
    {
        activeCooldown = false;
    }

    private void Throw()
    {
        hold.transform.parent = null;
        trashBody = hold.GetComponent<Rigidbody>();
        trashBody.AddForce(direction * throwForce, ForceMode.Impulse);
        
        
        hold = null;
    }

    private void Dash()
    {
        direction = new Vector3(VelocityX.x, 0, VelocityZ.z);
        rigidbody.AddForce(direction * dashSpeed, ForceMode.Impulse);
    }
}
