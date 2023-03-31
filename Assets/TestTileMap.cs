using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FTProject
{
    public class TestTileMap : MonoBehaviour
    {

        public Grid grid;
        public Tilemap tilemap;
        public int MaxNum;
        // Start is called before the first frame update
        void Start()
        {
            int temp = MaxNum;
            BoundsInt boundsInt = tilemap.cellBounds;
            string result = "";
            for (int i = boundsInt.xMin; i < boundsInt.xMax; i++)
            {
                result += "\n\r";
                for (int j = boundsInt.yMin; j < boundsInt.yMax; j++)
                {
                    TileBase tileBase = tilemap.GetTile<TileBase>(new Vector3Int(i, j));
                    if (tileBase != null)
                    {
                        Debug.Log(tileBase.name);
                        switch (tileBase.name)
                        {
                            case "Start":
                                result += "*";
                                break;
                            case "End":
                                result += "&";
                                break;
                            case "Normal":
                                result += "-";
                                break;
                            case "Obstacles":
                                result += "#";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            Debug.LogError(result);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
