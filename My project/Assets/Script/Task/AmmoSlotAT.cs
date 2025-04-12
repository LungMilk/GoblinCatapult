using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmmoSlotAT : ActionTask
{
    //ammo slot will, if a gobo is within a short range
    private bool isOccupied = false;
    public BBParameter<GameObject> projectile;
    public Transform slot;
    public LayerMask projectileLayer;
    private float sampleRadius =1;
    //it will do a short check of anything within a short sample range of the slot, change the target (gobo) to in catapult = tru
    //then move its position to be in the catapult slot.
    protected override string OnInit()
    {
        return null;
    }
    protected override void OnExecute()
    {
       if (isOccupied) { EndAction(true); return; }

        Collider[] potentialProjectiles =
            Physics.OverlapSphere(slot.position, sampleRadius,projectileLayer);
        if (potentialProjectiles.Length > 0)
        {
            projectile.value = potentialProjectiles[0].gameObject;
            //we need some way to assign the goblin InCatapult to true
            Blackboard projectileBlackboard =
                projectile.value.GetComponent<Blackboard>();
            if (projectileBlackboard != null)
            {
                projectileBlackboard.SetValue("InCatapult", true);
            }
            potentialProjectiles[0].gameObject.transform.SetParent(slot.transform, false);
        }
        EndAction(true);
    }
}
