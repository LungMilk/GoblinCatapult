using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcVisualiser : ActionTask
{
    public LineRenderer lineRenderer;

    public BBParameter<Transform> target;
    public Transform catapultSlot;
    public Transform Control;
    public int linePoints;
    protected override string OnInit()
    {
        lineRenderer = agent.GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePoints;
        return null;
    }
    public Vector3 evaluate(float t)
    {
        Vector3 ac = Vector3.Lerp(catapultSlot.position, Control.position, t);
        Vector3 cb = Vector3.Lerp(Control.position, target.value.position, t);
        return Vector3.Lerp(ac, cb, t);
    }
    protected override void OnExecute()
    {
        //ArcVisualiser <- this is an object that I cannto find doc on???

        //basically I need to assign a point in the line redereres array that is at each step during the beziuer curve
        //if I use the for loop limit I can get an even disripution by dividing evaluartes parameter
        for (int i = 0;linePoints > i; i++)
        {
            lineRenderer.SetPosition(i,evaluate((float)i/linePoints));
        }
        EndAction(true);
    }

}
