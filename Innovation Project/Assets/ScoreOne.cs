using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOne : MonoBehaviour
{

    Text score;
    int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = points.ToString();
    }

    public void ChangePoints(int points)
    {
        this.points += points;
    }
}
