using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTreeScript : MonoBehaviour
{
    Material treeColor;
    public Color healthy, sick;
    // Start is called before the first frame update
    void Start()
    {
        healthy = GetComponent<MeshRenderer>().materials[1].color;
        //sick = Color.yellow;
        treeColor = GetComponent<MeshRenderer>().materials[1];

        treeColor.color = sick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
