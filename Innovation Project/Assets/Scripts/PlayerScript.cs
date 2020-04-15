using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public string strName; //Namnet på karaktären
    public int points, gold, playerNr; //PlayerNr är vilken spelare det är

    //Något sätt att veta vilken kontroll som styr denna spelare...

    //Något sätt att ha items? vilka items ska vi ha???

    public void CreateCharacter(int playerNr)
    {
        this.playerNr = playerNr;
        points = 0;
        gold = 0;
    }
}
