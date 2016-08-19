using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yoshapihoff.Libs;

public class Player_TargetMove : MonoBehaviour
{
    public enum MoveType
    {
        ToTarget,
        AwayFromTarget
    }

    public MoveType Move;

    private Rigidbody Rigid;
    private GameObject MyGO;
    private Transform MyTransform;
    private List<GameObject> Targets = new List<GameObject>();
    private GameObject MyTarget;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        MyGO = this.gameObject;
        MyTransform = this.transform;
    }

    void OnCreate(MonoBehaviour sender, MonoBehaviour reciever, object arg)
    {
        if (sender.gameObject.GetInstanceID() != MyGO.GetInstanceID())
        {
            if (!Targets.Contains(sender.gameObject))
            {
                Targets.Add(sender.gameObject);
            }
            MyTarget = Targets[Random.Range(0, Targets.Count)];
        }
    }

    IEnumerator Start()
    {
        EventManager.Inst.Subscribe((int)Events.Create, OnCreate);
        yield return null;
        EventManager.Inst.PostNotification((int)Events.Create, this);
    }

    void Update()
    {
        if (MyTarget)
        {
            Vector3 MoveVector = Vector3.zero;
            switch (Move)
            {
                case MoveType.ToTarget:
                    MoveVector = MyTarget.transform.position - MyTransform.position;
                    break;
                case MoveType.AwayFromTarget:
                    MoveVector = MyTransform.position - MyTarget.transform.position;
                    break;
            }
            Rigid.velocity = MoveVector.normalized;
        }
    }
}
