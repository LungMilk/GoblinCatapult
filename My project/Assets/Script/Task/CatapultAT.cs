using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultAT : ActionTask
{
    //target will switch to player
    public BBParameter<Transform> target;
    public float launchHeight = 10f;
    public float speed = 10f;
    public float launchAngle = 45f;

    public BBParameter<GameObject> projectile;
    private Rigidbody rb;
    protected override void OnExecute()
    {
        rb = projectile.value.GetComponent<Rigidbody>();
        LaunchProjectile();
    }
    void LaunchProjectile()
    {
        projectile.value.transform.SetParent(null,false);
        //get the direction of where we are aiming the little guy
        Vector3 directionToTarget = target.value.position - agent.transform.position;
        //convert angle to rads
        float angleInRadians = launchAngle * Mathf.Deg2Rad;
        //to get its burst of upwards velocity, using trig we multiply the angle and speed to get a value ratio value of its speed
        float verticalVelocity = Mathf.Sin(angleInRadians) * speed;

        //calculating the horizontal velocity allows for the operation to use trig and project motion
        Vector3 horizontalDirection = directionToTarget;
        //setting horiz to y gives us the horiz plane value
        horizontalDirection.y = 0;
        float horizontalSpeed = speed * Mathf.Cos(angleInRadians);
        Vector3 horizontalVelocity = horizontalDirection.normalized * horizontalSpeed;

        Vector3 launchVelocity = horizontalVelocity;
        launchVelocity.y = verticalVelocity;

        //set velocity of the rb to the launch burst
        //this might be removed as a nav agent takes control
        rb.velocity = launchVelocity;
    }

}
