using UnityEngine;
using System.Collections;

// accepts input and animates the player
public class CheckToroidal : MonoBehaviour
{


    public GameObject floor;
   

    

    GameObject character;

    float rightBorder;
    float leftBorder;
    float topBorder;
    float botBorder;


    void Start()
    {

        Collider col = floor.GetComponent<BoxCollider>();
        //Vector3[] vertices = mesh.vertices;
        //Vector2[] uvs = new Vector2[vertices.Length];
        Bounds bounds = col.bounds;

     
        character = this.GetComponent<GameObject>();




        rightBorder = bounds.size.x / 2;
        leftBorder = (-bounds.size.x) / 2;
        topBorder = bounds.size.z / 2;
        botBorder = (-bounds.size.z) / 2;
        //Debug.Log("" + rightBorder);
        //Debug.Log("" + bounds.size.x * floor.transform.localScale.x);
        //Debug.Log("Vertical" + topBorder);
        //Debug.Log("" + bounds.size.z * floor.transform.localScale.z);
    }

    void Update()
    {
      
       
        Check(); // TOROIDAL CHECKED

       


    }

    void Check()
    {

        Vector3 perso = this.transform.position; //we simplify the use of tranform for our character
        Vector3 floorLim = floor.transform.position; // same for our floor


        //Debug.Log("Character x : " + perso.x);
        //Debug.Log("Character z: " + perso.z);
        if (perso.x > rightBorder) // when the character goes too far on the right
        {
            this.transform.position = new Vector3(leftBorder, 0, perso.z);
        }
        else if (perso.x < leftBorder)  // when the character goes too far on the left
        {
            this.transform.position = new Vector3(rightBorder, 0, perso.z);
        }
        else if (perso.z < botBorder)  // when the character goes too far on the botside
        {
            this.transform.position = new Vector3(perso.x, 0, topBorder);
        }
        else if (perso.z > topBorder)  // when the character goes too far on the botside
        {
            this.transform.position = new Vector3(perso.x, 0, botBorder);
        }
    }
}
