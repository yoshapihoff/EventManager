using UnityEngine;
using System.Collections;
using Yoshapihoff.Libs;

public class Test : MonoBehaviour
{
    void Awake()
    {
        EventManager.Inst.Subscribe(0,
            () => Debug.Log(this.name + ": Какой-то объект только что был создан"));
    }

    void Start()
    {
        EventManager.Inst.PostNotification(0);
    }
}
