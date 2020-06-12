using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Step
{
    public enum StepType
    {
        startStep,
        shopStep,
        step
    }

    public class StepScript : MonoBehaviour
    {
        public int stepId; //Används för att se till att spelaren går frammåt
        public List<GameObject> CharacterOnStepList;
        public GameObject[] nextStep;
        public bool chooseStep, pathWayStep;
        public StepType stepType;
        private float spaceOffsetH = 2f, spaceOffsetV = 1.7f;

        //public Vector3 oneCharacterPos;
        //public Vector3[] twoCharacterPos;
        //public Vector3[] threeCharacterPos;
        public Vector3[] characterPos = new Vector3[4];

        public Vector3 entryPointPos;
        private Vector3 MittTopPos, VänsterTopPos, HögerTopPos;
        private Vector3 MittMittPos, VänsterMittPos, HögerMittPos;
        private Vector3 MittBotPos, VänsterBotPos, HögerBotPos;

        private bool hasSteps = false;

        private void Start()
        {
            CharacterOnStepList = new List<GameObject>();

            //oneCharacterPos = new Vector3(transform.position.x, transform.position.y);
            //twoCharacterPos = new Vector3[] { new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x, transform.position.y) };
            StepVectors();

            for (int i = 0; i < characterPos.Length; i++)
            {
                characterPos[i] = new Vector3(transform.position.x, transform.position.y);
            }
            AllignCharacterOnStep();
        }

        public void AddCharacter(GameObject character)
        {
            if (!hasSteps)
            {
                StepVectors();
                hasSteps = true;
            }
            
            CharacterOnStepList.Add(character);
            AllignCharacterOnStep();
        }

        public void RemoveCharacter(GameObject character)
        {
            CharacterOnStepList.Remove(character);
            AllignCharacterOnStep();
        }

        private void AllignCharacterOnStep()
        {
            int characterCounter = CharacterOnStepList.Count;
            switch(characterCounter)
            {
                case 0:
                    break;
                case 1:
                    characterPos[0] = MittMittPos;                          //Mitt mitt
                    break;
                case 2:
                    characterPos[0] = VänsterMittPos;               //Vänster Mitt
                    characterPos[1] = HögerMittPos;                //Höger Mitt
                    break;
                case 3:
                    characterPos[0] = MittTopPos;                  //Top Mitt
                    characterPos[1] = VänsterBotPos; //Bot Vänster
                    characterPos[2] = HögerBotPos;   //Bot Höger
                    break;
                case 4:
                    characterPos[0] = HögerTopPos;   //Top Höger
                    characterPos[1] = VänsterTopPos; //Top Vänster
                    characterPos[2] = HögerBotPos;   //Bot Vänster
                    characterPos[3] = VänsterBotPos; //Bot Höger
                    break;
                default:
                    Debug.Log("Error: More then four on step! (HOW?)");
                    break;
            }   //Uppdaterar de relevanta positionerna

            for (int i = 0; i < CharacterOnStepList.Count; i++)
            {
                CharacterOnStepList[i].transform.position = characterPos[i]; //Flyttar karaktären till nya position (Använd moveTowards senare)
            }
        }

        private void StepVectors() {
            MittMittPos = new Vector3(transform.position.x, transform.position.y + spaceOffsetV, transform.position.z);
            VänsterMittPos = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z);
            HögerMittPos = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z);

            MittTopPos = new Vector3(transform.position.x, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);
            VänsterTopPos = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);
            HögerTopPos = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);

            MittBotPos = new Vector3(transform.position.x, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
            VänsterBotPos = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
            HögerBotPos = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);

            entryPointPos = MittMittPos;
        }
    }
    
}



