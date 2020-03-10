using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArriveA : MonoBehaviour
{

    public GameObject target;
    
    // Start is called before the first frame update
    public float m_maxVelocity = 10.0f;
    float lowVelocity ;

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
            if (System.Math.Round(transform.rotation.y, 2) != System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y, 2))
            {
                // The step size is equal to speed times frame time.
                float singleStep = lowVelocity * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                Debug.DrawRay(transform.position, newDirection, Color.blue);

                transform.rotation = Quaternion.LookRotation(newDirection);
               //Debug.Log("Rota y " + System.Math.Round(transform.rotation.y, 2) + " and " + System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y, 2));
            }
            else
            {
               //Debug.Log("Outside Near Radius " + distanceFromTarget + " Velocity " + _velocity);
                transform.position += transform.forward * Time.deltaTime * m_maxVelocity;
            }



        }
        else if ((target.transform.position - transform.position).magnitude > arrivalRadius) // Close to target CHECK A.i.
        {
            //Debug.Log("Inside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            transform.position += targetDirection * Time.deltaTime * lowVelocity;
        }
        else
        {
            //Debug.Log("Inside Arrive Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);

            transform.position += transform.forward * Time.deltaTime * 0;
           
        }
    }
}
