using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadPlayer2Controller : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("2A"))
        {
            Debug.Log("This happens");
            gameObject.AddComponent(typeof(GamepadPlayerController));
            gameObject.GetComponent<GamepadPlayerController>().ControllToPlayer(2);
            Destroy(this);
        }
    }



}

