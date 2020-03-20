using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusPlayerMovement : MonoBehaviour
{
    //ToDo: Flytta spelaren vänster höger, fram o tillbaka
    //      Fixa att spelaren inte springer ut ur spelplanen
    //      Fixa att spelaren inte kommer förlångt fram


    public double boundLeft, boundRight, boundTop, boundDown; //hur långt en spelare kan gå
    public float sprintSpeed, dashSpeed, dragDown;

    private bool inAir;
    private Vector3 vecMove;

    public void Start()
    {
        vecMove = transform.position;
        inAir = false;
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            
        }
        KeyboardMovement();

        vecMove.z -= dragDown;
        if (vecMove.z <= boundDown)
        {
            vecMove.z = (float)boundDown;
        }
        transform.position = vecMove;
    }

    private void KeyboardMovement()
    {
        if (!inAir)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Flytta framåt
                vecMove.z += sprintSpeed;
                if (vecMove.z >= boundTop)
                {
                    vecMove.z = (float)boundTop;
                }
            }

            else if (Input.GetKey(KeyCode.S))
            {
                //Flytta bakåt
                vecMove.z -= dashSpeed;
                if (vecMove.z <= boundDown)
                {
                    vecMove.z = (float)boundDown;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Hopp
            }
        }
       

        if (Input.GetKey(KeyCode.A))
        {
            vecMove.x -= dashSpeed;
            if (vecMove.x <= boundLeft)
            {
                vecMove.x = (float)boundLeft;
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //Flytta höger
            vecMove.x += dashSpeed;
            if (vecMove.x >= boundRight)
            {
                vecMove.x = (float)boundRight;
            }
        }

    }

}
