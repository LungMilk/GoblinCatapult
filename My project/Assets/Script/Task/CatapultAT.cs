using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static NodeCanvas.Tasks.Actions.GraphOwnerControl;
using static UnityEngine.UISystemProfilerApi;

public class CatapultAT : ActionTask
{
    //target will switch to player
    public BBParameter<Transform> target;
    public Transform catapultSlot;
    public float launchHeight = 10f;
    public float speed = 40f;
    public float launchAngle = 45f;
    public BBParameter<bool> isLoaded;

    public BBParameter<GameObject> projectile;
    private Rigidbody rb;
    //public float speed;
    public Transform control;

    private float sampleTime = 0;
    protected override void OnExecute()
    {
        //rb = projectile.value.GetComponentInChildren<Rigidbody>();
        //LaunchProjectile();
        StartCoroutine(projectileMotion());
    }
    void LaunchProjectile()
    {
        //projectile.value.transform.SetParent(null,false);
        isLoaded.value = false;
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
    public Vector3 evaluate(float t)
    {
        Vector3 ac = Vector3.Lerp(catapultSlot.position, control.position, t);
        Vector3 cb = Vector3.Lerp(control.position, target.value.position, t);
        return Vector3.Lerp(ac, cb, t);

    }
    IEnumerator projectileMotion()
    {
        Debug.Log("projectile should be moving");
        sampleTime = 0f;

        while (sampleTime <= 1f)
        {
            projectile.value.transform.position = evaluate(sampleTime);
            projectile.value.transform.forward = evaluate(sampleTime + 0.001f) - projectile.value.transform.position;

            sampleTime += Time.deltaTime * (speed / 10f);
            yield return null;
        }

        Debug.Log("Projectile reached end of curve.");
        //reestablish movement at the end of the thingy
        Blackboard projectileBlackboard =
                projectile.value.GetComponent<Blackboard>();
        if (projectileBlackboard != null)
        {
            projectileBlackboard.SetValue("InCatapult", false);
        }
    }
   
}
