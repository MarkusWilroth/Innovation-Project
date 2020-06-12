using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardCamera
{
    public class BoardCameraMovement : MonoBehaviour
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
        public float speed;
        public int mapRotation, alterRotation, walkingRotation, xRotation, yRotation;
        private Camera mainCamera;

        public Vector3 mapPos;
        private Vector3 cameraTargetPos;
        private bool isMoving;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = mapPos;
            mainCamera = GetComponent<Camera>();
            mainCamera.fieldOfView = 60;
            xRotation = 60;
            yRotation = 90;
            isMoving = false;
        }

        // Update is called once per frame
        void Update()
        {
            switch (cameraState)
            {
                case CameraState.introState:
                    cameraState = CameraState.alterState;
                    //Debug.Log("IntroState");
                    break;
                case CameraState.loadState:
                    //Debug.Log("LoadState");
                    break;
                case CameraState.alterState:
                    if (isMoving)
                    {
                        xRotation = alterRotation;
                        MoveTarget();
                    }
                    break;
                case CameraState.povState:
                    break;
                case CameraState.mapState:
                    break;
                default:
                    Debug.Log("Error! No cameraState");
                    break;
            }
        }

        private void MoveTarget()
        {
            if (cameraTargetPos != null)
            {

                transform.position = Vector3.MoveTowards(transform.position, cameraTargetPos, speed);
                Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(xRotation, yRotation, 0), speed);

                if (transform.position == cameraTargetPos && transform.rotation == Quaternion.Euler(xRotation, yRotation, 0))
                {
                    Debug.Log("In right position and rotation!");
                    isMoving = false;
                }
            }
        }

        private void IntroPart()
        {
            bool isDone = false;
        }

        public void GetTargetPos(Vector3 moveTarget)
        {
            cameraTargetPos = moveTarget;
            cameraTargetPos.y = transform.position.y;
            isMoving = true;
        }
    }
}


