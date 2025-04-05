using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateAT : ActionTask
{
    public BBParameter<Vector3> velocity;
    public BBParameter<Vector3> accel;
    public BBParameter<float> maxGroundSpeed;

    protected override void OnUpdate()
    {
        velocity.value += accel.value;
        Vector3 vel = velocity.value;
        float groundSpeed = Mathf.Sqrt(vel.x * vel.x + vel.z * vel.z);
        if (groundSpeed > maxGroundSpeed.value)
        {
            float cappedX = vel.x / groundSpeed * maxGroundSpeed.value;
            float cappedZ = vel.z / groundSpeed * maxGroundSpeed.value;

            velocity.value = new Vector3(cappedX, vel.y, cappedZ);
        }

        agent.transform.position += velocity.value * Time.deltaTime;
        accel.value = Vector3.zero;
    }

}
