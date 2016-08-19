using UnityEngine;

namespace Yoshapihoff
{
    namespace Libs
    {
        public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
        {
            private static T _Inst;

            public static T Inst
            {
                get
                {
                    if (_Inst == null)
                    {
                        _Inst = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            return _Inst;
                        }

                        if (_Inst == null)
                        {
                            GameObject singleton = new GameObject();
                            _Inst = singleton.AddComponent<T>();

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return _Inst;
                }
            }

            void Awake()
            {
                this.name = "(singleton) " + typeof(T).ToString();
            }
        }
    }
}