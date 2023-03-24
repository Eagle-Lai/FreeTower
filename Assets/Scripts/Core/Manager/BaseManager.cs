using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FTProject
{
    public class BaseManager<T> : IManagerInterface where T : class, new()
    {
        public virtual void OnInit()
        {
            //Debug.LogError("##################");
        }

        public virtual void OnDestroy()
        {

        }

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
    }
}
