using NodeCanvas.Framework;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowSteerAT : ActionTask
{
    [SerializeField] private Seeker seekAI;
    public BBParameter<Transform> target;
    public float stoppingDistance;
    public BBParameter<Vector3> accel;
    public float steeringAccel;

    protected override string OnInit()
    {
        seekAI = agent.GetComponent<Seeker>();
        return null;
    }
    protected override void OnExecute()
    {
        //SetPath();

        float distanceToTarget = Vector3.Distance(agent.transform.position, target.value.position);
        if (distanceToTarget < stoppingDistance)
        {
            EndAction(true);
            return;
        }
        Vector3 direction = target.value.position - agent.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        accel.value += direction.normalized * steeringAccel * Time.deltaTime;
        EndAction(true);
    }
    public void SetPath()
    {
        seekAI.StartPath(agent.transform.position,target.value.position);
    }

}
