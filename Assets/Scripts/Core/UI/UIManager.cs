using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UILayout
{
    BgPanel,
    NormalPanel,
    TipsPanel,
}

namespace FTProject
{
    public class UIManager : BaseManager
    {


        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIManager();
                }
                return _instance;
            }
        }

       
        private Dictionary<UILayout, RectTransform> _panelDictionary = new Dictionary<UILayout, RectTransform>();

        private RectTransform _uiCanvas;

        private Dictionary<string, BaseView> panelDictionary = new Dictionary<string, BaseView>();

        public RectTransform UICanvas
        {
            get
            {
                if (_uiCanvas == null)
                {
                    _uiCanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<RectTransform>();
                }
                return _uiCanvas;
            }
        }

        public RectTransform GetPanelLayoutParent(UILayout type)
        {
            RectTransform rect;
            if (_panelDictionary.TryGetValue(type, out rect))
            {
                return rect;
            }

            if (type == UILayout.BgPanel)
            {
                rect = UICanvas.transform.Find("BgPanel").GetComponent<RectTransform>();
            }
            else if (type == UILayout.NormalPanel)
            {
                rect = UICanvas.transform.Find("NormalPanel").GetComponent<RectTransform>();
            }
            else if (type == UILayout.TipsPanel)
            {
                rect = UICanvas.transform.Find("TipsPanel").GetComponent<RectTransform>();
            }
            _panelDictionary.Add(type, rect);
            return _panelDictionary[type];
        }


        public override void OnInit()
        {
            base.OnInit();
            TimerManager.Instance.AddTimer(1, -1, OnDestroyView);
        }

        private void OnDestroyView()
        {

        }

        public T OpenView<T>(string ViewName, object[] data = null) where T : BaseView, new()
        {
            if (UIData.uiDict.TryGetValue(ViewName, out UIItemData item))
            {
                T t = null;
                if (panelDictionary.ContainsKey(ViewName))
                {

                    BaseView baseView = panelDictionary[ViewName];
                    baseView.OnShow();
                    return baseView as T;
                }
                else
                {
                    GameObject go = ResourcesManager.Instance.LoadAndInitGameObject(item.path);
                    if (go != null)
                    {
                        t = new T();
                        t.OnInit();
                        t.SetGameObject(go);
                        RectTransform parent = GetPanelLayoutParent(item.UILayout);
                        t.SetParent(parent.transform);
                        t.SetData(data);
                        t.OnShow();
                        panelDictionary[ViewName] = t;
                        return t;
                    }
                }
            }
            return null;
        }

        public void CloseView(string viewName)
        {
            if (panelDictionary.TryGetValue(viewName, out BaseView baseView))
            {
                baseView.OnHide();
            }
        }
    }
}