/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    public struct AssetItemData
    {
        public string name;
        public string path;
        public UILayout UILayout;
    }
    public class AssetData
    {
        public static Dictionary<string, AssetItemData> AssetDictionary = new Dictionary<string, AssetItemData>
        {
            { "MainView", new AssetItemData(){ name = "MainView", path = "UIPrefabs/MainView", UILayout = UILayout.NormalPanel} },

            { "NormalTower", new AssetItemData(){name = "NormalTower", path = "Tower/NormalTower" } },
            { "PowerTower", new AssetItemData(){name = "NormalTower", path = "Tower/PowerTower" } },
            { "SlowTower", new AssetItemData(){name = "NormalTower", path = "Tower/SlowTower" } },

            { "StartNode", new AssetItemData(){ name = "StartNode", path = "Node/StartNode"} },

            { "EndNode", new AssetItemData(){ name = "EndNode", path = "Node/EndNode"} },

            { "NormalNode", new AssetItemData(){ name = "BaseNode", path = "Node/NormalNode"} },

            { "NodeParent", new AssetItemData(){ name = "MapParent", path = "Node/NodeParent"} },

            { "NormalObstacle", new AssetItemData(){ name = "NormalObstacle", path = "Obstacle/NormalObstacle"} },
            {"NormalEnemy", new AssetItemData(){name = "NormalEnemy", path = "Enemy/NormalEnemy"} },

        };
    }
}