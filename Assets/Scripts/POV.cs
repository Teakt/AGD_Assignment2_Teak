using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV : MonoBehaviour
{

    public float ViewRadius = 30f;
    [Range(10, 360)]
    

    Collider[] list_points;

    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 8;

    // This would cast rays only against colliders in layer 8.
    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
    


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        list_points = ZoneDetectSphere(transform.position, ViewRadius);
        while (i < list_points.Length){
            Debug.Log(list_points[i].transform.position);
            i++;
        }

        
    }

    // Update is called once per frame
    void Update()
    {



        


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    Collider[] ZoneDetectSphere(Vector3 center, float radius)
    {
        

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;
        while (i < hitColliders.Length )
        {
            
            
            i++;
        }

        return hitColliders;
    }
}
