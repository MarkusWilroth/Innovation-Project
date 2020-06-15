using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public string strName; //Namnet på karaktären
    public int points, gold, playerNr; //PlayerNr är vilken spelare det är
    public string[] inventory; //Använder itemId;
    public Material limb, skin, armor, component;

    public int stepId;
    

    public void CreateCharacter(int playerNr, Material limb, Material skin, Material armor, Material component, string name)
    {
        this.playerNr = playerNr;
        strName = name;
        this.limb = limb;
        this.skin = skin;
        this.armor = armor;
        this.component = component;

        points = 0;
        gold = 0;
        
        //ScoreScript.scoreScript.LoadPlayerScript(this.playerNr, this.limb, this.skin, this.armor, this.component, strName);
        ScoreScript.scoreScript.LoadPlayerScript(this);
    }
    
    public void LoadCharacter(int playerNr, Material limb, Material skin, Material armor, Material component, string name)
    {
        this.playerNr = playerNr;
        strName = name;
        this.limb = limb;
        this.skin = skin;
        this.armor = armor;
        this.component = component;

        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = skin.color;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].color = component.color;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[3].color = armor.color;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[4].color = limb.color;
        
    }
}
