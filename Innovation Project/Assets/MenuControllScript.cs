using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
     * Starta spelet med en kontroller
     * Få knapparna att lysa när man har de som alternativ
    */

    public GameObject[] mainButtons, selectButtons, levelButtons, connectButtons;
    public MenuState menuState;
    private int maxPlayers;
    private int[] activeButton;
    private float verticalAxis;
    private PointerEventData pointer = new PointerEventData(EventSystem.current);

    private void Start()
    {
        maxPlayers = 4;
        activeButton = new int[maxPlayers];
        menuState = MenuState.MainMenu;
        ResetActiveButtons();
    }

    private void ResetActiveButtons()
    {
        
        for (int i = 0; i < activeButton.Length; i++)
        {
            activeButton[i] = 1; //När den är 0 är ingen knapp aktiv (activeButton kommer därför behövas göra -1 för att komma åt rätt knapp
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
            if (Input.GetButtonDown(i + "A") && activeButton[i] != 0)
            {
                //ExecuteEvents.Execute(buttons[activeButton], pointer, ExecuteEvents.pointerEnterHandler); //Force Hover
                //ExecuteEvents.Execute(buttons[activeButton], pointer, ExecuteEvents.pointerExitHandler); //Force Unhover
                ExecuteEvents.Execute(buttons[activeButton[i] - 1], pointer, ExecuteEvents.submitHandler); //Click
            } else
            {
                verticalAxis = Input.GetAxisRaw(i + "JoyVertical");
                if (verticalAxis < 0) 
                {
                    Debug.Log("Vertical pressed by Controll: " + verticalAxis);
                    
                }
            }
            verticalAxis = 0;
        }
        
    }

    public void ChangeMenu()
    {
        ResetActiveButtons();
    }
}
