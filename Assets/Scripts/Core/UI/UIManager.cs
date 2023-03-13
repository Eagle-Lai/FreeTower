using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UILayout
{
    BgPanel,
    NormalPanel,
    TipsPanel,
}

namespace FTProject
{
    public class UIManager : BaseManager<UIManager>
    {
       
        private Dictionary<UILayout, RectTransform> layoutDicitonary = new Dictionary<UILayout, RectTransform>();

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
            if (layoutDicitonary.TryGetValue(type, out rect))
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
            layoutDicitonary.Add(type, rect);
            return layoutDicitonary[type];
        }


        public override void OnInit()
        {
            base.OnInit();
            TimerManager.Instance.AddTimer(1, -1, OnDestroyView);
        }

        private void OnDestroyView()
        {
            for (int i = 0; i < panelDictionary.Count;)
            {
                KeyValuePair<string, BaseView> view = panelDictionary.ElementAt(i);
                if (view.Value.isShow == false)
                {
                    view.Value.activeTime -= 1;
                    if (view.Value.activeTime <= 0)
                    {
                        panelDictionary.Remove(view.Value.UIItemData.name);
                        continue;
                    }
                }
                i++;
            }
        }

        public T OpenView<T>(string ViewName, object[] data = null) where T : BaseView, new()
        {
            if (AssetData.AssetDictionary.TryGetValue(ViewName, out AssetData.AssetItemData item))
            {
                T t = null;
                if (panelDictionary.ContainsKey(ViewName))
                {

                    BaseView baseView = panelDictionary[ViewName];
                    baseView.OnShow();
                    t.isShow = false;
                    return baseView as T;
                }
                else
                {
                    RectTransform parent = GetPanelLayoutParent(item.UILayout);
                    GameObject go = ResourcesManager.Instance.LoadAndInitGameObject(item.name, parent, (obj) => 
                    {
                        if (obj != null)
                        {
                            t = new T();
                            t.OnInit();
                            t.SetGameObject(obj);
                            t.SetUIItem(item);
                            t.SetParent(parent.transform);
                            t.SetData(data);
                            t.OnShow();
                            t.isShow = true;
                            panelDictionary[ViewName] = t;
                        }                        
                    });
                    return t;
                }
            }
            return null;
        }

        public void CloseView(string viewName)
        {
            if (panelDictionary.TryGetValue(viewName, out BaseView baseView))
            {
                baseView.OnHide();
                baseView.isShow = false;
                baseView.CloseSelf();
            }
        }

        public T GetView<T>(string viewName) where T : BaseView
        {
            if (panelDictionary.TryGetValue(viewName, out BaseView baseView))
            {
                return baseView as T;
            }
            return null;
        }
    }
}