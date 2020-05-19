using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderScript : MonoBehaviour
{
    public float leftBoundary, rightBoundary, botBoundary, topBoundary;
    private Vector3 topLeft, topRight, botLeft, botRight;
    private void Start()
    {
        topLeft = new Vector3(leftBoundary, 1, topBoundary);
        topRight = new Vector3(rightBoundary, 1, topBoundary);
        botLeft = new Vector3(leftBoundary, 1, botBoundary);
        botRight = new Vector3(rightBoundary, 1, botBoundary);
    }

    private void OnDrawGizmos()
    {
        topLeft = new Vector3(leftBoundary, 0.2f, topBoundary);
        topRight = new Vector3(rightBoundary, 0.2f, topBoundary);
        botLeft = new Vector3(leftBoundary, 0.2f, botBoundary);
        botRight = new Vector3(rightBoundary, 0.2f, botBoundary);
        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
    }
}
