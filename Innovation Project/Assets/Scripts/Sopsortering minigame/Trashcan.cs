using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer test;
    MeshRenderer testTwo;
    Trash trash;
    Sounds sound;
    AudioSource aS;
    public ScoreOne scoreOne, scoreTwo, scoreThree, scoreFour;

    void Start()
    {
        test = GetComponent<MeshRenderer>();
        aS = GetComponent<AudioSource>();
        sound = GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            testTwo = collision.gameObject.GetComponent<MeshRenderer>();
            trash = collision.gameObject.GetComponent<Trash>();
            if (test.material.color == testTwo.material.color)
            {
                aS.PlayOneShot(sound.Traffa, 0.5f);
                if (trash.holder == 1)
                {
                    scoreOne.ChangePoints(10);
                }
                else if (trash.holder == 2)
                {
                    scoreTwo.ChangePoints(10);
                }
                else if (trash.holder == 3)
                {
                    scoreThree.ChangePoints(10);
                }
                else if (trash.holder == 4)
                {
                    scoreFour.ChangePoints(10);
                }
                Destroy(collision.gameObject);
            }
            else
            {
                aS.PlayOneShot(sound.fel, 0.5f);
                if (trash.holder == 1)
                {
                    scoreOne.ChangePoints(-10);
                }
                else if (trash.holder == 2)
                {
                    scoreTwo.ChangePoints(-10);
                }
                else if (trash.holder == 3)
                {
                    scoreThree.ChangePoints(-10);
                }
                else if (trash.holder == 4)
                {
                    scoreFour.ChangePoints(-10);
                }
                Destroy(collision.gameObject);

            }
        }
    }
}
