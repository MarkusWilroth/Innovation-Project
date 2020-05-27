using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupBox : MonoBehaviour
{
    public GameObject RotationBox, ArmorBox, BodyBox, LimbBox, ComponentsBox;
    public GameObject ArmorColor, BodyColor, LimbColor, ComponentsColor;
    public GameObject ActiveBox;

    private Color activeColor, notActiveColor;

    private void Start()
    {
        activeColor = Color.white;
        notActiveColor = Color.black;
    }

    public void SwitchActiveBox(GameObject newActiveBox)
    {
        if (ActiveBox != null)
        {
            ActiveBox.GetComponent<Image>().color = notActiveColor;
        }
        ActiveBox = newActiveBox;
        ActiveBox.GetComponent<Image>().color = activeColor;
    }
    
    public void NoneActive()
    {
        ActiveBox.GetComponent<Image>().color = notActiveColor;
    }
}
