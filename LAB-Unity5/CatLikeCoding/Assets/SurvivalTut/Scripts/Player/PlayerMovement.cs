using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5.4f;
	
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	
	int floorMask;
	
	float camRayLength = 100f;
	
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		Move (h, v);
		Turning ();
		Animating (h, v);
	}
	
	void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		
		movement = movement.normalized * speed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
		
	}
	
	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Debug.Log ("Ray hit!");
			Vector3 playerToMouse = floorHit.point - transform.position;
			
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			
			playerRigidbody.MoveRotation (newRotation);
		} else {
			Debug.Log ("Ray Doesn't hit!");			
		}
	}
	
	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}


