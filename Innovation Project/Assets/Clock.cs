using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    double secound = 1f;
    int time = 30;
    Text countDown;
    Sounds sounds;
    AudioSource clock;
    public ScoreOne[] scores;
    int higestScore = 0;
    string player;
    string winner;
    // Start is called before the first frame update
    void Start()
    {
        countDown = GetComponent<Text>();
        sounds = GetComponent<Sounds>();
        clock = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        secound -= Time.deltaTime;
        countDown.text = time.ToString();
        if (time <= 0)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (i == 0 || scores[i].points > higestScore)
                {
                    higestScore = scores[i].points;
                    player = "player " + (i + 1);
                }
            }
            countDown.text = "winner: " + player;
            Time.timeScale = 0f;
        }
        if (secound <= 0f)
        {
            secound = 1f;
            time--;
        }
        if (time == 3)
        {
            clock.PlayOneShot(sounds.countDown, 0.1f);
        }
    }
}
