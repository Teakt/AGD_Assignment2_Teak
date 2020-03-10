using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TagSystem : MonoBehaviour
{

    public GameObject[] players;
    GameObject it; 
    // Start is called before the first frame update
    void Start()
    {
        it = players[Random.Range(0, 2)];
        Debug.Log(it.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
