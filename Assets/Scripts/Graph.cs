using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{


    [SerializeField] private MeshRenderer Circle;
    public Plane graphContainer;
    public GameObject root_node;

    public float ViewRadius = 50f;
    


    Collider[] list_points;

    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 8;



    private void Awake()
    {
        graphContainer = GetComponent<Plane>();
        root_node = GetComponent<GameObject>();

        int i = 0;
        list_points = ZoneDetectSphere(transform.position, ViewRadius);
        while (i < list_points.Length)
        {
            Debug.Log(list_points[i].transform.position);
            i++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(list_points[i].transform.position).normalized * hit.distance, Color.black);
        
    }

    // Update is called once per frame
    void Update()
    {



        

    }

    Collider[] ZoneDetectSphere(Vector3 center, float radius)
    {


        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;


        return hitColliders;
    }
}
