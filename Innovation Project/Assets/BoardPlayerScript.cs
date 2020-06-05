using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step;

public class BoardPlayerScript : MonoBehaviour
{
    private enum PlayerPhase
    {
        itemPhase,
        movePhase,
        eventPhase,
        endPhase
    }

    private enum PlayerMove
    {
        rollDie,
        getSteps,
        chooseRoute,
        walk
    }

    public float movementSpeed;

    private PlayerPhase playerPhase;
    private PlayerMove playerMove;
    private Node node;
    private bool isPlayerTurn, hasTarget;
    private Gameboard gameboard;

    private List<GameObject> stepList; //De steg karaktären kommer gå till (För att den inte ska gena vid korsningar)
    private GameObject[] allSteps;
    private GameObject targetStep, activeStep;
    private List<Node> nodeList;

    private List<GameObject> routeList, pickedRoute;
    private List<List<GameObject>> allRouteList;

    private int moveLength; //How many steps to move

    // Start is called before the first frame update
    void Start()
    {
        nodeList = new List<Node>();
        stepList = new List<GameObject>();
        routeList = new List<GameObject>();
        allRouteList = new List<List<GameObject>>();

        foreach (GameObject step in allSteps)
        {
            if (step.GetComponent<StepScript>().stepId == GetComponent<PlayerScript>().stepId)
            {
                activeStep = step;
                break;
            }
        }
        playerMove = PlayerMove.rollDie;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {

            switch (playerPhase)
            {
                case PlayerPhase.itemPhase:
                    //Check if use item or roll (or both)
                    //Debug.Log("Didn't use Item");
                    playerPhase = PlayerPhase.movePhase;
                    break;
                case PlayerPhase.movePhase:
                    //Roll die and Move to next step
                    switch (playerMove)
                    {
                        case PlayerMove.rollDie:
                            RollDie();
                            break;
                        case PlayerMove.getSteps:
                            GetTargetSteps();
                            break;
                        case PlayerMove.chooseRoute:
                            ChooseStepPath();
                            break;
                        case PlayerMove.walk:
                            MoveOnBoard();
                            break;
                    }

                    //if (!hasTarget)
                    //{
                    //    //moveLength = RollDie();
                    //    MoveOnBoard(moveLength);
                        
                    //    for (int i = 1; i <= moveLength; i++) //Tar fram alla steg karaktären kommer gå till för att nå målet
                    //    {
                    //        bool isRemoved = false;
                    //        foreach (GameObject step in allSteps)
                    //        {
                    //            if (!isRemoved && step.GetComponent<StepScript>().stepId == GetComponent<PlayerScript>().stepId)
                    //            {
                    //                step.GetComponent<StepScript>().RemoveCharacter(gameObject);
                    //            }
                    //            if (step.GetComponent<StepScript>().stepId == GetComponent<PlayerScript>().stepId + i)
                    //            {
                    //                stepList.Add(step);
                    //                targetStep = step;
                    //                //Flytta de som står på rutan

                    //                break;
                    //            }
                    //        }
                    //    }
                    //    hasTarget = true;

                    //} else
                    //{
                    //    //Debug.Log("Stepcount: " + stepList.Count);
                    //    if (stepList.Count != 0)
                    //    {
                            
                    //        Vector3 targetPos = stepList[0].GetComponent<StepScript>().entryPointPos; //går mot första målet i listan
                    //        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);

                    //        if (transform.position == targetPos) //Den har nått sin target!
                    //        {
                    //            stepList.Remove(stepList[0]); //Ser till att den går till nästa target
                    //            GetComponent<PlayerScript>().stepId = targetStep.GetComponent<StepScript>().stepId;
                    //        }
                    //    } else //Den har gått till alla sina target (Den är färdig
                    //    {
                    //        targetStep.GetComponent<StepScript>().AddCharacter(gameObject);
                    //        stepList.Clear();
                    //        playerPhase = PlayerPhase.eventPhase;
                    //    }

                    //}
                    break;
                case PlayerPhase.eventPhase:
                    //Check if event happens on step
                    //Debug.Log("Implement eventPhase");
                    playerPhase = PlayerPhase.endPhase;
                    break;
                case PlayerPhase.endPhase:
                    //Debug.Log("Implement endPhase");
                    EndTurn();
                    //Use items or end turn (or both)
                    break;
            }
        }
    }

    private void GetTargetSteps()
    {
        //if (activeStep.GetComponent<StepScript>().chooseStep) //Rutan den står på är en ruta med gångalternativ
        //{
        //    playerMove = PlayerMove.chooseRoute;
        //} else
        //{
        //    if (moveLength > 0) //Om den är över 0 har den drag att göra
        //    {

        //        stepList.Add(targetStep.GetComponent<StepScript>().nextStep[0]); //Lägger till steget i listan
        //        targetStep = targetStep.GetComponent<StepScript>().nextStep[0]; //ser till att den kollar nästa stegs steg nästa gång
        //        moveLength--; //Minskar stegen

        //        if (targetStep.GetComponent<StepScript>().chooseStep) //Om steget den just gick till är splittning ska den gå till den rutan
        //        {
        //            playerMove = PlayerMove.walk;
        //        }
        //    } else
        //    {
        //        playerMove = PlayerMove.walk; //Har den inga drag att göra ska den gå
        //    }
        //}
        Debug.Log("ActiveStep: " + activeStep.name);
        RecursiveStep(activeStep);
        
        ClearNodes();
        
        playerMove = PlayerMove.chooseRoute;
    }

