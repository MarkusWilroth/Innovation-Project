using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTrashMagnet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    float horizontalForce;
    float verticalForce;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            horizontalForce = Random.Range(-2f, 2f);
            verticalForce = Random.Range(-2f, 2f);
            rigidbody.AddForce(new Vector3(horizontalForce, 2, horizontalForce), ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            horizontalForce = Random.Range(-0.1f, 0.1f);
            verticalForce = Random.Range(-0.1f, 0.1f);
            rigidbody.AddForce(new Vector3(horizontalForce, 0.1f, horizontalForce), ForceMode.Impulse);
        }
    }
}
