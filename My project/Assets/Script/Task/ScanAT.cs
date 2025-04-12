using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ScanAT : ActionTask
{
    //scan will be for the goblin
    //it will check for the closest player over a distance
    //if none are found the goblin sets its target to the nearest catapult point
    //
    public BBParameter<Transform> target;
    public BBParameter<LayerMask> playerLayer;
    public BBParameter<LayerMask> catapultSlotLayer;
    public BBParameter<float> sampleRadius;
    protected override string OnInit()
    {
        return null;
    }
    protected override void OnExecute()
    {
        //having the if statement be the colider check is probs better
        Collider[] playerColliders =
        Physics.OverlapSphere(agent.transform.position, sampleRadius.value, playerLayer.value);
        if (playerColliders.Length > 0 && target.value != playerColliders[0].gameObject.transform)
        {
            //Debug.Log(playerColliders[0].name);
            target.value = playerColliders[0].gameObject.transform;
        }
        else
        {
            Collider[] catapultSlotColliders=
            Physics.OverlapSphere(agent.transform.position, sampleRadius.value, catapultSlotLayer.value);
            if (catapultSlotColliders.Length > 0)
            {
                //Debug.Log(catapultSlotColliders[0].name);
                target.value = catapultSlotColliders[0].gameObject.transform;
            }
            else { //Debug.Log("could not find target");
                   }
        }
        EndAction(true);
        //we need to perrform a scan for certain items
    }
}
