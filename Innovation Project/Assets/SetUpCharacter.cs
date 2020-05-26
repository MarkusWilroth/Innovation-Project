using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class SetUpCharacter : MonoBehaviour
{
    private Material limb, skin, armor, component;
    private Color[] colors;
    public int playerNr;
    private int armorCounter, skinCounter, limbCounter, compCounter, changeCounter;
    public int resetTime;
    private float verticalAxis, oldVerticalAxis, axisValue, changeTimer;
    private bool isChangeable;
    public GameObject lblReady;
    private Vector2 readyPos;
    public PlayerConnectScritp connectScript;

    public SetupBox setUpBoxScript;

    public bool isReady;
    public int occupiedSlot; //Vilken ruta karaktären är i
    private ThingToChange toChange;

    private enum ThingToChange
    {
        armorColor,
        skinColor,
        limbColor,
        componentColor,
        rotate,
    }
    // Start is called before the first frame update
    void Start()
    {
        //Gör det lättare att ändra färg
        skin = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
        component = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
        armor = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[3];
        limb = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[4];
        
        colors = new Color[] { Color.red, Color.white, Color.yellow, Color.black, Color.blue, Color.clear, Color.cyan, Color.gray, Color.green, Color.grey, Color.magenta };
        skin.color = colors[Random.Range(0, colors.Length)];
        armor.color = colors[Random.Range(0, colors.Length)];
        component.color = colors[Random.Range(0, colors.Length)];
        limb.color = colors[Random.Range(0, colors.Length)];

        UpdateBoxColors();

        //RotateToCamera();
        
        toChange = ThingToChange.armorColor;
        isChangeable = true;
        resetTime = 1;

        axisValue = Input.GetAxisRaw(playerNr + "JoyVertical");

        lblReady = Instantiate(lblReady);
        readyPos = new Vector3(55, 240, -1);
        //readyPos.x -= 0.11f;
        //readyPos.y += 0.47f;

        lblReady.transform.position = readyPos;
        //lblReady.transform.Rotate(0, 180, 0);
        lblReady.transform.localScale = new Vector3(1,1,1);
        lblReady.transform.SetParent(transform.parent.transform, false);
        RectTransform rt = (RectTransform)lblReady.transform;
        rt.sizeDelta = new Vector2(300, 80);

        lblReady.SetActive(false);

        oldVerticalAxis = Input.GetAxisRaw(playerNr + "JoyVertical");

        UpdateHighlightBox();
    }

    private void Update()
    {
        if (!isReady)
        {
            if (Input.GetButtonDown(playerNr + "L1"))
            {
                AlterCharacter(-1);
            }
            else if (Input.GetButtonDown(playerNr + "R1"))
            {
                AlterCharacter(1);
            }

            changeTimer -= Time.deltaTime;
            if (changeTimer <= 0)
            {
                isChangeable = true;
            }

            if (isChangeable)
            {
                verticalAxis = Input.GetAxisRaw(playerNr + "JoyVertical");
                //Debug.Log("VerticalAxis: " + verticalAxis);
                if (verticalAxis == 1)
                {
                    oldVerticalAxis = verticalAxis;
                    SwitchToChange(1);
                    isChangeable = false;
                    changeTimer = resetTime;
                }
                else if (verticalAxis == -1)
                {
                    oldVerticalAxis = verticalAxis;
                    SwitchToChange(-1);
                    isChangeable = false;
                    changeTimer = resetTime;
                }
            }
            if (Input.GetButtonDown(playerNr + "B"))
            {
                connectScript.DisconnectPlayer(playerNr, occupiedSlot, gameObject);
                Destroy(gameObject.transform.parent.transform.gameObject);
            }
        }
        

        if (Input.GetButtonDown(playerNr + "A"))
        {
            if (!isReady)
            {
                isReady = true;
                GetComponent<PlayerScript>().CreateCharacter(playerNr, limb, skin, armor, component, "James");
                RotateToCamera();
                lblReady.SetActive(true);
                setUpBoxScript.NoneActive();
            } 
        }

        if (Input.GetButtonDown(playerNr + "B")) //Om spelaren klickar på bakåt knappen
        {
            if (isReady) //Om spelaren är redo ska den tas bort från ScoreScript och sättas som oredo
            {
                isReady = false;
                lblReady.SetActive(false);
                foreach (PlayerScript playerScript in ScoreScript.scoreScript.playerScripts)
                {
                    if (playerScript.playerNr == playerNr)
                    {
                        ScoreScript.scoreScript.playerScripts.Remove(playerScript);
                        break;
                    }
                }
                toChange = ThingToChange.armorColor;
                UpdateHighlightBox();
            }
        }
        UpdateBoxColors();
    }

    private void UpdateBoxColors()
    {
        setUpBoxScript.ArmorColor.GetComponent<Image>().color = armor.color;
        setUpBoxScript.BodyColor.GetComponent<Image>().color = skin.color;
        setUpBoxScript.LimbColor.GetComponent<Image>().color = limb.color;
        setUpBoxScript.ComponentsColor.GetComponent<Image>().color = component.color;
    }

    private void SwitchToChange(int modifier)
    {
        if (modifier > 0)
        {
            toChange--;
        } else
        {
            toChange++;
        }
        if (toChange > ThingToChange.rotate)
        {
            toChange = ThingToChange.armorColor;
        } else if (toChange < ThingToChange.armorColor)
        {
            toChange = ThingToChange.rotate;
        }
        UpdateHighlightBox();
    }

    private void UpdateHighlightBox()
    {
        switch(toChange)
        {
            case ThingToChange.rotate:
                setUpBoxScript.ActiveRotation();
                break;
            case ThingToChange.armorColor:
                setUpBoxScript.ActiveArmor();
                break;
            case ThingToChange.skinColor:
                setUpBoxScript.ActiveBody();
                break;
            case ThingToChange.limbColor:
                setUpBoxScript.ActiveLimb();
                break;
            case ThingToChange.componentColor:
                setUpBoxScript.ActiveComponents();
                break;
        }
    }

    private void AlterCharacter(int modifier)
    {
        switch (toChange)
        {
            case ThingToChange.armorColor:
                armorCounter += modifier;
                if (armorCounter >= colors.Length)
                {
                    armorCounter = 0;
                } else if (armorCounter < 0)
                {
                    armorCounter = colors.Length - 1;
                }
                
                armor.color = colors[armorCounter];
                setUpBoxScript.ArmorColor.GetComponent<Image>().color = colors[armorCounter];
                break;
            case ThingToChange.skinColor:
                skinCounter += modifier;
                if (skinCounter >= colors.Length)
                {
                    skinCounter = 0;
                }
                else if (skinCounter < 0)
                {
                    skinCounter = colors.Length - 1;
                }

                skin.color = colors[skinCounter];
                setUpBoxScript.BodyColor.GetComponent<Image>().color = colors[armorCounter];
                break;
            case ThingToChange.limbColor:
                limbCounter += modifier;
                if (limbCounter >= colors.Length)
                {
                    limbCounter = 0;
                }
                else if (limbCounter < 0)
                {
                    limbCounter = colors.Length - 1;
                }

                limb.color = colors[limbCounter];
                setUpBoxScript.LimbColor.GetComponent<Image>().color = colors[armorCounter];
                break;
            case ThingToChange.componentColor:
                compCounter += modifier;
                if (compCounter >= colors.Length)
                {
                    compCounter = 0;
                }
                else if (compCounter < 0)
                {
                    compCounter = colors.Length - 1;
                }

                component.color = colors[compCounter];
                setUpBoxScript.ComponentsColor.GetComponent<Image>().color = colors[armorCounter];
                break;
            case ThingToChange.rotate:
                transform.Rotate(0, 90 * modifier, 0);
                break;
            default:
                Debug.Log("Error!");
                break;
        }
    }

    private void RotateToCamera()
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //transform.LookAt(MainCamera.transform);
    }
    
}

/* ToDo:
 * - Ha en bestämd kontroller
 * - Spara karaktären till WorldScript
 */
