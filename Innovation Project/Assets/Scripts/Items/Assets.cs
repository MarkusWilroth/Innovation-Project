using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets
{
    public static Assets assets;

    //Items
    public ScriptDieObject[] dieTemp;

    public void GetAssets() //Får alla assets
    {
        dieTemp = GetAtPath<ScriptDieObject>("ItemFolder/DieObjects");
    }

    private static T[] GetAtPath<T>(string path)
    {
        object[] t = Resources.LoadAll(path, typeof(T));
        T[] result = new T[t.Length];

        for (int i = 0; i < t.Length; i++)
        {
            result[i] = (T)t[i];
        }

        return result;
    }
}
