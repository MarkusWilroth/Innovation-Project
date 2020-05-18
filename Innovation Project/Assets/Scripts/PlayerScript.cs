using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public string strName; //Namnet på karaktären
    public int points, gold, playerNr; //PlayerNr är vilken spelare det är
    public Material limb, skin, armor, component;

    public int stepId;

    //Något sätt att veta vilken kontroll som styr denna spelare...

    //Något sätt att ha items? vilka items ska vi ha???
    private void Start()
    {
        //trust me it's right!
        //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0] = skin;
        //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2] = component;
        //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[3] = armor;
        //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[4] = limb;
    }

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
