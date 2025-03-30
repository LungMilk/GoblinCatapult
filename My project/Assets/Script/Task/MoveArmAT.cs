using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class MoveArmAT : ActionTask
{
    public float lerpSpeed = 1.0f;  
    private Quaternion currentRotation;
    public GameObject catapultArm;

    protected override string OnInit()
    {
        currentRotation = catapultArm.transform.rotation;
        return null;
    }
    protected override void OnUpdate()
    {
        //neglected to look and the behavoir tree to see why it was wrong
        //currentRotation = catapultArm.transform.rotation;
        //Quaternion changingRot;
        //changingRot.x = currentRotation.x + 1 * Time.deltaTime;
        //catapultArm.transform.rotation = Quaternion.Euler(changingRot.x, currentRotation.y, currentRotation.z);
        // Interpolate the current rotation toward the target rotation
        currentRotation.x = Mathf.Lerp(currentRotation.x, -45f, lerpSpeed * Time.deltaTime);

        // Apply the new rotation
        catapultArm.transform.rotation = Quaternion.Euler(currentRotation.x, catapultArm.transform.rotation.eulerAngles.y, catapultArm.transform.rotation.eulerAngles.z);
    }
}
