using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenu, CharacterMenu, LevelMenu;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(false);

        //Skapar de static script som finns
    }
    
    public void GetToMainMenu()
    {
        MainMenu.SetActive(true);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(false);
    }

    public void GetToCharacterSelect()
    {
        MainMenu.SetActive(false);
        CharacterMenu.SetActive(true);
        LevelMenu.SetActive(false);
    }

    public void GetToLevelSelect()
    {
        MainMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelMenu.SetActive(true);
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
