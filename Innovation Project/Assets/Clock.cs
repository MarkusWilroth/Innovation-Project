using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    double secound = 1f;
    int time = 60;
    Text countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        secound -= Time.deltaTime;
        countDown.text = time.ToString();
        if (time <= 0)
        {
            Time.timeScale = 0f;
        }
        if (secound <= 0f)
        {
            secound = 1f;
            time--;
        }
    }
}
