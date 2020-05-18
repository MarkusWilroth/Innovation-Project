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
        public StepType stepType;
        private float spaceOffsetH = 0.3f, spaceOffsetV = 0.25f;

        //public Vector3 oneCharacterPos;
        //public Vector3[] twoCharacterPos;
        //public Vector3[] threeCharacterPos;
        public Vector3[] characterPos = new Vector3[4];

        private void Start()
        {
            CharacterOnStepList = new List<GameObject>();

            //oneCharacterPos = new Vector3(transform.position.x, transform.position.y);
            //twoCharacterPos = new Vector3[] { new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x, transform.position.y) };

            for (int i = 0; i < characterPos.Length; i++)
            {
                characterPos[i] = new Vector3(transform.position.x, transform.position.y);
            }
        }

        public void AddCharacter(GameObject character)
        {
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
                    characterPos[0] = new Vector3(transform.position.x, transform.position.y + spaceOffsetV, transform.position.z);
                    break;
                case 2:
                    characterPos[0] = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z);
                    characterPos[1] = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z);
                    break;
                case 3:
                    characterPos[0] = new Vector3(transform.position.x, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);
                    characterPos[1] = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
                    characterPos[2] = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
                    break;
                case 4:
                    characterPos[0] = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);
                    characterPos[1] = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z + spaceOffsetH);
                    characterPos[2] = new Vector3(transform.position.x + spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
                    characterPos[3] = new Vector3(transform.position.x - spaceOffsetH, transform.position.y + spaceOffsetV, transform.position.z - spaceOffsetH);
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
    }
    
}



