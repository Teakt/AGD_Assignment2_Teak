using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{


    [SerializeField] private MeshRenderer Circle;


    List<GameObject> nodes_gameobj = new List<GameObject>();
    List<Node> nodes = new List<Node>();


    public class Node
    {
        public Transform transform;
        
        public int weight;
        public List<Node> neighbours = new List<Node>();
        public Dictionary<Transform, float> distances_to_neigh = new Dictionary<Transform, float>();

        public Node(Transform transform, int weight)
        {
            this.transform = transform;
            this.weight = weight; 
          
        }

        public void AddNeighbour(Node node)
        {
            neighbours.Add(node);
        }

        public void DrawConnections()
        {
            if(neighbours!= null)
            {
                foreach (Node neigh in neighbours)
                {
                    Debug.DrawLine(this.transform.position, neigh.transform.position, Color.black, 100.0f);
                    distances_to_neigh.Add(neigh.transform, Vector3.Distance(neigh.transform.position , this.transform.position));
                   
                }
            }
            
        }

       
        
    }


    private void Awake()
    {
       foreach(Transform node in transform)
        {
            nodes_gameobj.Add(node.gameObject);
            Node new_node = new Node(node, Random.Range(15, 30));
            nodes.Add(new_node);
        }
        Debug.Log(nodes.Count);

        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        int layerMask = 1 << 9;
        foreach (Node node in nodes)
        {
            foreach (Node other_node in nodes)
            {
                Vector3 dirToNextPoint = other_node.transform.position - node.transform.position;
                float distToNextPoint = Vector3.Distance(node.transform.position, other_node.transform.position);
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(node.transform.position, dirToNextPoint, out hit, distToNextPoint, layerMask))
                {   
                    if(hit.collider == null)
                    {
                        
                        Debug.Log(hit);

                    }
                    else
                    {
                        Debug.Log("Did not Hit 2");
                    }
                    
                }
                else
                {
                    Debug.DrawRay(node.transform.position, dirToNextPoint.normalized * distToNextPoint, Color.yellow, 3.0f);
                    node.AddNeighbour(other_node);
                    Debug.Log("Did not Hit");
                }
            }
        }

        foreach(Node node in nodes)
        {
            node.DrawConnections();
            Debug.Log(node.weight + "" +  node.transform.position );
            foreach(KeyValuePair<Transform, float> trans in node.distances_to_neigh)
            {
                Debug.Log(trans.Key + "" + trans.Value);
            }
        }



    }

    // Update is called once per frame
    void Update()
    {


       


    }

    
}
