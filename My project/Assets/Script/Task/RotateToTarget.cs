using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using Pathfinding.Examples;

public class RotateToTarget : ActionTask
{
    public Transform transformToRot;
    public Transform target;
    public float turnSpeed;

    protected override string OnInit()
    {
        transformToRot = agent.GetComponent<Transform>();
        return null;
    }
    protected override void OnUpdate()
    {
        Quaternion lookRotation =Quaternion.LookRotation((target.position - transformToRot.position).normalized);
        lookRotation.x = 0;
        lookRotation.z = 0;
        //over time
        transformToRot.rotation = Quaternion.Slerp(transformToRot.rotation, lookRotation, Time.deltaTime * turnSpeed);
        EndAction(true);
    }


}



