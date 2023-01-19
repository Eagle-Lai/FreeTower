using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    public struct UIItemData
    {
        public string name;
        public string path;
        public UILayout UILayout;
    }
    public class UIData
    {
        public static Dictionary<string, UIItemData> uiDict = new Dictionary<string, UIItemData>
        {
            { "MainView", new UIItemData(){ name = "MainView", path = "UIPrefabs/MainView", UILayout = UILayout.NormalPanel} },

        };
    }
}