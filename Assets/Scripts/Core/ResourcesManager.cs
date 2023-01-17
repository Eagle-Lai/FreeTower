using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    public static ResourcesManager _instance;

    public static ResourcesManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ResourcesManager();
            }
            return _instance;
        }
    }

    public GameObject LoadAndInitGameObject(string name)
    {
        return LoadAndInitGameObject(name, null, null);
    }

    public GameObject LoadAndInitGameObject(string name, Transform transform)
    {
        return LoadAndInitGameObject(name, transform, null);
    }


    public GameObject LoadAndInitGameObject(string name, Transform parent, Action<GameObject> callback)
    {
        GameObject obj = Resources.Load<GameObject>(name);
        if (obj != null)
        {
            GameObject go = GameObject.Instantiate(obj);

            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            if (callback != null)
            {
                callback(go);
            }
            return go;
        }
        return null;
    }
}
