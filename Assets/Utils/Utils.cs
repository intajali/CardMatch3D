using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Component Generate<T>(GameObject prefabObj, Transform parent = null) where T : Component
    {
        GameObject obj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
        if (parent != null) obj.transform.SetParent(parent, false);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.identity;
        return obj.GetComponent<T>();
    }
}
