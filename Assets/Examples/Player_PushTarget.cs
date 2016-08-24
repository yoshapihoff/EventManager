using UnityEngine;
using System.Collections;
using Yoshapihoff.Libs;

public class Player_PushTarget : MonoBehaviour
{
    private Rigidbody MyRigid;
    private GameObject MyGO;

    void OnPush (MonoBehaviour sender, MonoBehaviour reciever, object arg)
    {
        if ( MyGO.GetInstanceID () == reciever.gameObject.GetInstanceID () )
        {
            EventManager.Inst.PostNotification ((int)Events.FindNewTargetAndChangeMoveType, this);
        }
    }

    void Awake ()
    {
        MyGO = this.gameObject;
        MyRigid = GetComponent<Rigidbody> ();
    }

    void OnCollisionEnter (Collision other)
    {
        if ( other.gameObject.tag == "Player" )
        {
            var reciever = other.gameObject.GetComponent<Player_PushTarget> ();
            EventManager.Inst.PostNotification ((int)Events.Push, this, null, reciever);
        }
    }

    void Start ()
    {
        EventManager.Inst.Subscribe ((int)Events.Push, OnPush);
    }
}
