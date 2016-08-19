using UnityEngine;
using System.Collections.Generic;

namespace Yoshapihoff
{
    namespace Libs
    {
        public class EventManager : Singleton<EventManager>
        {
            public delegate void Action(MonoBehaviour sender, MonoBehaviour reciever, object arg);

            public bool DebugMode = true;

            private Dictionary<int, List<Action>> EventActions = new Dictionary<int, List<Action>>();

            /// <summary>
            /// Подписаться на событие
            /// </summary>
            /// <param name="eventType">Событие</param>
            /// <param name="action">Действие-обработчик события</param>
            /// <returns></returns>
            public bool Subscribe(int eventType, Action action)
            {
                List<Action> actions;
                if (EventActions.TryGetValue(eventType, out actions))
                {
                    if (actions.Contains(action))
                    {
                        Debug.LogError("The handler is already subscribed to the event");
                        return false;
                    }
                    actions.Add(action);
                    if (DebugMode)
                    {
                        Debug.Log(action + " subscribed to event:  \"" + eventType + "\"");
                    }
                    return true;
                }

                actions = new List<Action>();
                actions.Add(action);
                EventActions.Add(eventType, actions);
                return true;
            }

            /// <summary>
            /// Отписаться от события
            /// </summary>
            /// <param name="eventType">Событие</param>
            /// <param name="action">Действие-обработчик события</param>
            /// <returns></returns>
            public bool Unsubscribe(int eventType, Action action)
            {
                List<Action> actions;
                if (EventActions.TryGetValue(eventType, out actions))
                {
                    if (actions.Contains(action))
                    {
                        actions.Remove(action);
                        if (DebugMode)
                        {
                            Debug.Log(action + " unsubscribed from event: \"" + eventType + "\"");
                        }
                        return true;
                    } 
                    Debug.LogError("This handler is not subscribed to this event");
                    return false;
                }
                Debug.LogError("This event has no handlers");
                return false;
            }

            /// <summary>
            /// Оповестить всех подписчиков о событии
            /// </summary>
            /// <param name="eventType">Событие</param>
            public void PostNotification(int eventType, MonoBehaviour sender, object arg = null, MonoBehaviour reciever = null)
            {
                List<Action> actions;
                if (EventActions.TryGetValue(eventType, out actions))
                {
                    for (int i = 0; i < actions.Count; ++i)
                    {
                        actions[i].Invoke(sender, reciever, arg);
                        if (DebugMode)
                        {
                            Debug.Log("Event: \"" + eventType + "\" is happened");
                        }
                    }
                }
            }

            void UnsubscribeAllDestroyedSubscribers()
            {
                foreach (var actions in EventActions.Values)
                {
                    for (int i = actions.Count - 1; i >= 0; --i)
                    {
                        var action = actions[i];
                        if (action.Equals(null))
                        {
                            actions.RemoveAt(i);
                        }
                    }
                }
            }

            void OnLevelWasLoaded()
            {
                UnsubscribeAllDestroyedSubscribers();
            }
        }
    }
}
/*
TODO: нужны события адресованные кому-то конкретному, а это значит,
что нужно в подписчики, помимо обработчика передавать того, кто обрабатывает эот событие.
*/