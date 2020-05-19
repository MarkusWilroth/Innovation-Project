using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordeRoute : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childNodeList;

    private void Start()
    {
        childNodeList = new List<Transform>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if (i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
        Gizmos.DrawLine(childNodeList[childNodeList.Count - 1].position, childNodeList[0].position);
    }

    private void FillNodes()
    {
        childNodeList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if (child != transform)
            {
                childNodeList.Add(child);
            }
        }
    }
}
