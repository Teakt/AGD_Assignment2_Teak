using UnityEngine;
using System.Collections;

public class Arrive : MonoBehaviour
{
    Rigidbody rb; 
    public GameObject target;
    public float speed = 20.0f;
    public float nearSpeed = 10.0f;
    public float nearRadius = 20.0f;
    public float arrivalRadius = 10.0f;
    float distanceFromTarget;

    Vector3 tempVect;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        distanceFromTarget = (target.transform.position - transform.position).magnitude;
        if (distanceFromTarget > nearRadius)
        {
            Debug.Log("Outside Near Radius " + distanceFromTarget);
            Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed)); 
            rb.velocity = ((target.transform.position - transform.position).normalized * speed);
        }
        else if ((target.transform.position - transform.position).magnitude > arrivalRadius)
        {
            Debug.Log("Inside Near Radius " + distanceFromTarget);
            Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed));
            rb.velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
        }
        else
        {
            Debug.Log("Inside Arrive Radius " + distanceFromTarget);
            Debug.Log("Velocity " + ((target.transform.position - transform.position).normalized * nearSpeed));
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        
    }
}
