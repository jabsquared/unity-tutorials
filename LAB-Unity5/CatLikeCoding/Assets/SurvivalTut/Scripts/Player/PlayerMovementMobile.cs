using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovementMobile : MonoBehaviour
{
	public float speed = 5.4f;
	
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	
	int floorMask;
	
	float camRayLength = 100f;
	
	public bool canMove, canTurn, canAnimate;
	
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		Input.multiTouchEnabled = true;
	}
	
	void FixedUpdate ()
	{
		float h = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw ("Vertical");
		
		if (canMove)
			Move (h, v);
		if (canTurn)
			Turning ();
		if (canAnimate)
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
		if (Input.touchCount < 1)
			return;
		
		//int touchIndex = (Input.touchCount > 1) ? 1 : 0;
		
		Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (1).rawPosition);
		
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			//Debug.Log ("Ray hit!");
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


