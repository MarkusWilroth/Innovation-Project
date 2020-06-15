using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptDieObject : ScriptableObject
{
    public string itemName = "Item name here"; //The name that will be displayed
    public string itemDesc = "Item desc here"; //Info about the item
    public string itemID = "Fills itself!"; //Id if needed (Fills itself)
    public string soundID = "Needed?";      //The sound that will be played when activated
    public int cost, itemTier; //Cost - store cost, itemTier - rareity
    public Sprite icon; //Display image

    public int[] dieEyes; //How the die will work, could be a 1-6 or 1-4 or a 2, 2, 5, 5 die
}
