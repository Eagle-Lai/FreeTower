using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class FTProjectUtils
    {
       public static float GetPointDistance(GameObject goA, GameObject goB)
        {
            return Vector2.Distance(new Vector2(goA.transform.position.x, goA.transform.position.z), new Vector2(goB.transform.position.x, goB.transform.position.z));
        }
    }
}