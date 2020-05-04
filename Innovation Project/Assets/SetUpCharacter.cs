using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SetUpCharacter : MonoBehaviour
{
    private Material limb, skin, armor, component;
    private Color[] colors;
    public int playerNr;
    private int armorCounter, skinCounter, limbCounter, compCounter;
    float rotateCounter;

    public bool isReady;
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

        RotateToCamera();

        toChange = ThingToChange.rotate;
        playerNr = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown(playerNr+"L1"))
        {
            AlterCharacter(-1);
        } else if (Input.GetButtonDown(playerNr + "R1"))
        {
            AlterCharacter(1);
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
        transform.LookAt(MainCamera.transform);
        rotateCounter = transform.rotation.y;
    }
    
}

/* ToDo:
 * - Rotera till kameran
 * - Ha en bestämd kontroller
 * - Ha bestämda färger
 * - Spara karaktären till WorldScript
 */
