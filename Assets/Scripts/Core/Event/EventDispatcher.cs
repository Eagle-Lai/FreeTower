/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System;
using System.Collections.Generic;

namespace FTProject
{
    /// <summary>
    /// ����ע��ʹ�������0~4����������Event�ص���
    /// ��Ϸ�¼�����ȫ����EventDispatcher�С�
    /// </summary>
    public class EventDispatcher
    {
        private static EventController m_eventController = new EventController();

        //ע��������Ͳ�ͬ����Ϣ����;
        public static void AddEventListener<T>(string eventType, Action<T> handler)
        {
            m_eventController.AddEventListener<T>(eventType, handler);
        }

        public static void AddEventListener(string eventType, Action handler)
        {
            m_eventController.AddEventListener(eventType, handler);
        }

        public static void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            m_eventController.AddEventListener<T, U>(eventType, handler);
        }

        public static void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            m_eventController.AddEventListener<T, U, V>(eventType, handler);
        }

        public static void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            m_eventController.AddEventListener<T, U, V, W>(eventType, handler);
        }

        public static void Cleanup()
        {
            m_eventController.Cleanup();
        }

        //���������ʱ�����ɵ���event����;
        public static void MarkAsPermanent(string eventType)
        {
            m_eventController.MarkAsPermanent(eventType);
        }

        public static void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            m_eventController.RemoveEventListener<T>(eventType, handler);
        }

        public static void RemoveEventListener(string eventType, Action handler)
        {
            m_eventController.RemoveEventListener(eventType, handler);
        }

        public static void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            m_eventController.RemoveEventListener<T, U>(eventType, handler);
        }

        public static void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            m_eventController.RemoveEventListener<T, U, V>(eventType, handler);
        }

        public static void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            m_eventController.RemoveEventListener<T, U, V, W>(eventType, handler);
        }

        public static void TriggerEvent(string eventType)
        {
            m_eventController.TriggerEvent(eventType);
        }

        public static void TriggerEvent<T>(string eventType, T arg1)
        {
            m_eventController.TriggerEvent<T>(eventType, arg1);
        }

        public static void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            m_eventController.TriggerEvent<T, U>(eventType, arg1, arg2);
        }

        public static void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            m_eventController.TriggerEvent<T, U, V>(eventType, arg1, arg2, arg3);
        }

        public static void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            m_eventController.TriggerEvent<T, U, V, W>(eventType, arg1, arg2, arg3, arg4);
        }

        public static Dictionary<string, Delegate> TheRouter
        {
            get
            {
                return m_eventController.TheRouter;
            }
        }
    }
}