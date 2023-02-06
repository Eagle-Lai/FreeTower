using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public static class FTProjectUtils
    {
       public static float GetPointDistance(GameObject goA, GameObject goB)
        {
            return Vector2.Distance(new Vector2(goA.transform.position.x, goA.transform.position.z), new Vector2(goB.transform.position.x, goB.transform.position.z));
        }    

        public static float GetPointDistance(Vector3 pos1, Vector3 pos2)
        {
            return Vector2.Distance(new Vector2(pos1.x, pos1.z), new Vector2(pos2.x, pos2.z));
        }
        public static Quaternion GetRotate(Transform transform, GameObject go)
        {
            return Quaternion.LookRotation(new Vector3(transform.position.x - go.transform.position.x, 0, transform.position.z - go.transform.position.z));
        }

        public static Quaternion GetRotate(Vector3 pos1, Vector3 pos2)
        {
            return Quaternion.LookRotation(new Vector3(pos1.x - pos2.x, 0, pos1.z - pos2.z));
        }
           

        public static void SetObjParent(this Transform transform, Transform parent, Vector3 position = default, Vector3 scale = default, Quaternion quaternion = default)
        {
            transform.transform.SetParent(parent);
            transform.transform.localPosition = position;
            transform.transform.localScale = scale;
            transform.transform.localRotation = quaternion;           
        }
    }
}