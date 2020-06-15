using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManagerScript : MonoBehaviour {
    public GameObject D6;   //Vilken form tärningen har (Hur många sidor den har)
    public int dieOffset; //Avståndet mellan tärningarna
    public float dieSpeed;
    public float startY;

    private List<GameObject> diesToRollList, landedDies; //De tärningar som ska rullas
    private GameObject holder; //Simple holder ser till att gameobjct får rätt värden
    private BoardPlayerScript boardPlayerScript; //Fås i GetDie och det är här den sen ska ge tillbaka ett värde

    private int rolledValue, totalRoll;
    private float forceDown, g;
    private bool isRolling;

    private void Start() {
        diesToRollList = new List<GameObject>();
        isRolling = false;
        forceDown = 0;
        g = 0.00005f;
        startY = 1.2f;

        if (Assets.assets == null) { //Flytta till gameboard
            Assets.assets = new Assets();
            Assets.assets.GetAssets();
        }
    }

    public void GetDie(string[] diesToRoll, BoardPlayerScript boardPlayerScript) {
        this.boardPlayerScript = boardPlayerScript;

        foreach (string dieId in diesToRoll) {
            foreach (ScriptDieObject die in Assets.assets.dieTemp) {
                if (die.itemID == dieId) {
                    switch (die.dieEyes.Length) {
                        case 6: //Det är en d6:a
                            CreateDie(die, D6, boardPlayerScript.gameObject.transform); //Skapar en tärning med dessa sidor
                            break;
                    }
                    break;
                }
            }
        }
        
        AdjustDies();
        //Har lagt till alla rätta tärningar och ska rulla
    }

    public void RollDies() {
        landedDies = new List<GameObject>();
        isRolling = true;
    }

    private void AdjustDies() { //Flyttar de så de hamnar på rätt position
        Vector3 diePos = new Vector3(0, startY, 0);
        if (diesToRollList.Count > 1) {
            Debug.Log("Rullade fler än en tärning!");
        } else {
            diesToRollList[0].transform.localPosition = diePos;
        }
    }

    private void CreateDie(ScriptDieObject die, GameObject prefab, Transform parent) {
        holder = Instantiate(prefab);
        holder.GetComponent<DieScript>().GetData(die.dieEyes, die.itemName, die.itemTier);
        holder.transform.SetParent(parent, false);
        diesToRollList.Add(holder);
    }

    private void Update() {
        if (isRolling) {
            foreach (GameObject die in diesToRollList) {
                Vector2 diePos = die.transform.localPosition;
                diePos.y += dieSpeed - forceDown;
                die.transform.localPosition = diePos;

                if (die.transform.localPosition.y < startY) { //Tärningen är nere igen
                    rolledValue = die.GetComponent<DieScript>().ReturnValue();
                    totalRoll += rolledValue;
                    landedDies.Add(die);
                    diesToRollList.Remove(die);
                    break;
                }
            }

            forceDown += g;
            
            if (diesToRollList.Count <= 0) { //Alla tärningar är på plats
                
                boardPlayerScript.RollResult(totalRoll);
                diesToRollList.Clear(); //Bara för att vara säker...
                forceDown = 0;
                totalRoll = 0;
                rolledValue = 0;

                foreach (GameObject die in landedDies) {
                    Destroy(die.gameObject);
                }
                isRolling = false;
            }
        }
    }
    private void SpinDies(GameObject die, bool getToPos) { //Roterar tärningarna

    }

}
