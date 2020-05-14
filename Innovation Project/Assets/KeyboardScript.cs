using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour
{

    public Sprite keySmall, keyBig;
    public GameObject key, space, done;
    public float startX, startY;

    private int startNrChar, startSmallChar, startBigChar;

    public Vector2 spacePos, donePos;

    private string[] intArr = new string[] { 1.ToString(), 2.ToString(), 3.ToString(), 4.ToString(), 5.ToString(), 6.ToString(), 7.ToString(), 8.ToString(), 9.ToString(), 0.ToString() };
    private char[] smallArr = new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', '-', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '@', '.', '_' };
    private char[] largeArr = new char[] { 'Q', 'W', 'E', 'T', 'Y', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', '-', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '@', '.', '_' };

    private Vector2 keyPos;
    private Vector2[] keyPosArr;
    private bool isSmall;
    private GameObject[] keyArr;

    private int playerNr = 1;
    private int activeKey;
    private char[] inputName;
    private int inputPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        startNrChar = 49;
        startSmallChar = 97;
        startBigChar = 65;
        keyPos = new Vector2(startX, startY);
        keyArr = new GameObject[56];

        //Create keys
        keyPosArr = new Vector2[56];
        MakeKeyboard(false, null, 1);
    }

    private void MakeKeyboard(bool isSmall, string playerName, int playerNr)
    {
        foreach (Transform child in gameObject.transform) //Rensar gamla keys
        {
            Destroy(child.gameObject);
        }

        inputName = new char[8];
        if (playerName != null)
        {
            int count = 0;
            foreach (char ch in name)
            {
                inputName[count] = ch;
                count++;

                if (count >= 8)
                {
                    break;
                }
            }
        }//Uppdaterar spelarnamnet

        keyPos = new Vector2(startX, startY);
        int keyCounter = 0;

        if (isSmall)
        {
            GetComponent<Image>().sprite = keySmall;
        } else
        {
            GetComponent<Image>().sprite = keyBig;
        }

        for (int i = 0; i < keyPosArr.Length; i++)
        {
            
            if (i < 40) //Behöver inte räkna med space
            {
                if (i < 10) 
                {
                    CreateKey(keyPos, i, System.Convert.ToChar(intArr[keyCounter]));
                    keyCounter++;
                    if (keyCounter == 10)
                    {
                        keyCounter = 0;
                    }
                }
                else
                {
                    if (isSmall)
                    {
                        CreateKey(keyPos, i, smallArr[keyCounter]);
                    } else
                    {
                        CreateKey(keyPos, i, largeArr[keyCounter]);
                    }
                    
                    keyCounter++;
                }

                if ((i+1)%10 == 0) //Ny rad
                {
                    keyPos.x = startX;
                    keyPos.y -= 27;
                } else
                {
                    keyPos.x += 33;
                }
            } else //Sista raden med alla specialknappar
            {

            }
            
        }
        keyArr[0].SetActive(true);
    }
    
    private void CreateKey(Vector2 pos, int id, char ch) //value är char value från Avicc tabell
    {
        keyArr[id] = Instantiate(key);
        keyArr[id].transform.position = keyPos;
        keyArr[id].transform.SetParent(transform, false);
        keyArr[id].name = "Key: " + ch;

        keyArr[id].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(playerNr + "B")) //Om spelaren klickar snabbkommando space
        {
            inputName[inputPos] = ' ';
            inputPos++;
        }
    }
}
