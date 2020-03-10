using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArriveB : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;


    public float ViewRadius = 30f;
    [Range(10, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();






    public GameObject target;
    Transform target_transform;
    // Start is called before the first frame update
    public float m_maxVelocity = 10.0f;

    //acceleration in unity units per second
    public float m_acceleration = 0.5f;

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
     
        speed_Limit = m_maxVelocity / 4f;
     

    }

    // Update is called once per frame
    void Update()
    {
        /*


        // Determine which direction to rotate towards
        Vector3 targetDirection = target.transform.position - transform.position;



        Debug.Log("angle par arapport a la vitesse" + viewAngle * _velocity / 100);

       


            Debug.Log("Velocity" + _velocity);
          
            
      

            viewAngle = (viewAngle * _velocity) / 100 + 10;

            Vector3 viewAngleA = this.DirFromAngle(-this.viewAngle / 2, false);
            Vector3 viewAngleB = this.DirFromAngle(this.viewAngle / 2, false);

            Debug.DrawRay(this.transform.position, targetDirection + viewAngleA * this.ViewRadius, Color.magenta);
            Debug.DrawRay(this.transform.position, targetDirection + viewAngleB * this.ViewRadius, Color.magenta);

            
            _velocity += m_acceleration;
            if (_velocity > m_maxVelocity)
            {
                _velocity = m_maxVelocity;
            }
        



            Transform target_temp = target.transform;
            Vector3 dirToTarget = (target_temp.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) // Inside of viewAgnle
            {
                float distFromTarget = Vector3.Distance(transform.position, target_temp.position);
                Debug.Log("INSIDE of VIewAngle");

                // The step size is equal to speed times frame time.
                float singleStep = m_maxVelocity * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                Debug.DrawRay(transform.position, newDirection, Color.black);

                transform.rotation = Quaternion.LookRotation(newDirection);
              
                transform.position += transform.forward * Time.deltaTime * m_maxVelocity;


            }
            else // Outside of viewAngle
            {
                        Debug.Log("Outside of VIewAngle");
                    if (System.Math.Round(transform.rotation.y, 2) != System.Math.Round(Quaternion.LookRotation(target.transform.position - transform.position).y, 2))
                    {
                        // The step size is equal to speed times frame time.
                        float singleStep = _velocity/4 * Time.deltaTime;

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

            */

        agent.SetDestination(target.transform.position);


    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
