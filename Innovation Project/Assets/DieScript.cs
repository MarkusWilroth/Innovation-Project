using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieScript : MonoBehaviour {
    public TMP_Text[] dieSides;
    private int[] sideValueArr;
    private string dieName;
    private int dieTier;

    public void GetData(int[] sideValueArr, string dieName, int dieTier) {
        this.sideValueArr = sideValueArr;

        for (int i = 0; i < sideValueArr.Length; i++) {
            dieSides[i].text = this.sideValueArr[i].ToString();
        }
    }

    public int ReturnValue() {
        int value = 0;
        float closestDist = int.MaxValue;
        int childCounter = 0;

        foreach (Transform child in transform) { //Går igenom alla sidor
            if (child.position.x <= closestDist) { //Om sidan är närmare skärmen är det den som förmodligen är den rätta sidan
                value = sideValueArr[childCounter];
            }
            childCounter++;
        }

        return value;
    }
}
