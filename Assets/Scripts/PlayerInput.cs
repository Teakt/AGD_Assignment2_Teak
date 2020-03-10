using UnityEngine;
using System.Collections;

// accepts input and animates the player
public class PlayerInput : MonoBehaviour
{


    public GameObject floor; 
	//the animator component attached to this game object
	Animator _animator;

	//veloctiy upper clamp
	public float m_maxVelocity;

	//acceleration in unity units per second
	public float m_acceleration;

	//deccelaration speed with respect to acceleration speed
	public float m_deccelerationMultiplier;

	//rotation speed
	public float rotationDegreesPerSecond = 360;

	// the current velocity
	float _velocity;

	// the input vector
	Vector2 _input = Vector2.zero;

    GameObject character;

    //Collider floormesh
    float rightBorder;
    float leftBorder;
    float topBorder;
    float botBorder; 
    

    void Start ()
    {

        Collider col = floor.GetComponent<BoxCollider>();
        //Vector3[] vertices = mesh.vertices;
        //Vector2[] uvs = new Vector2[vertices.Length];
        Bounds bounds = col.bounds;

        //cache the animator
        _animator = GetComponent<Animator> ();
        character = this.GetComponent<GameObject>();
       

      
        
        rightBorder = bounds.size.x / 2  ;
        leftBorder = (-bounds.size.x) / 2 ; 
        topBorder = bounds.size.z / 2  ;
        botBorder = (-bounds.size.z) / 2  ;
        //Debug.Log("" + rightBorder);
        //Debug.Log("" + bounds.size.x * floor.transform.localScale.x);
        //Debug.Log("Vertical" + topBorder);
        //Debug.Log("" + bounds.size.z * floor.transform.localScale.z);
    }

    void Update ()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
		//cache the input
		_input.x = horizontal;
		_input.y = vertical;

		//calculate input magnitude (you may use this to assign the blend parameter in your movement
		// blend tree directly, the acceleration system in this example is given to showcase its
		// potential effect on a PC without a controller)
		float inputMag = _input.magnitude;


        // Check for inputs
		if (!Mathf.Approximately (vertical, 0.0f) || !Mathf.Approximately (horizontal, 0.0f)) {
			Vector3 direction = new Vector3 (horizontal, 0.0f, vertical);
			direction = Vector3.ClampMagnitude (direction, 1.0f);

			// increment velocity
			if (_velocity < m_maxVelocity) {
				_velocity += m_acceleration * Time.deltaTime;
				if (_velocity > m_maxVelocity)
					_velocity = m_maxVelocity;
			}

			// look towards the input direction
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (direction), rotationDegreesPerSecond * Time.deltaTime);


		} else if (_velocity > 0){
			
			//decrement velocity if there is no input
			_velocity -= m_acceleration * m_deccelerationMultiplier * Time.deltaTime;
			if (_velocity < 0)
				_velocity = 0;
		}

        CheckToroidal(); // TOROIDAL CHECKED

        // TODO: Translate the game object in world space
        transform.position += transform.forward * Time.deltaTime * _velocity;

		// set the blend parameter in your animator's movement blend tree
		//_animator.SetFloat ("Blend", _velocity / m_maxVelocity);

        

    }

    void CheckToroidal()
    {
        
        Vector3 perso = this.transform.position; //we simplify the use of tranform for our character
        Vector3 floorLim = floor.transform.position; // same for our floor


        //Debug.Log("Character x : " + perso.x);
        //Debug.Log("Character z: " + perso.z);
        if (perso.x > rightBorder) // when the character goes too far on the right
        {
            this.transform.position =  new Vector3(leftBorder,0,perso.z);
        }else if (perso.x < leftBorder)  // when the character goes too far on the left
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
