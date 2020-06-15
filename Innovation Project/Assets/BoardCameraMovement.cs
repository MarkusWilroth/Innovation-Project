using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardCamera {
    public class BoardCameraMovement : MonoBehaviour {
        public enum CameraState {
            introState, //Det som händer när scenen öppnas (Kameran går igenom spelplanen)
            loadState,  //Om scenen laddas in andra gången (Kameran startar uppifrån och går dirr till nästa spelare)
            alterState, //Spelaren kan flytta på kameran
            povState,   //Kameran följer spelaren (kan inte röras)
            mapState,   //Zoomar snabbt ut så att hela spelbrädet syns
            switchCharacterState, //Flyttar kameran till nästa spelare
            dieRollState //Kameran är plan och hela spelaren syns samt tärningarna
        }

        public CameraState cameraState;
        public float speed;
        public int mapRotation, alterRotation, moveRotation, dieRotation, mapY, alterY, walkY, dieY;
        private int xRotation, yRotation, playerNr;
        private float yPos;
        private Camera mainCamera;
        public GameObject activeCharacter;

        public Vector3 alterAdjust, moveAdjust, rollAdjust;

        public Vector3 mapPos;
        private Vector3 cameraTargetPos;
        private bool isMoving;

        // Start is called before the first frame update
        void Start() {
            transform.position = mapPos;
            mainCamera = GetComponent<Camera>();
            mainCamera.fieldOfView = 60;
            xRotation = 60;
            yRotation = 90;
            isMoving = false;
        }

        // Update is called once per frame
        void Update() {
            switch (cameraState) {
                case CameraState.introState:
                    cameraState = CameraState.alterState;
                    //Debug.Log("IntroState");
                    break;
                case CameraState.loadState:
                    //Debug.Log("LoadState");
                    break;
                case CameraState.alterState:
                    if (isMoving) {
                        MoveTarget();
                    }
                    break;
                case CameraState.povState:
                    if (isMoving) {
                        MoveTarget();
                    }
                    break;
                case CameraState.mapState:
                    break;
                default:
                    Debug.Log("Error! No cameraState");
                    break;
            }
        }

        private void MoveTarget() {
            if (cameraTargetPos != null) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, cameraTargetPos, speed);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(xRotation, yRotation, 0), speed);
                if (transform.localPosition == cameraTargetPos && transform.localRotation == Quaternion.Euler(xRotation, yRotation, 0)) {
                    isMoving = false;
                }
            }
        }

        private void IntroPart() {
            bool isDone = false;
        }

        public void GetTargetPos(Vector3 moveTarget) {
            cameraTargetPos = moveTarget;
            cameraTargetPos.y = yPos;
            isMoving = true;
        }

        public void PovMode() {
            isMoving = true;
            cameraTargetPos = moveAdjust;
            xRotation = moveRotation;

            cameraState = CameraState.povState; //Sätter cameraState till dieRoll
        }

        public void AdjustMode() {
            isMoving = true;
            cameraTargetPos = alterAdjust;
            xRotation = alterRotation;

            cameraState = CameraState.alterState; //Sätter cameraState till dieRoll
        }

        public void SetRollState() {
            isMoving = true;
            cameraTargetPos = rollAdjust;
            xRotation = dieRotation;

            cameraState = CameraState.povState; //Sätter cameraState till dieRoll
        }

        public void GetStep(GameObject step) {
            gameObject.transform.SetParent(step.transform, true);
        }

        public void GetPlayer(GameObject player, int playerNr) {
            gameObject.transform.SetParent(player.transform, true);
            this.playerNr = playerNr;
        }
    }
}


