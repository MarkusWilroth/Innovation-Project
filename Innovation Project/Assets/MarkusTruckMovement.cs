using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusTruckMovement : MonoBehaviour
{

    //ToDo:
    //Få truck att åka åt sidorna
    //Banan den kan åka på ä x-10 till x:10

    private Vector3 moveVec;
    private bool inMid, inLeft, inRight; //Vilken line den är i (Avgör hur den kan svänga)
    private bool canTurn; //Bestämmer om den ska svänga
    private bool isDecided; //Om sant börjar svängen
    private bool hasModifer; //Onödig kanske men orkar inte ändra
    private bool isLeftTurn; //kolla om den ska svänga vänster
    private float turnTimer; //Hur lång tid innan den gör nästa sväng
    public float minTime, maxTime; //Intervallet på hur lång tid det kan ta innan nästa sväng
    private float midLine, leftLine, rightLine, targetLine; //Vart på banan den kommer att svänga till
    private float moveModifier; //Hur den svänger
    public float moveSpeed; //Hur snabbt den svänger

    // Start is called before the first frame update
    void Start()
    {
        moveVec = transform.position;
        inMid = true;
        canTurn = false;
        isDecided = false;
        turnTimer = Random.Range(minTime, maxTime);

        midLine = transform.position.x;
        leftLine = midLine - 7;
        rightLine = midLine + 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTurn) //Läge att svänga
        {
            if(!isDecided) //Vet den vart den ska svänga
            {
                DirectionDecider();
            } else
            {
                MoveToTarget();
            }
            //Gör en sväng
        } else
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canTurn = true; //Gör svängen
                isDecided = false; //Ska välja ny position att åka till
                turnTimer = Random.Range(minTime, maxTime); //Skapar en ny timer tills när svängen är klar
            }
        }
    }

    private void DirectionDecider()
    {
        if (inMid)
        {
            //Kan inte göra hard svängar
            inMid = false;
            switch (Random.Range(0,2))
            {
                case 0:
                    targetLine = leftLine;
                    inLeft = true;
                    break;
                case 1:
                    targetLine = rightLine;
                    inRight = true;
                    break;
                default:
                    Debug.Log("Error... range");
                    break;
            }
        } else if (inLeft)
        {
            //Kan inte svänga vänster
            inLeft = false;

            switch (Random.Range(0, 2))
            {
                case 0:
                    targetLine = midLine;
                    inMid = true;
                    break;
                case 1:
                    targetLine = rightLine;
                    inRight = true;
                    break;
                default:
                    Debug.Log("Error... range");
                    break;
            }
        } else if (inRight)
        {
            //Kan inte svänga höger
            inRight = false;

            switch (Random.Range(0, 2))
            {
                case 0:
                    targetLine = midLine;
                    inMid = true;
                    break;
                case 1:
                    targetLine = leftLine;
                    inLeft = true;
                    break;
                default:
                    Debug.Log("Error... range");
                    break;
            }
        } else
        {
            Debug.Log("Error! No Line Found");
        }
        isDecided = true;
        hasModifer = false;
    }

    private void MoveToTarget()
    {
        if (!hasModifer)
        {
            if (targetLine < transform.position.x)
            {
                isLeftTurn = true;
                moveModifier = -moveSpeed;
            }
            else if (targetLine > transform.position.x)
            {
                isLeftTurn = false;
                moveModifier = moveSpeed;
            }
            hasModifer = true;
        }
        if (isLeftTurn)
        {
            if (targetLine > transform.position.x)
            {
                canTurn = false;
                isDecided = false;
            }
        } else
        { 

            if (targetLine < transform.position.x)
            {
                canTurn = false;
                isDecided = false;
            }
        }
        moveVec.x += moveModifier;
        transform.position = moveVec;
    }
}
