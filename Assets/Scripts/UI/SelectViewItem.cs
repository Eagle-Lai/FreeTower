using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    public class SelectViewItem : MonoBehaviour
    {
        public bool isInit;
        private void Awake()
        {
            isInit = false;
        }

        public void Init()
        {
            isInit = true;
        }

        public void MarkAsInited()
        {
            isInit = true;
        }

        public void SetItemData()
        {

        }
    }
}
