using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupBox : MonoBehaviour
{
    public GameObject RotationBox, ArmorBox, BodyBox, LimbBox, ComponentsBox;
    public GameObject ArmorColor, BodyColor, LimbColor, ComponentsColor;

    private Color activeColor, notActiveColor;

    private void Start()
    {
        activeColor = Color.white;
        notActiveColor = Color.black;
    }
    //Borde göra denna del med att spara active och old för att endast ändra färg på de relevanta
    #region UpdateHighlight
    public void ActiveRotation()
    {
        RotationBox.GetComponent<Image>().color = activeColor;
        ArmorBox.GetComponent<Image>().color = notActiveColor;
        BodyBox.GetComponent<Image>().color = notActiveColor;
        LimbBox.GetComponent<Image>().color = notActiveColor;
        ComponentsBox.GetComponent<Image>().color = notActiveColor;
    }
    public void ActiveArmor()
    {
        RotationBox.GetComponent<Image>().color = notActiveColor;
        ArmorBox.GetComponent<Image>().color = activeColor;
        BodyBox.GetComponent<Image>().color = notActiveColor;
        LimbBox.GetComponent<Image>().color = notActiveColor;
        ComponentsBox.GetComponent<Image>().color = notActiveColor;
    }
    public void ActiveBody()
    {
        RotationBox.GetComponent<Image>().color = notActiveColor;
        ArmorBox.GetComponent<Image>().color = notActiveColor;
        BodyBox.GetComponent<Image>().color = activeColor;
        LimbBox.GetComponent<Image>().color = notActiveColor;
        ComponentsBox.GetComponent<Image>().color = notActiveColor;
    }
    public void ActiveLimb()
    {
        RotationBox.GetComponent<Image>().color = notActiveColor;
        ArmorBox.GetComponent<Image>().color = notActiveColor;
        BodyBox.GetComponent<Image>().color = notActiveColor;
        LimbBox.GetComponent<Image>().color = activeColor;
        ComponentsBox.GetComponent<Image>().color = notActiveColor;
    }
    public void ActiveComponents()
    {
        RotationBox.GetComponent<Image>().color = notActiveColor;
        ArmorBox.GetComponent<Image>().color = notActiveColor;
        BodyBox.GetComponent<Image>().color = notActiveColor;
        LimbBox.GetComponent<Image>().color = notActiveColor;
        ComponentsBox.GetComponent<Image>().color = activeColor;
    }

    public void NoneActive()
    {
        RotationBox.GetComponent<Image>().color = notActiveColor;
        ArmorBox.GetComponent<Image>().color = notActiveColor;
        BodyBox.GetComponent<Image>().color = notActiveColor;
        LimbBox.GetComponent<Image>().color = notActiveColor;
        ComponentsBox.GetComponent<Image>().color = notActiveColor;
    }
    #endregion
}
