using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    GameObject hold;
    GameObject targetPlayer;
    Rigidbody trashBody;
    Rigidbody rigidbody;
    AudioSource playerAudio;
    Sounds sounds;
    public bool keepGoing;
    public bool activeCooldown = false;
    public bool holding = false;
    public float speed = 5f;
    float dashSpeed = 5f;
    float xBoundary = 8.5f;
    float zBoundary = 7f;
    float lowZBoundary = -4f;
    float throwForce = 5f;
    public int player;
    Vector3 dropDirection;
    public float leftAxis;
    public float forwardAxis;
    Vector3 VelocityX;
    Vector3 VelocityZ;
    public Vector3 direction;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        sounds = GetComponent<Sounds>();
        animator = GetComponent<Animator>();

    }

    public void ControllToPlayer(int number)
    {
        player = number;
    }
    // Update is called once per frame
    void Update()
    {
        leftAxis = Input.GetAxisRaw(player + "JoyHorizontal");
        forwardAxis = Input.GetAxisRaw(player + "JoyVertical");

        Vector3 movement = new Vector3(leftAxis, 0.0f, forwardAxis);

      
        transform.Translate(movement * Time.deltaTime *  speed, Space.World);

        BoundaryMovement();
        VelocityZ.z = forwardAxis * speed;
        VelocityX.x = leftAxis * speed;
        direction = new Vector3(VelocityX.x, 0.5f, VelocityZ.z);
        if (leftAxis != 0 || forwardAxis != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            animator.SetFloat("movement", 1f);
        }
        else
        {
            animator.SetFloat("movement", 0f);
        }
        if (holding)
        {
            hold.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (holding && Input.GetButtonDown(player + "X") && (leftAxis != 0 || forwardAxis != 0))
        {


            Throw();
        }
        if (!activeCooldown && Input.GetButtonDown(player + "A"))
        {

            Invoke("Cooldown", 1f);
            Dash();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash") && !holding)
        {
            holding = true;
            hold = collision.gameObject;
            collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            collision.transform.parent = transform;
        }
        else if (collision.gameObject.CompareTag("Player") && holding)
        {
            targetPlayer = collision.gameObject;
            keepGoing = targetPlayer.GetComponent<GamepadPlayerController>().activeCooldown;
            playerAudio.PlayOneShot(sounds.knuff, 1f);

            if (keepGoing)
            {

                dropDirection = gameObject.transform.position - collision.gameObject.transform.position;

                Drop();
            }
        }
    }

    private void Cooldown()
    {
        activeCooldown = false;
    }

    private void BoundaryMovement()
    {
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }
        else if (transform.position.z < lowZBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowZBoundary);
        }
    }
    private void Drop()
    {
        holding = false;
        hold.transform.parent = null;
        trashBody = hold.GetComponent<Rigidbody>();
        dropDirection.y = 1;
        trashBody.AddForce(dropDirection * throwForce, ForceMode.Impulse);
        hold = null;
        targetPlayer = null;
    }
    private void Throw()
    {

        holding = false;
        hold.transform.parent = null;
        trashBody = hold.GetComponent<Rigidbody>();
        direction.y = 1;
        trashBody.AddForce(direction * throwForce, ForceMode.Impulse);
        playerAudio.PlayOneShot(sounds.kasta, 1f);


        hold = null;
    }

    private void Dash()
    {
        activeCooldown = true;

        direction = new Vector3(VelocityX.x, 0, VelocityZ.z);
        rigidbody.AddForce(direction * dashSpeed, ForceMode.Impulse);
    }
}
