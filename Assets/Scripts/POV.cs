using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV : MonoBehaviour
{

    public float ViewRadius = 50f;
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



        int i = 0;


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        while (i < list_points.Length)
        {
            Vector3 dirToNextPoint = transform.position - list_points[i].transform.position;
            float distToNextPoint = Vector3.Distance(transform.position, list_points[i].transform.position);


            if (Physics.Raycast(transform.position, transform.TransformDirection(list_points[i].transform.position), out hit, distToNextPoint))
            {
                
                if (hit.collider.CompareTag("Point"))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(list_points[i].transform.position).normalized * hit.distance, Color.black);

                }
                else
                {
                   
                }
               
            }
            
            

            i++;
        }
        
    }

    Collider[] ZoneDetectSphere(Vector3 center, float radius)
    {
        

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;
        

        return hitColliders;
    }
}
