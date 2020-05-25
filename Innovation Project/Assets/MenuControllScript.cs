using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Menu;

namespace Menu
{
    public enum MenuState
    {
        MainMenu,
        CharacterSelect,
        LevelSelect,
        PlayerConnect
    }
}

public class MenuControllScript : MonoBehaviour
{
    /*ToDo:
     * Få knapparna att lysa när man har de som alternativ
    */
    public Sprite[] markerTexture;
    public GameObject[] mainButtons, selectButtons, levelButtons, connectButtons;
    public List<GameObject> playerList;
    public GameObject markerO;
    public MenuState menuState;
    public float switchCooldown;

    private PointerEventData pointer = new PointerEventData(EventSystem.current);
    private GameObject[] markers;
    private float[] controllerCooldown;
    private int[] activeButton;
    private float verticalAxis;
    private int maxPlayers;
    
    private void Start()
    {
        maxPlayers = 4;

        activeButton = new int[maxPlayers];
        controllerCooldown = new float[maxPlayers];
        menuState = MenuState.MainMenu;
        

        markers = new GameObject[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            markers[i] = Instantiate(markerO);
            markers[i].GetComponent<Image>().sprite = markerTexture[i];
            markers[i].transform.SetParent(mainButtons[0].transform.parent, false);
        }

        ResetActiveButtons();
    }

    private void ResetActiveButtons()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            activeButton[i] = 0; //När den är 0 är ingen knapp aktiv (activeButton kommer därför behövas göra -1 för att komma åt rätt knapp
            controllerCooldown[i] = 0;
            markers[i].SetActive(false); //Ska bara synas om kontrollen har rört sig
        }
    }

    private void Update()
    {
        switch(menuState)
        {
            case MenuState.MainMenu:
                ControllMenu(mainButtons);
                break;
            case MenuState.CharacterSelect:
                ControllMenu(selectButtons);
                break;
            case MenuState.LevelSelect:
                ControllMenu(levelButtons);
                break;
            case MenuState.PlayerConnect:
                ControllMenu(connectButtons);
                break;
            default:
                Debug.Log("Error! No MenuState");
                break;
        }
    }

    private void ControllMenu(GameObject[] buttons)
    {
        for (int i = 1; i <= maxPlayers; i++)
        {
            if (controllerCooldown[i-1] >= 0)
            {
                //Debug.Log("Cooldown[" + i + "]: " + controllerCooldown[i - 1]);
                controllerCooldown[i - 1] -= Time.deltaTime;
                
            }
            
            if (Input.GetButtonDown(i + "A") && activeButton[i] != 0)
            {
                //ExecuteEvents.Execute(buttons[activeButton], pointer, ExecuteEvents.pointerEnterHandler); //Force Hover
                //ExecuteEvents.Execute(buttons[activeButton], pointer, ExecuteEvents.pointerExitHandler); //Force Unhover
                ExecuteEvents.Execute(buttons[activeButton[i] - 1], pointer, ExecuteEvents.submitHandler); //Click
            }
            if (controllerCooldown[i - 1] <= 0)
            {
                bool canChange = true;
                if (menuState == MenuState.PlayerConnect)
                {
                    canChange = false;

                    foreach(GameObject player in playerList)
                    {
                        if (player.GetComponent<PlayerScript>().playerNr == i)
                        {
                            if (player.GetComponent<SetUpCharacter>().isReady)
                            {
                                canChange = true;
                            } else
                            {
                                activeButton[i] = 0;
                                markers[i - 1].SetActive(false);
                            }
                            break;
                        }
                    }
                    //Special thing
                    //If player is Ready make change
                }

                if (canChange)
                {
                    verticalAxis = Input.GetAxisRaw(i + "JoyVertical");

                    if (verticalAxis == 1)
                    {
                        ScrollMenu(-1, i, buttons.Length); //-1 för att den scrollar ner
                        controllerCooldown[i - 1] = switchCooldown;
                        //Debug.Log("Vertical pressed by Controll: " + i + "\nVerticalAxis: " + verticalAxis);
                        //Debug.Log("Button: " + buttons[activeButton[i] - 1].name + "\ncontroller: " + i);
                        UpdateMarker(i - 1, buttons[activeButton[i] - 1]);

                    }
                    else if (verticalAxis == -1)
                    {
                        ScrollMenu(1, i, buttons.Length);
                        controllerCooldown[i - 1] = switchCooldown;
                        //Debug.Log("Vertical pressed by Controll: " + i + "\nVerticalAxis: " + verticalAxis);
                        //Debug.Log("Button: " + buttons[activeButton[i] - 1].name + "\ncontroller: " + i);
                        UpdateMarker(i - 1, buttons[activeButton[i] - 1]);
                    }
                }
                
            }
            verticalAxis = 0;
        }
    }

    private void UpdateMarker(int i, GameObject button)
    {
        markers[i].SetActive(true); //Ser till att den syns
        markers[i].transform.SetParent(button.transform, false); //Ser till att knappen den är på är dess parent (markern syns alltid)
        RectTransform rt = (RectTransform)button.transform;
        Vector3 buttonPos = button.transform.position;
        
        switch (i)
        {
            case 0: //Player 1 - Högst upp till höger av knappen
                markers[i].transform.position = new Vector3(buttonPos.x + rt.rect.width/2, buttonPos.y + rt.rect.height/2, buttonPos.z);
                break;
            case 1: //Player 2 - Högst upp till vänster av knappen
                markers[i].transform.position = new Vector3(buttonPos.x -rt.rect.width / 2, buttonPos.y + rt.rect.height / 2, buttonPos.z);
                break;
            case 2: //Player 3 - Längst ner till höger av knappen
                markers[i].transform.position = new Vector3(buttonPos.x + rt.rect.width / 2, buttonPos.y - rt.rect.height / 2, buttonPos.z);
                break;
            case 3: //Player 4 - Längst ner till vänster av knappen
                markers[i].transform.position = new Vector3(buttonPos.x - rt.rect.width / 2, buttonPos.y - rt.rect.height / 2, buttonPos.z);
                break;
            default:
                Debug.Log("Error! i is too large");
                break;
        }
    }

    private void ScrollMenu(int scrollfactor, int controller, int scrollLength)
    {
        if (activeButton[controller] == 0) //första gången kontrollen används denna meny
        {
            activeButton[controller] = 1; //Sätts till första knapppen
        }
        else
        {
            activeButton[controller] += scrollfactor;
            if (activeButton[controller] <= 0) //Den har scrollat förbi slutet
            {
                activeButton[controller] = scrollLength;
            }
            else if (activeButton[controller] > scrollLength)
            {
                activeButton[controller] = 1;
            }
        }
    }


    public void ChangeMenu()
    {
        ResetActiveButtons();
    }
}