    private void RecursiveStep(GameObject step)
    {
        if (moveLength <= 0)
        {
            allRouteList.Add(routeList);
            return;
        }
        if (step.GetComponent<StepScript>().nextStep.Length > 1)
        {
            for (int i = 0; i < step.GetComponent<StepScript>().nextStep.Length; i++)
            {
                //Debug.Log("Step: " + step.name);
                //Debug.Log("I: " + i);
                moveLength--;
                routeList.Add(step.GetComponent<StepScript>().nextStep[i]);

                Node.node = new Node(moveLength, routeList, i);
                //Node.node.path = routeList;
                nodeList.Add(Node.node);
                //Debug.Log("routes: " + nodeList[0].path.Length);
                routeList.Remove(step.GetComponent<StepScript>().nextStep[i]);
                moveLength++;
            }
        } else
        {
            if (step != activeStep)
            {
                routeList.Add(step);
                moveLength--;
            }
            RecursiveStep(step.GetComponent<StepScript>().nextStep[0]);
        }
    }

    private void ClearNodes()
    {
        Debug.Log("Node List: " + nodeList.Count);

        while (nodeList.Count > 0)
        {
            Debug.Log("---End---");
            routeList.Clear();
            for (int i = 0; i < nodeList[0].path.Length; i++)
            {
                routeList.Add(nodeList[0].path[i]);
            }
            Debug.Log("route List count: " + routeList.Count);

            //Debug.Log("Node List count: " + nodeList[0].path.Length);
            moveLength = nodeList[0].moveLength;
            //routeList = nodeList[0].path;
            
            RecursiveStep(routeList[routeList.Count - 1]);
            
            nodeList.Remove(nodeList[0]);
        }
        playerMove = PlayerMove.chooseRoute;
    }

    private void ChooseStepPath()
    {
        foreach (List<GameObject> route in allRouteList)
        {
            //Debug.Log("-----New Route-----");
            foreach (GameObject step in route)
            {
                //Debug.Log("Step: " + step.name);
            }
        }
        stepList = allRouteList[0];
        playerMove = PlayerMove.walk;
    }

    private void MoveOnBoard()
    {
        if (stepList.Count != 0)
        {

            Vector3 targetPos = stepList[0].GetComponent<StepScript>().entryPointPos; //går mot första målet i listan
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);

            if (transform.position == targetPos) //Den har nått sin target!
            {
                activeStep = stepList[0];
                stepList.Remove(stepList[0]); //Ser till att den går till nästa target
                //GetComponent<PlayerScript>().stepId = targetStep.GetComponent<StepScript>().stepId;
            }
        }
        else //Den har gått till alla sina target (Den är färdig
        {
            //targetStep.GetComponent<StepScript>().AddCharacter(gameObject);
            stepList.Clear();
            playerPhase = PlayerPhase.eventPhase;
        }
    }

    private void EndTurn()
    {
        isPlayerTurn = false;
        hasTarget = false;
        gameboard.NextTurn();

        routeList.Clear();
        allRouteList.Clear();
    }

    public void SpawnCharacter(Gameboard gameboard, GameObject[] allSteps)
    {
        this.gameboard = gameboard;
        this.allSteps = allSteps;
        hasTarget = false;
    }

    public void GetTurn()
    {
        isPlayerTurn = true;
        hasTarget = false;
        playerPhase = PlayerPhase.itemPhase;
        playerMove = PlayerMove.rollDie;
        targetStep = activeStep;
        //routeList.Clear();
        //allRouteList.Clear();
    }

    public void RollDie()
    {
        int dieRoll = Random.Range(1, 7); //Tar positionen den står på och lägger till tärningsslaget för att få fram nästa steg
        moveLength = dieRoll;
        playerMove = PlayerMove.getSteps;
    }
}

public class Node {
    public int moveLength, pathID;
    public GameObject[] path;

    public static Node node;

    public Node(int moveLength, List<GameObject> pathList, int pathID)
    {
        this.moveLength = moveLength;
        //this.path = path;
        this.pathID = pathID;

        path = new GameObject[pathList.Count];
        for (int i = 0; i < pathList.Count; i++)
        {
            path[i] = pathList[i];
        }

        //Debug.Log("route: " + path.Length);
        //Debug.Log("-----Node-----");
        foreach (GameObject step in pathList)
        {
            //Debug.Log("Step: " + step.name);
        }
    }
}
