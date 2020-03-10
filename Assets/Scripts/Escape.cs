using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;

    public GameObject target;

    // Start is called before the first frame update
    public float m_maxVelocity = 10.0f;
    float lowVelocity;

    //acceleration in unity units per second
    public float m_acceleration = 2.0f;


    //rotation speed
    public float rotationDegreesPerSecond = 360;

    // the current velocity
    float _velocity;

    public float nearRadius = 3.0f;
    public float arrivalRadius = 1.0f;
    float distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        lowVelocity = m_maxVelocity / 5;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.transform.position - transform.position;


        distanceFromTarget = (target.transform.position - transform.position).magnitude;


        if (distanceFromTarget > nearRadius) // Far from target A.ii
        {
            //Debug.Log("Inside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            transform.position += -(targetDirection)  * Time.deltaTime * m_maxVelocity;


        }
        else if ((target.transform.position - transform.position).magnitude > arrivalRadius) // Close to target CHECK A.i.
        {
            //Debug.Log("Inside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            transform.position += -(targetDirection) + new Vector3(5,0,0) * Time.deltaTime * m_maxVelocity;
        }
        else
        {
            

        }
    }
}
