using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{  
    public class AssetData
    {
        public struct AssetItemData
        {
            public string name;
            public string path;
            public UILayout UILayout;
        }

        public static Dictionary<string, AssetItemData> AssetDictionary = new Dictionary<string, AssetItemData>
        {
            { "MainView", new AssetItemData(){ name = "MainView", path = "UIPrefabs/MainView", UILayout = UILayout.NormalPanel} },
            { "SelectView", new AssetItemData(){ name = "SelectView", path = "UIPrefabs/SelectView", UILayout = UILayout.NormalPanel} },
            { "StartView", new AssetItemData(){ name = "StartView", path = "UIPrefabs/StartView", UILayout = UILayout.NormalPanel} },
            { "TowerInfoView", new AssetItemData(){ name = "TowerInfoView", path = "UIPrefabs/TowerInfoView", UILayout = UILayout.NormalPanel} },

            { "NormalTower", new AssetItemData(){name = "NormalTower", path = "Tower/NormalTower" } },
            { "PowerTower", new AssetItemData(){name = "NormalTower", path = "Tower/PowerTower" } },
            { "SlowTower", new AssetItemData(){name = "NormalTower", path = "Tower/SlowTower" } },

            { "StartPoint", new AssetItemData(){ name = "StartPoint", path = "Point/StartPoint"} },

            { "EndPoint", new AssetItemData(){ name = "EndPoint", path = "Point/EndPoint"} },

            { "NormalPoint", new AssetItemData(){ name = "NormalPoint", path = "Point/NormalPoint"} },

            { "EmptyPoint", new AssetItemData(){ name = "EmptyPoint", path = "Point/EmptyPoint"} },

            { "PointParent", new AssetItemData(){ name = "PointParent", path = "Point/PointParent"} },

            { "NormalObstacle", new AssetItemData(){ name = "NormalObstacle", path = "Obstacle/NormalObstacle"} },
            {"QuickEnemy", new AssetItemData(){name = "QuickEnemy", path = "Enemy/QuickEnemy"} },
             {"NormalEnemy", new AssetItemData(){name = "NormalEnemy", path = "Enemy/NormalEnemy"} },
            {"NormalBullet", new AssetItemData(){name = "NormalBullet", path = "Bullet/NormalBullet"} },
            {"MapModel1", new AssetItemData(){name = "MapModel1", path = "MapModel/MapModel1"} },
            {"MapModel2", new AssetItemData(){name = "MapModel2", path = "MapModel/MapModel2"} },
            {"LineRender", new AssetItemData(){name = "LineRender", path = "LineRender/LineRender"} },
            {"MapArrow", new AssetItemData(){name = "MapArrow", path = "MapModel/MapGameObjects/MapArrow"} },
        };
    }
}