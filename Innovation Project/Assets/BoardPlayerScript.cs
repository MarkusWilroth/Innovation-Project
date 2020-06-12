using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardCamera;
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
    public GameObject ChoiceMarker;
    private BoardCameraMovement boardCameraMovement;

    private PlayerPhase playerPhase;
    private PlayerMove playerMove;
    private Node node;
    private bool isPlayerTurn, hasTarget;
    private int playerNr, choiceNr;
    private Gameboard gameboard;

    private List<GameObject> stepList; //De steg karaktären kommer gå till (För att den inte ska gena vid korsningar)
    private GameObject[] allSteps;
    private GameObject targetStep, activeStep;
    private List<Node> nodeList;
    private List<GameObject> choiceMarkerList;

    private List<GameObject> routeList, pickedRoute, pathList;
    private List<List<GameObject>> allRouteList;

    private int moveLength; //How many steps to move
    
    void Start()
    {
        pathList = new List<GameObject>();
        nodeList = new List<Node>();
        stepList = new List<GameObject>();
        routeList = new List<GameObject>();
        allRouteList = new List<List<GameObject>>();
        choiceMarkerList = new List<GameObject>();

        boardCameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BoardCameraMovement>();

        playerNr = GetComponent<PlayerScript>().playerNr;

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

    public void SpawnCharacter(Gameboard gameboard, GameObject[] allSteps)
    {
        this.gameboard = gameboard;
        this.allSteps = allSteps;
        hasTarget = false;
    }

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
                            if (boardCameraMovement.cameraState == BoardCameraMovement.CameraState.alterState)
                            {
                                ChooseStepPath();
                            }
                            break;
                        case PlayerMove.walk:
                            MoveOnBoard();
                            break;
                    }
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

    #region movePhase
    public void RollDie() //Första som händer rullas tärningen
    {
        
        int dieRoll = Random.Range(1, 7); //Tar positionen den står på och lägger till tärningsslaget för att få fram nästa steg
        moveLength = dieRoll;
        playerMove = PlayerMove.getSteps;
    }

    private void GetTargetSteps() //Nästa som händer tar den fram de möjliga dragen
    {
        pathList.Add(activeStep);
        RecursiveStep(activeStep);
        ClearNodes();
        SpawnChoiceMarkers();
        playerMove = PlayerMove.chooseRoute;
    }

    private void SpawnChoiceMarkers()
    {
        if (allRouteList.Count > 1)
        {
            foreach (List<GameObject> stepList in allRouteList)
            {
                GameObject holder = Instantiate(ChoiceMarker);
                holder.transform.SetParent(stepList[stepList.Count - 1].transform, false);
                holder.transform.localPosition = new Vector3(0, 1, 0);
                choiceMarkerList.Add(holder);
            }
        }
    }

    private void ChooseStepPath() //Sen väljs vilken väg som kan gå
    {
        if (allRouteList.Count > 1)
        {
            if (playerNr >= 3)
            {
                int rand = Random.Range(0, allRouteList.Count);
                stepList = allRouteList[rand];
                DestroyMarkers();
                playerMove = PlayerMove.walk;
            }
            boardCameraMovement.GetTargetPos(choiceMarkerList[choiceNr].transform.position);
            if (Input.GetButtonDown(playerNr+"R1"))
            {
                choiceNr++;
                if (choiceNr >= allRouteList.Count)
                {
                    choiceNr = 0;
                }
            } else if (Input.GetButtonDown(playerNr + "L1"))
            {
                choiceNr--;
                if (choiceNr < 0)
                {
                    choiceNr = allRouteList.Count - 1;
                }
            } else if (Input.GetButtonDown(playerNr + "X"))
            {
                stepList = allRouteList[choiceNr];
                DestroyMarkers();
                playerMove = PlayerMove.walk;
            }
        }
        else
        {
            stepList = allRouteList[0];
            playerMove = PlayerMove.walk;
        }
    }

    private void DestroyMarkers()
    {
        foreach (GameObject marker in choiceMarkerList)
        {
            Destroy(marker.gameObject);
        }
        choiceMarkerList.Clear();
    }

    private void MoveOnBoard() //Sen går karaktären den valda vägen
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

    private void RecursiveStep(GameObject step) //Går igenom trädet av möjliga vägar
    {
        //Debug.Log("Step: " + step.name);
        if (!step.GetComponent<StepScript>().pathWayStep)
        {
            pathList = new List<GameObject>();
            //Debug.Log(step.name + ": is not a pathway step");
        }
        if (moveLength <= 0) //Finns det inga fler steg att ta läggs vägen in i möjliga vägar lista
        {
            //Debug.Log("Added to list!\nFirst step in list: " + routeList[0]);
            allRouteList.Add(routeList);
            routeList = new List<GameObject>();
            return;
        }

        if (step.GetComponent<StepScript>().nextStep.Length > 1)
        {
            for (int i = 0; i < step.GetComponent<StepScript>().nextStep.Length; i++)
            {
                bool canAdd = true;
                foreach (GameObject pastStep in pathList)
                {
                    if (step.GetComponent<StepScript>().nextStep[i].name == pastStep.name) //Om den redan har gått denna väg så ska den gå tillbaka
                    {
                        canAdd = false;
                    }
                }
                if (canAdd)
                {
                    moveLength--;
                    routeList.Add(step.GetComponent<StepScript>().nextStep[i]);
                    pathList.Add(step.GetComponent<StepScript>().nextStep[i]);

                    Node.node = new Node(moveLength, routeList, pathList);
                    nodeList.Add(Node.node);

                    pathList.Remove(step.GetComponent<StepScript>().nextStep[i]);
                    routeList.Remove(step.GetComponent<StepScript>().nextStep[i]);
                    moveLength++;
                }

            }
        }
        else
        {

            foreach (GameObject pastStep in pathList)
            {
                //Debug.Log("Step: " + step.GetComponent<StepScript>().nextStep[0].name + "\npastStep: " + pastStep.name);
                if (step.GetComponent<StepScript>().nextStep[0].name == pastStep.name) //Om den redan har gått denna väg så ska den gå tillbaka
                {
                    return;
                }
            }
            routeList.Add(step.GetComponent<StepScript>().nextStep[0]);
            moveLength--;
            pathList.Add(step.GetComponent<StepScript>().nextStep[0]);
            RecursiveStep(step.GetComponent<StepScript>().nextStep[0]);

            if (step != activeStep)
            {


                //Debug.Log("pathWayStep: " + step.GetComponent<StepScript>().pathWayStep);
                if (step.GetComponent<StepScript>().pathWayStep)
                {

                    //pathList.Add(step);

                }
            }

        }
    }

    private void ClearNodes() //Fortsätter de grenar som finns
    {
        while (nodeList.Count > 0)
        {
            //Debug.Log("-----New Step-----");
            routeList.Clear();
            for (int i = 0; i < nodeList[0].path.Length; i++)
            {
                routeList.Add(nodeList[0].path[i]);
            }

            pathList.Clear();
            for (int i = 0; i < nodeList[0].noWayPath.Length; i++)
            {
                pathList.Add(nodeList[0].noWayPath[i]);
            }
            moveLength = nodeList[0].moveLength;

            RecursiveStep(routeList[routeList.Count - 1]);

            nodeList.Remove(nodeList[0]);
        }
        playerMove = PlayerMove.chooseRoute;
    }
    #endregion

    #region TurnManager
    public void GetTurn() //Det som händer när turen startar
    {
        isPlayerTurn = true;
        hasTarget = false;
        playerPhase = PlayerPhase.itemPhase;
        playerMove = PlayerMove.rollDie;
        targetStep = activeStep;
        choiceNr = 0;
        //routeList.Clear();
        //allRouteList.Clear();
    }

    private void EndTurn() //Det som händer när turne tar slut
    {
        isPlayerTurn = false;
        hasTarget = false;
        gameboard.NextTurn();

        routeList.Clear();
        allRouteList.Clear();
    }
    #endregion
}

public class Node //Sparar de nodes som finns
{ 
    public int moveLength;
    public GameObject[] path, noWayPath;

    public static Node node;

    public Node(int moveLength, List<GameObject> pathList, List<GameObject> noWayPathList)
    {
        this.moveLength = moveLength;

        path = new GameObject[pathList.Count];
        for (int i = 0; i < pathList.Count; i++)
        {
            path[i] = pathList[i];
        }

        noWayPath = new GameObject[noWayPathList.Count];
        for (int i = 0; i < noWayPathList.Count; i++)
        {
            noWayPath[i] = noWayPathList[i];
        }
    }
}
