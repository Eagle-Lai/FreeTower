using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject {
    public class ResourcesManager
    {
        public static ResourcesManager _instance;

        public static ResourcesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourcesManager();
                }
                return _instance;
            }
        }

        public GameObject LoadAndInitGameObject(string name)
        {
            return LoadAndInitGameObject(name, null, null, Vector3.zero, Vector3.one,Quaternion.identity);
        }

        public GameObject LoadAndInitGameObject(string name, Transform parent)
        {
            return LoadAndInitGameObject(name, parent, null, Vector3.zero, Vector3.one,Quaternion.identity);
        }

        public GameObject LoadAndInitGameObject(string name, Transform parent, Vector3 position)
        {
            return LoadAndInitGameObject(name, parent, null, position, Vector3.one, Quaternion.identity);
        }

        public GameObject LoadAndInitGameObject(string name, Transform parent, Action<GameObject> callback)
        {
            return LoadAndInitGameObject(name, parent, callback, Vector3.zero, Vector3.one, Quaternion.identity);
        }

        public GameObject LoadAndInitGameObject(string name, Transform parent, Action<GameObject> callback, Vector3 position, Vector3 scale)
        {
            return LoadAndInitGameObject(name, parent, callback, position, scale, Quaternion.identity);
        }



        public GameObject LoadAndInitGameObject(string name, Transform parent, Action<GameObject> callback, Vector3 position, Vector3 scale, Quaternion rot)
        { 
            if (AssetData.AssetDictionary.TryGetValue(name, out AssetData.AssetItemData item))
            {
                GameObject obj = Resources.Load<GameObject>(item.path);
                
                if (obj != null)
                {
                    GameObject go = GameObject.Instantiate(obj);
                    go.name = go.name.Replace("(Clone)", "");
                    go.transform.SetParent(parent);
                    go.transform.localPosition = position;
                    go.transform.localScale = scale;
                    go.transform.rotation = rot;
                    if (callback != null)
                    {
                        callback(go);
                    }
                    return go;
                }
            }
            return null;
        }


        public GameObject LoadUI(string name)
        {

            return null;
        }
    }
}