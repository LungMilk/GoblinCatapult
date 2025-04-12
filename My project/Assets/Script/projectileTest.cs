using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTest : MonoBehaviour
{
    public QuadraticCurve curve;
    public float speed;

    private float sampleTime;
    // Start is called before the first frame update
    void Start()
    {
        sampleTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //what this means for the goblin is that its transform will follow this line and b will be set to target position
        sampleTime += Time.deltaTime * speed;
        transform.position = curve.evaluate(sampleTime);
        transform.forward = curve.evaluate(sampleTime +0.001f) - transform.position;

        if (sampleTime >= 1f)
        {
            //Debug.Log("projectile reacehd end of sample time.");
        }
    }
}
