using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusDumpScript : MonoBehaviour
{
    private Vector3 targetPos;
    public float speed;
    public int score;
    // Start is called before the first frame update

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);

        if (transform.position == targetPos)
        {
            Destroy(gameObject);
        }
    }

    public void GetDir(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
