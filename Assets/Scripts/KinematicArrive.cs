using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{

    public float ViewRadius = 30f;
    [Range(10,360)]
    public float viewAngle ;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    
    



    public GameObject target;
    Transform target_transform;
    // Start is called before the first frame update
    public float m_maxVelocity = 10.0f;

    //acceleration in unity units per second
    public float m_acceleration = 2.0f;

    //deccelaration speed with respect to acceleration speed
    public float m_deccelerationMultiplier;

    //rotation speed
    public float rotationDegreesPerSecond = 360;

    // the current velocity
    float _velocity;

    //public float slow_speed ; //We set to 10 , the user sets it, below this speed : A. of our exercice
    //public float small_distance; // We set the "small distance" from the target : A.i of our exercice 

    public float speed = 10.0f;
    //public float nearSpeed = 10.0f;
    public float nearRadius = 5.0f;
    public float arrivalRadius = 1.0f;
    float distanceFromTarget;


    float speed_Limit; // When above this then A , below then B from the assignement

    void Start()
    {
        target_transform = target.transform;
        speed_Limit = m_maxVelocity / 4f;
        //StartCoroutine("FindTargetWithDelay", .3f);
        
    }

    // Update is called once per frame
    void Update()
    {

        

        // Determine which direction to rotate towards
        Vector3 targetDirection = target_transform.position - transform.position;

        

        Debug.Log("angle par arapport a la vitesse" + viewAngle * _velocity / 100);

        /*
        
        distanceFromTarget = (target.transform.position - transform.position).magnitude; 
        if (distanceFromTarget > nearRadius) // Far from target A.ii
        {
            

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            Debug.DrawRay(transform.position, newDirection, Color.blue);

            transform.rotation = Quaternion.LookRotation(newDirection);

            Debug.Log("Outside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            //Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed));
            //rb.velocity = ((target.transform.position - transform.position).normalized * speed);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if ((target.transform.position - transform.position).magnitude > arrivalRadius) // Close to target CHECK A.i.
        {
            Debug.Log("Inside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            //Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed));
            //rb.velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
            transform.position += targetDirection * Time.deltaTime * nearSpeed;
        }
        else
        {
            Debug.Log("Inside Arrive Radius " + distanceFromTarget+ " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
            //Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed));
            //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position += transform.forward * Time.deltaTime * 0;
        }
        */
        distanceFromTarget = (target.transform.position - transform.position).magnitude;

        


        //if (_velocity < speed_Limit  ) // A
        if (_velocity > m_maxVelocity)
        {
            

            Debug.Log("Velocity" + _velocity);  
            _velocity += m_acceleration;
            if (_velocity > m_maxVelocity)
            {
                _velocity = m_maxVelocity;
            }
                

            
            if (distanceFromTarget > nearRadius) // Far from target A.ii
            {
                if(System.Math.Round(transform.rotation.y,2) != System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y,2))
                {
                    // The step size is equal to speed times frame time.
                    float singleStep = _velocity * Time.deltaTime;

                    // Rotate the forward vector towards the target direction by one step
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                    Debug.DrawRay(transform.position, newDirection, Color.blue);

                    transform.rotation = Quaternion.LookRotation(newDirection);
                    Debug.Log("Rota y " + System.Math.Round(transform.rotation.y,2) + " and " + System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y,2));
                }
                else
                {
                    Debug.Log("Outside Near Radius " + distanceFromTarget + " Velocity " + speed);
                    transform.position += transform.forward * Time.deltaTime * m_maxVelocity;
                }
                

                
            }
            else if ((target.transform.position - transform.position).magnitude > arrivalRadius) // Close to target CHECK A.i.
            {
                Debug.Log("Inside Near Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
                transform.position += targetDirection * Time.deltaTime * _velocity;
            }
            else
            {
                Debug.Log("Inside Arrive Radius " + distanceFromTarget + " Velocity " + this.GetComponent<Rigidbody>().velocity.normalized.magnitude);
                
                transform.position += transform.forward * Time.deltaTime * 0;
                _velocity = 0;
            }
        }
        else // B 
        {

            //viewAngle = viewAngle * _velocity / 100 + 10;

            Vector3 viewAngleA = this.DirFromAngle(-this.viewAngle / 2, false);
            Vector3 viewAngleB = this.DirFromAngle(this.viewAngle / 2, false);

            Debug.DrawRay(this.transform.position,  targetDirection + viewAngleA * this.ViewRadius, Color.magenta);
            Debug.DrawRay(this.transform.position, targetDirection + viewAngleB  * this.ViewRadius, Color.magenta);

            /*
            Debug.Log("Velocity B" + _velocity);
            _velocity += m_acceleration;
            if (_velocity > m_maxVelocity)
            {
                _velocity = m_maxVelocity;
            }
            */

            Debug.Log("BOUGE PLUS" + _velocity);
            _velocity = speed_Limit - 1 ;
            Transform target_temp = target.transform;
            Vector3 dirToTarget = (target_temp.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) // Inside of viewAgnle
            {
                float distFromTarget = Vector3.Distance(transform.position, target_temp.position);
                Debug.Log("INSIDE of VIewAngle");
               
                    // The step size is equal to speed times frame time.
                    float singleStep = _velocity * Time.deltaTime;

                    // Rotate the forward vector towards the target direction by one step
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                    Debug.DrawRay(transform.position, newDirection, Color.black);

                    transform.rotation = Quaternion.LookRotation(newDirection);
                    //Debug.Log("Rota y " + System.Math.Round(transform.rotation.y,2) + " and " + System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y,2));
               
                    //Debug.Log("Outside Near Radius " + distanceFromTarget + " Velocity " + speed);
                    transform.position += transform.forward * Time.deltaTime * _velocity;
             

            }
            else // Outside of viewAngle
            {
                Debug.Log("Outside of VIewAngle");
                if (System.Math.Round(transform.rotation.y, 2) != System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y, 2))
                {
                    // The step size is equal to speed times frame time.
                    float singleStep = _velocity * Time.deltaTime;

                    // Rotate the forward vector towards the target direction by one step
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                    Debug.DrawRay(transform.position, newDirection, Color.black);

                    transform.rotation = Quaternion.LookRotation(newDirection);
                    //Debug.Log("Rota y " + System.Math.Round(transform.rotation.y,2) + " and " + System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y,2));
                }
                else
                {
                    //Debug.Log("Outside Near Radius " + distanceFromTarget + " Velocity " + speed);
                    transform.position += transform.forward * Time.deltaTime * _velocity;
                }

            }
        }

    }


    public Vector3 DirFromAngle(float angleInDegrees , bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y; 
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)); 
    }

    /*
    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) // Inside of viewAgnle
            {
                float distFromTarget = Vector3.Distance(transform.position, target.position);
                Debug.Log("INSIDE of VIewAngle");
                if (!Physics.Raycast(transform.position, dirToTarget, distFromTarget, obstacleMask)) // Add all targets to list
                {
                    
                    visibleTargets.Add(target);
                }
               
            }
            else // Outside of viewAngle
            {
                Debug.Log("Outside of VIewAngle");
            }
        }

    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    */
}
