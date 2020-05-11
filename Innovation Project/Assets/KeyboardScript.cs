using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{

    public Sprite keySmall, keyBig;
    public GameObject key, space, done;
    public float startX, startY;

    private int startNrChar, startSmallChar, startBigChar;

    public Vector2 spacePos, donePos;

    private Vector2 keyPos;
    private Vector2[] keyPosArr;
    private bool isSmall;
    private GameObject[] keyArr;

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
        MakeKeyboard();
    }

    private void MakeKeyboard()
    {
        foreach (Transform child in gameObject.transform) //Rensar gamla keys
        {
            Destroy(child.gameObject);
        }
        keyPos = new Vector2(startX, startY);
        int keyCounter = 0;

        for (int i = 0; i < keyPosArr.Length; i++)
        {
            if (i < 10) //nr char (49-57) - 0 är 48 men tangentbordet börjar på 1
            {
                CreateKey(keyPos, i, startNrChar + keyCounter);
                keyCounter++;
            }
            if (i < 40) //Behöver inte räkna med space
            {
                if ((i+1)%10 == 0) //Ny rad
                {
                    keyPos.x = startX;
                    keyPos.y -= 26;
                } else
                {
                    keyPos.x += 33;
                }
            } else
            {

            }
            
        }
    }

    private void CreateKey(Vector2 pos, int id, int value) //value är char value från Avicc tabell
    {
        keyArr[id] = Instantiate(key);
        keyArr[id].transform.position = keyPos;
        //Ge key id
        keyArr[id].transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
