/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{

    public class ObjectPool<T> where T : class, new()
    {
        private Queue<T> _FreePool;
        public Queue<T> FreePool
        {
            get
            {
                return _FreePool;
            }

        }

        private Queue<T> _BusyPool;
        public Queue<T> BusyPool
        {
            get
            {
                return _BusyPool;
            }
        }

        public ObjectPool()
        {
            _FreePool = new Queue<T>();
            _BusyPool = new Queue<T>();
        }

        public T Get<T>() where T :class
        {
            T t = default(T);
            if(_FreePool.Count > 0)
            {
                t = _FreePool.Dequeue() as T;
                return t;
            }
            return t;
        }
    }
}