using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace FTProject
{
    public class TimerItem
    {
        public int id;
        public float interval;
        public int loopTimes;

        public UnityEngine.Object callbackObject;
        public object[] args;

        public Action callback;
        public Action<UnityEngine.Object> callbackObjectEvent;
        public Action<UnityEngine.Object, object[]> callbackObjAndArgs;
        public bool isNotLimtied;
        public bool isStop;

        private float _interval;



        public TimerItem(int id, float interval, int loopTimes, Action action)
        {
            this.id = id;
            this.interval = interval;
            this.loopTimes = loopTimes;
            this.callback = action;
            isStop = false;
            _interval = this.interval;
            isNotLimtied = loopTimes == -1;
        }

        public TimerItem(int id, float interval, int loopTimes, Action<UnityEngine.Object> action, UnityEngine.Object obj)
        {
            this.interval = interval;
            this.loopTimes = loopTimes;
            this.callbackObjectEvent = action;
            this.callbackObject = obj;
            isStop = false;
            _interval = this.interval;
            isNotLimtied = loopTimes == -1;
        }
        public TimerItem(int id, float interval, int loopTimes, Action<UnityEngine.Object, object[]> action, UnityEngine.Object obj, object[] args)
        {
            this.interval = interval;
            this.loopTimes = loopTimes;
            this.callbackObjAndArgs = action;
            this.callbackObject = obj;
            this.args = args;
            isStop = false;
            _interval = this.interval;
            isNotLimtied = loopTimes == -1;
        }

        public void Reset()
        {
            this.interval = _interval;
            loopTimes--;
        }
    }

    public class TimerManager
    {
        public static TimerManager _timerManager;
        public static TimerManager Instance
        {
            get
            {
                if (_timerManager == null)
                {
                    _timerManager = new TimerManager();
                }
                return _timerManager;
            }
        }
        private Dictionary<int, TimerItem> _timerDicti;
        public int actionIndex;
        public TimerManager()
        {
            _timerDicti = new Dictionary<int, TimerItem>();
            actionIndex = 0;
        }
        /// <summary>
        /// 添加一个计时器，返回计时器的 ID
        /// </summary>
        /// <param name="interval">时间间隔</param>
        /// <param name="loopTimes">执行次数，-1表示无限执行</param>
        /// <param name="callback">执行函数</param>
        /// <returns></returns>
        public int AddTimer(float interval, int loopTimes, Action callback)
        {
            TimerItem item = new TimerItem(actionIndex, interval, loopTimes, callback);
            actionIndex++;
            if (!_timerDicti.ContainsKey(actionIndex))
            {
                _timerDicti.Add(actionIndex, item);
            }
            return actionIndex;
        }
        /// <summary>
        /// 添加一个计时器，返回计时器的 ID
        /// </summary>
        /// <param name="interval">时间间隔</param>
        /// <param name="loopTimes">执行次数，-1表示无限执行</param>
        /// <param name="callback">执行函数</param>
        /// <returns></returns>
        public int AddTimer(float interval, int loopTimes, Action<UnityEngine.Object> action, UnityEngine.Object obj)
        {
            TimerItem item = new TimerItem(actionIndex, interval, loopTimes, action, obj);
            actionIndex++;
            if (!_timerDicti.ContainsKey(actionIndex))
            {
                _timerDicti.Add(actionIndex, item);
            }
            return actionIndex;
        }
        /// <summary>
        /// 添加一个计时器，返回计时器的 ID
        /// </summary>
        /// <param name="interval">时间间隔</param>
        /// <param name="loopTimes">执行次数，-1表示无限执行</param>
        /// <param name="callback">执行函数</param>
        /// <returns></returns>
        public int AddTimer(float interval, int loopTimes, Action<UnityEngine.Object, object[]> action, UnityEngine.Object obj, object[] args)
        {
            TimerItem item = new TimerItem(actionIndex, interval, loopTimes, action, obj, args);
            actionIndex++;
            if (!_timerDicti.ContainsKey(actionIndex))
            {
                _timerDicti.Add(actionIndex, item);
            }
            return actionIndex;
        }

        public void Update(float intervalTime)
        {
            for (int i = 0; i < _timerDicti.Count;)
            {
                KeyValuePair<int, TimerItem> item = _timerDicti.ElementAt(i);
                i++;
                if (!item.Value.isStop)
                {
                    if (item.Value.isNotLimtied || item.Value.loopTimes > 0)
                    {
                        item.Value.interval -= intervalTime;
                        if (item.Value.interval <= 0)
                        {
                            if (item.Value.callback != null)
                            {
                                item.Value.callback();
                            }
                            if (item.Value.callbackObjectEvent != null && item.Value.callbackObject != null)
                            {
                                item.Value.callbackObjectEvent(item.Value.callbackObject);
                            }
                            if (item.Value.callbackObjAndArgs != null && item.Value.callbackObject != null && item.Value.args != null)
                            {
                                item.Value.callbackObjAndArgs(item.Value.callbackObject, item.Value.args);
                            }
                            item.Value.Reset();
                        }
                    }
                    else
                    {
                        i--;
                        _timerDicti.Remove(item.Key);
                    }
                }
            }
        }

        public void StopTimerById(int id, bool isStop = true)
        {
            if (_timerDicti.ContainsKey(id))
            {
                _timerDicti[id].isStop = isStop;
            }
        }

        public void RemoveTimerById(int id)
        {
            if (_timerDicti.ContainsKey(id))
            {
                _timerDicti.Remove(id);
            }
        }
    }
}