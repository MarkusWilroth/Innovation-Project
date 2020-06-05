using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardCamera
{

    
    public class BoardCamearMovement : MonoBehaviour
    {
        public enum CameraState
        {
            introState, //Det som händer när scenen öppnas (Kameran går igenom spelplanen)
            loadState,  //Om scenen laddas in andra gången (Kameran startar uppifrån och går dirr till nästa spelare)
            alterState, //Spelaren kan flytta på kameran
            povState,   //Kameran följer spelaren (kan inte röras)
            mapState,   //Zoomar snabbt ut så att hela spelbrädet syns
            switchCharacterState //Flyttar kameran till nästa spelare
        }

        public CameraState cameraState;
        private Camera mainCamera;

        public Vector3 mapPos;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = mapPos;
            mainCamera = GetComponent<Camera>();
            mainCamera.fieldOfView = 60;
        }

        // Update is called once per frame
        void Update()
        {
            switch (cameraState)
            {
                case CameraState.introState:
                    //Debug.Log("IntroState");
                    break;
                case CameraState.loadState:
                    //Debug.Log("LoadState");
                    break;
                default:
                    Debug.Log("Error! No cameraState");
                    break;
            }
        }
    }
}


