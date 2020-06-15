using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//----- NOTE: Disable script when making a build!!! -----

public class MakeObject
{
    [MenuItem("Assets/Create/Item/DieObject")]
    public static void CreateDieObject()
    {
        ScriptDieObject asset = ScriptableObject.CreateInstance<ScriptDieObject>();
        int itemCounter = 0;
        itemCounter = Directory.GetFiles("Assets/Resources/ItemFolder/DieObjects").Length;

        if (!(itemCounter == 0))
        {
            itemCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        }
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemFolder/DieObjects/do" + itemCounter + ".asset");
        asset.itemID = "do" + itemCounter.ToString();
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
