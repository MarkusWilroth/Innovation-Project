﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Menu;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenu, CharacterMenu, LevelMenu, SetupMenu;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(false);
        SetupMenu.SetActive(false);

        if (ScoreScript.scoreScript == null)
        {
            ScoreScript.scoreScript = new ScoreScript();
            ScoreScript.scoreScript.StartWorld();
        } 
        //Skapar de static script som finns
    }
    
    public void GetToMainMenu()
    {
        MainMenu.SetActive(true);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(false);
        SetupMenu.SetActive(false);
        GetComponent<MenuControllScript>().ChangeMenu();
        GetComponent<MenuControllScript>().menuState = MenuState.MainMenu;
    }

    public void GetToCharacterSelect()
    {
        MainMenu.SetActive(false);
        CharacterMenu.SetActive(true);
        LevelMenu.SetActive(false);
        SetupMenu.SetActive(false);
        GetComponent<MenuControllScript>().ChangeMenu();
        GetComponent<MenuControllScript>().menuState = MenuState.CharacterSelect;
        
    }

    public void GetToLevelSelect()
    {
        MainMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(true);
        SetupMenu.SetActive(false);
        GetComponent<MenuControllScript>().ChangeMenu();
        GetComponent<MenuControllScript>().menuState = MenuState.LevelSelect;
    }

    public void GoToSetup()
    {
        MainMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(false);
        SetupMenu.SetActive(true);
        GetComponent<MenuControllScript>().ChangeMenu();
        GetComponent<MenuControllScript>().menuState = MenuState.PlayerConnect;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Jack");
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}
